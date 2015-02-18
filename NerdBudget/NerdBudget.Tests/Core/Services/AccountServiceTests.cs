using System;
using System.Collections;
using System.Collections.Generic;
using FizzWare.NBuilder;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;

namespace NerdBudget.Tests.Core.Services
{
    [TestClass]
    public class AccountServiceTests : ServiceTestHelper<Account, AccountService, IAccountRepository>
    {
        #region Helpers/Test Initializers

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        #endregion

        #region Tests - Get

        [TestMethod]
        public void AccountService_GetMany_Should_SucceedOnCacheMiss()
        {
            //	arrange
            var accounts = Builder<Account>.CreateListOfSize(10).Build();

            MockRepo.Setup(x => x.GetList()).Returns(accounts);

            MockCache.Setup(x => x.Get(Any.String)).Returns(null as IList<Account>);

            MockCache.Setup(x => x.Add(Any.String, accounts, Any.TimeSpan, Any.CacheExpiration, Any.CachePriority));

            //	act
            var actual = SubjectUnderTest.GetList();

            //	assert
            CollectionAssert.AreEqual(accounts as ICollection, actual as ICollection);

            MockRepo.VerifyAll();

            MockCache.VerifyAll();
        }

        [TestMethod]
        public void AccountService_GetMany_Should_SucceedOnCacheHit()
        {
            //	arrange
            var accounts = Builder<Account>.CreateListOfSize(10).Build();

            MockCache.Setup(x => x.Get(Any.String)).Returns(accounts);

            //	act
            var actual = SubjectUnderTest.GetList();

            //	assert
            CollectionAssert.AreEqual(accounts as ICollection, actual as ICollection);

            MockRepo.VerifyAll();

            MockCache.VerifyAll();
        }

        [TestMethod]
        public void AccountService_GetOne_Should_SucceedOnCacheMiss()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            MockRepo.Setup(x => x.Get(account.Id)).Returns(account);

            MockCache.Setup(x => x.Get(Any.String)).Returns(null as Account);

            MockCache.Setup(x => x.Add(Any.String, account, Any.TimeSpan, Any.CacheExpiration, Any.CachePriority));

            //	act
            var actual = SubjectUnderTest.Get(account.Id);

            //	assert
            AssertEqual(account, actual);

            MockRepo.VerifyAll();

            MockCache.VerifyAll();
        }

        [TestMethod]
        public void AccountService_GetOne_Should_SucceedOnCacheHit()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            MockCache.Setup(x => x.Get(Any.String)).Returns(account);

            //	act
            var actual = SubjectUnderTest.Get(account.Id);

            //	assert
            AssertEqual(account, actual);

            MockRepo.VerifyAll();

