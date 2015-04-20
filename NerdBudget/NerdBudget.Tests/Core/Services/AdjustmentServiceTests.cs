using System;
using System.Linq;
using FizzWare.NBuilder;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;

namespace NerdBudget.Tests.Core.Services
{
    [TestClass]
    public class AdjustmentServiceTests : ServiceTestHelper<Adjustment, AdjustmentService, IAdjustmentRepository>
    {
        #region Helpers/Test Initializers

        private const int CountOfCategories = 3;

        private const int CountOfBudgets = 3;

        private const int CountOfAdjustments = 3;

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

                    var adjustments = Builder<Adjustment>.CreateListOfSize(CountOfAdjustments).Build();

                    foreach (var a in adjustments)
                    {
                        a.Id = c.Id + b.Id + a.Id;

                        b.Adjustments.Add(a);
                    }
                }
            }

            return account;
        }

        #endregion

        #region Tests - Insert

        [TestMethod]
        public void AdjustmentService_Insert_Should_Succeed()
        {
            //	arrange
            var adjustment = Builder<Adjustment>.CreateNew()
                 .With(x => x.Id = "")
                 .With(x => x.CreatedAt = DateTime.MinValue)
                 .Build();

            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            var po = new PrivateObject(account);

            var allAdjustments = po.GetProperty("Adjustments") as AdjustmentCollection;

            MockRepo.Setup(x => x.Save(adjustment));

            MockValidator.Setup(x => x.Validate(adjustment)).Returns(new ValidationResult());

            //	act
            SubjectUnderTest.Insert(budget, adjustment);

            //	assert

            //	assert
            Assert.AreEqual(CountOfAdjustments + 1, budget.Adjustments.Count);

            Assert.AreEqual(account.Id, adjustment.AccountId);
            Assert.AreEqual(budget.Id, adjustment.BudgetId);
            Assert.AreNotEqual(DateTime.MinValue, budget.CreatedAt);
            Assert.AreNotEqual("", adjustment.Id);

            //  Since we didn't load ACCOUNT from the database, our test will
            //  only have the newly inserted budget for the "ALL" FK
            Assert.AreEqual(1, allAdjustments.Count);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void AdjustmentService_Insert_Should_ThrowException()
        {
            //	arrange
            var adjustment = Builder<Adjustment>.CreateNew()
                .With(x => x.Id = "")
                .With(x => x.CreatedAt = DateTime.MinValue)
                .Build();

            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            MockRepo.Setup(x => x.Save(adjustment));

            MockValidator.Setup(x => x.Validate(adjustment)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Insert(budget, adjustment);
        }

        #endregion

        #region Tests - Update

        [TestMethod]
        public void AdjustmentService_Update_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            var adjustment = budget.Adjustments.Last();

            adjustment.UpdatedAt = DateTime.MinValue;

            MockRepo.Setup(x => x.Save(adjustment));

            MockValidator.Setup(x => x.Validate(adjustment)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(adjustment);

            //	assert
            Assert.AreNotEqual(DateTime.MinValue, adjustment.UpdatedAt);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentService_Update_Should_Succeed_WithDifferentBudget()
        {
            //	arrange
            var account = GetAccount();

            var goToBud = account.Categories.First().Budgets.First();
            var fromBud = account.Categories.Last().Budgets.Last();

            var po = new PrivateObject(account);

            //  Budgets is "internally" managed
            (po.GetProperty("Budgets") as BudgetCollection).AddRange(new[] { goToBud, fromBud });

            var adjustment = fromBud.Adjustments.Last();

            adjustment.BudgetId = goToBud.Id;

            adjustment.UpdatedAt = DateTime.MinValue;

            MockRepo.Setup(x => x.Save(adjustment));

            MockValidator.Setup(x => x.Validate(adjustment)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(adjustment);

            //	assert
            Assert.AreNotEqual(DateTime.MinValue, adjustment.UpdatedAt);

            Assert.AreEqual(CountOfAdjustments + 1, goToBud.Adjustments.Count);
            Assert.AreEqual(CountOfAdjustments - 1, fromBud.Adjustments.Count);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void AdjustmentService_Update_Should_ThrowException()
        {
            //	arrange
            var adjustment = Builder<Adjustment>.CreateNew().Build();

            MockRepo.Setup(x => x.Save(adjustment));

            MockValidator.Setup(x => x.Validate(adjustment)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Update(adjustment);
        }

        #endregion

        #region Tests - Delete

        [TestMethod]
        public void AdjustmentService_Delete_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            var adjustment = budget.Adjustments.Last();

            MockRepo.Setup(x => x.Delete(adjustment));

            //	act
            SubjectUnderTest.Delete(adjustment);

            //	assert
            Assert.AreEqual(CountOfAdjustments - 1, budget.Adjustments.Count);

            MockRepo.VerifyAll();
        }

        #endregion
    }
}