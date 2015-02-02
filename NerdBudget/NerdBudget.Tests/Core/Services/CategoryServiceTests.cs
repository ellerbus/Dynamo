using System;
using System.Linq;
using FizzWare.NBuilder;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;

namespace NerdBudget.Tests.Core.Services
{
    [TestClass]
    public class CategoryServiceTests : ServiceTestHelper<Category, CategoryService, ICategoryRepository>
    {
        #region Helpers/Test Initializers

        private const int CountOfCategories = 10;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        private Account GetAccount()
        {
            var account = Builder<Account>.CreateNew().Build();

            var categories = Builder<Category>.CreateListOfSize(CountOfCategories).Build();

            account.Categories.AddRange(categories);

            return account;
        }

        #endregion

        #region Tests - Insert

        [TestMethod]
        public void CategoryService_Insert_Should_Succeed()
        {
            //	arrange
            var category = Builder<Category>.CreateNew()
                .With(x => x.Id = "")
                .With(x => x.CreatedAt = DateTime.MinValue)
                .Build();

            var account = GetAccount();

            MockRepo.Setup(x => x.Insert(category));

            MockValidator.Setup(x => x.Validate(category)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Insert(account, category);

            //	assert
            Assert.AreEqual(CountOfCategories + 1, account.Categories.Count);

            Assert.AreEqual(account.Id, category.AccountId);
            Assert.AreEqual(account.Categories.Count * 10 - 10, category.Sequence);
            Assert.AreNotEqual(DateTime.MinValue, category.CreatedAt);
            Assert.AreNotEqual("", category.Id);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void CategoryService_Insert_Should_ThrowException()
        {
            //	arrange
            var category = Builder<Category>.CreateNew().Build();

            var account = GetAccount();

            MockRepo.Setup(x => x.Insert(category));

            MockValidator.Setup(x => x.Validate(category)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Insert(account, category);
        }

        #endregion

        #region Tests - Update

        [TestMethod]
        public void CategoryService_Update_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            category.UpdatedAt = DateTime.MinValue;

            MockRepo.Setup(x => x.Update(category));

            MockValidator.Setup(x => x.Validate(category)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(category);

            //	assert
            Assert.AreNotEqual(DateTime.MinValue, category.UpdatedAt);

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void CategoryService_Update_Should_ThrowException()
        {
            //	arrange
            var category = Builder<Category>.CreateNew().Build();

            MockRepo.Setup(x => x.Update(category));

            MockValidator.Setup(x => x.Validate(category)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Update(category);
        }

        [TestMethod]
        public void CategoryService_UpdateMany_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            foreach (var c in account.Categories)
            {
                c.UpdatedAt = DateTime.MinValue;
            }

            MockRepo.Setup(x => x.Update(account.Categories));

            MockValidator.Setup(x => x.Validate(Any.Item)).Returns(ValidationSuccess);

            //	act
            SubjectUnderTest.Update(account.Categories);

            //	assert
            foreach (var c in account.Categories)
            {
                Assert.AreNotEqual(DateTime.MinValue, c.UpdatedAt);
            }

            MockRepo.VerifyAll();
            MockValidator.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void CategoryService_UpdateMany_Should_ThrowException()
        {
            //	arrange
            var account = GetAccount();

            MockRepo.Setup(x => x.Update(account.Categories));

            MockValidator.Setup(x => x.Validate(Any.Item)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Update(account.Categories);
        }

        #endregion

        #region Tests - Delete

        [TestMethod]
        public void CategoryService_Delete_Should_Succeed()
        {
            //	arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            MockRepo.Setup(x => x.Delete(category));

            //	act
            SubjectUnderTest.Delete(category);

            //	assert
            Assert.AreEqual(CountOfCategories - 1, account.Categories.Count);

            MockRepo.VerifyAll();
        }

        #endregion
    }
}