            MockCache.VerifyAll();
        }

        #endregion

        #region Tests - Insert

        [TestMethod]
        public void AccountService_Insert_Should_Succeed()
        {
            //	arrange
            var account = Builder<Account>.CreateNew()
                .With(x => x.Id = "")
                .With(x => x.StartedAt = DateTime.MinValue)
                .With(x => x.CreatedAt = DateTime.MinValue)
                .Build();

            MockRepo.Setup(x => x.Insert(account));

            MockValidator.Setup(x => x.Validate(account)).Returns(ValidationSuccess);

            MockCache.Setup(x => x.GetAllKeys()).Returns(new[] { Any.ListKey });

            MockCache.Setup(x => x.Remove(Any.ListKey));

            //	act
            SubjectUnderTest.Insert(account);

            //	assert
            Assert.AreEqual("C", account.Type);
            Assert.AreNotEqual(DateTime.MinValue, account.StartedAt);
            Assert.AreNotEqual(DateTime.MinValue, account.CreatedAt);
            Assert.AreNotEqual("", account.Id);

            MockRepo.VerifyAll();

            MockValidator.VerifyAll();

            MockCache.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void AccountService_Insert_Should_ThrowException()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            MockRepo.Setup(x => x.Insert(account));

            MockValidator.Setup(x => x.Validate(account)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Insert(account);
        }

        #endregion

        #region Tests - Update

        [TestMethod]
        public void AccountService_Update_Should_Succeed()
        {
            //	arrange
            var account = Builder<Account>.CreateNew()
                .With(x => x.UpdatedAt = DateTime.MinValue)
                .Build();

            MockRepo.Setup(x => x.Update(account));

            MockValidator.Setup(x => x.Validate(account)).Returns(ValidationSuccess);

            MockCache.Setup(x => x.GetAllKeys()).Returns(new[] { Any.ListKey });

            MockCache.Setup(x => x.Remove(Any.ListKey));

            MockCache.Setup(x => x.Remove(Any.GetByPrimaryKey(account.Id)));

            //	act
            SubjectUnderTest.Update(account);

            //	assert
            Assert.AreNotEqual(DateTime.MinValue, account.UpdatedAt);

            MockRepo.VerifyAll();

            MockValidator.VerifyAll();

            MockCache.VerifyAll();
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void AccountService_Update_Should_ThrowException()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            MockRepo.Setup(x => x.Update(account));

            MockValidator.Setup(x => x.Validate(account)).Returns(ValidationFailure);

            //	act
            SubjectUnderTest.Update(account);
        }

        #endregion

        #region Tests - Delete

        [TestMethod]
        public void AccountService_Delete_Should_Succeed()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            MockRepo.Setup(x => x.Delete(account));

            MockCache.Setup(x => x.GetAllKeys()).Returns(new[] { Any.ListKey });

            MockCache.Setup(x => x.Remove(Any.ListKey));

            MockCache.Setup(x => x.Remove(Any.GetByPrimaryKey(account.Id)));

            //	act
            SubjectUnderTest.Delete(account);

            //	assert

            MockRepo.VerifyAll();

            MockCache.VerifyAll();
        }

        #endregion

        #region Tests - Save (Balances/Ledgers)

        [TestMethod]
        public void AccountService_SaveBalances_Should_Succeed()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            account.Balances.Add(new Balance { AsOf = new DateTime(2015, 2, 6), CreatedAt = new DateTime(2015, 1, 1) });
            account.Balances.Add(new Balance { AsOf = new DateTime(2015, 2, 28) });

            MockRepo.Setup(x => x.Save(account.Balances));

            //	act
            SubjectUnderTest.Save(account.Balances);

            //	assert
            Assert.AreEqual(new DateTime(2015, 1, 1).Date, account.Balances[0].CreatedAt.Date);
            Assert.AreEqual(DateTime.UtcNow.Date, account.Balances[0].UpdatedAt.Value.Date);

            Assert.AreEqual(DateTime.UtcNow.Date, account.Balances[1].CreatedAt.Date);
            Assert.AreEqual(null, account.Balances[1].UpdatedAt);

            MockRepo.VerifyAll();
        }

        [TestMethod]
        public void AccountService_SaveLedgers_Should_Succeed()
        {
            //	arrange
            var account = Builder<Account>.CreateNew().Build();

            account.Ledgers.Add(new Ledger { OriginalText = Helpers.OriginalLedgerText, CreatedAt = new DateTime(2015, 1, 1) });
            account.Ledgers.Add(new Ledger { OriginalText = Helpers.OriginalLedgerText });

            MockRepo.Setup(x => x.Save(account.Ledgers));

            //	act
            SubjectUnderTest.Save(account.Ledgers);

            //	assert
            Assert.AreEqual(new DateTime(2015, 1, 1).Date, account.Ledgers[0].CreatedAt.Date);
            Assert.AreEqual(DateTime.UtcNow.Date, account.Ledgers[0].UpdatedAt.Value.Date);

            Assert.AreEqual(DateTime.UtcNow.Date, account.Ledgers[1].CreatedAt.Date);
            Assert.AreEqual(null, account.Ledgers[1].UpdatedAt);

            MockRepo.VerifyAll();
        }

        #endregion

        #region Assertion Helpers

        private void AssertEqual(Account expected, Account actual)
        {
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, actual.Id);
            Assert.AreEqual(actual.Name, actual.Name);
            Assert.AreEqual(actual.Type, actual.Type);
            Assert.AreEqual(actual.StartedAt, actual.StartedAt);
            Assert.AreEqual(actual.CreatedAt, actual.CreatedAt);
            Assert.AreEqual(actual.UpdatedAt, actual.UpdatedAt);
        }

        #endregion
    }
}