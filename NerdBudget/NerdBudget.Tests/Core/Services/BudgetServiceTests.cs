using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;

namespace NerdBudget.Tests.Core.Services
{
    [TestClass]
    public class BudgetServiceTests : ServiceTestHelper<Budget, BudgetService, IBudgetRepository>
    {
        #region Helpers/Test Initializers

        private const int CountOfCategories = 10;

        private const int CountOfBudgets = 3;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        private Account GetAccount()
        {
            var account = Builder<Account>.CreateNew().Build();

            var categories = Builder<Category>.CreateListOfSize(CountOfCategories)
                .Build();

            account.Categories.AddRange(categories);

            foreach (var c in account.Categories)
            {
                var budgets = Builder<Budget>.CreateListOfSize(CountOfBudgets).Build();

                foreach (var b in budgets)
                {
                    b.Id = c.Id + b.Id;

                    c.Budgets.Add(b);
                }
            }

            return account;
        }

        #endregion

        #region Tests - Insert

        [TestMethod]
        public void BudgetService_Insert_Should_Succeed()
        {
            //	arrange
            var budget = Builder<Budget>.CreateNew()
                .With(x => x.Id = "")
                .With(x => x.CreatedAt = DateTime.MinValue)
                .Build();

            var account = GetAccount();

            var category = account.Categories.Last();

            var po = new PrivateObject(account);

            var allBudgets = po.GetProperty("Budgets") as BudgetCollection;

            MockRepo.Setup(x => x.Save(budget));

            MockValidator.Setup(x => x.Validate(budget)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Insert(category, budget);

            //	assert
            Assert.AreEqual(CountOfBudgets + 1, category.Budgets.Count);

            Assert.AreEqual(account.Id, budget.AccountId);
            Assert.AreEqual(category.Id, budget.CategoryId);
            Assert.AreEqual(category.Budgets.Count * 10 - 10, budget.Sequence);
            Assert.AreNotEqual(DateTime.MinValue, budget.CreatedAt);
            Assert.AreNotEqual("", budget.Id);

            //  Since we didn't load ACCOUNT from the database, our test will
            //  only have the newly inserted budget for the "ALL" FK
            Assert.AreEqual(1, allBudgets.Count);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void BudgetService_Insert_Should_ThrowException()
        {
            //	arrange
            var budget = Builder<Budget>.CreateNew()
                .With(x => x.Id = "")
                .With(x => x.CreatedAt = DateTime.MinValue)
                .Build();

            var account = GetAccount();

            var category = account.Categories.Last();

            MockRepo.Setup(x => x.Save(budget));

            MockValidator.Setup(x => x.Validate(budget)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Insert(category, budget);
        }

        #endregion

        #region Tests - Update

        [TestMethod]
        public void BudgetService_Update_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var budget = category.Budgets.Last();

            budget.UpdatedAt = DateTime.MinValue;

            MockRepo.Setup(x => x.Save(budget));

            MockValidator.Setup(x => x.Validate(budget)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(budget);

            //	assert
            Assert.AreNotEqual(DateTime.MinValue, budget.UpdatedAt);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_Update_Should_Succeed_WithDifferentCategory()
        {
            //	arrange
            var account = GetAccount();

            var goToCat = account.Categories.First();
            var isFromCat = account.Categories.Last();

            var budget = isFromCat.Budgets.Last();

            budget.CategoryId = goToCat.Id;

            budget.UpdatedAt = DateTime.MinValue;

            MockRepo.Setup(x => x.Save(budget));

            MockValidator.Setup(x => x.Validate(budget)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(budget);

            //	assert
            Assert.AreNotEqual(DateTime.MinValue, budget.UpdatedAt);

            Assert.AreEqual(CountOfBudgets + 1, goToCat.Budgets.Count);
            Assert.AreEqual(CountOfBudgets - 1, isFromCat.Budgets.Count);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void BudgetService_Update_Should_ThrowException()
        {
            //	arrange
            var budget = Builder<Budget>.CreateNew().Build();

            MockRepo.Setup(x => x.Save(budget));

            MockValidator.Setup(x => x.Validate(budget)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Update(budget);
        }

        [TestMethod]
        public void BudgetService_UpdateMany_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            MockRepo.Setup(x => x.Save(category.Budgets));

            MockValidator.Setup(x => x.Validate(Any.Item)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(category.Budgets);

            //	assert
            foreach (var b in category.Budgets)
            {
                Assert.AreNotEqual(DateTime.MinValue, b.UpdatedAt);
            }

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void BudgetService_UpdateMany_Should_ThrowException()
        {
            //	arrange
            var budgets = Builder<Budget>.CreateListOfSize(10).Build();

            MockRepo.Setup(x => x.Save(budgets));

            MockValidator.Setup(x => x.Validate(It.IsAny<Budget>())).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Update(budgets);
        }

        #endregion

        #region Tests - Delete

        [TestMethod]
        public void BudgetService_Delete_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var budget = category.Budgets.Last();

            MockRepo.Setup(x => x.Delete(budget));

            //	act
            SubjectUnderTest.Delete(budget);

            //	assert
            Assert.AreEqual(CountOfBudgets - 1, category.Budgets.Count);

            MockRepo.VerifyAll();
        }

        #endregion
    }
}