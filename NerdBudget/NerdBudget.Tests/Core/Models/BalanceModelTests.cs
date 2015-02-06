using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class BalanceModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Balance_AccountId_Should_UpperCase()
        {
            var actual = new Balance() { AccountId = "aa" };

            Assert.AreEqual("AA", actual.AccountId);
        }

                
        //[TestMethod]
        //public void Balance_AsOf_Should_DoSomething()
        //{
        //	var actual = new Balance() { AsOf = new DateTime(2000, 1, 1) };
        //
        //	Assert.AreEqual(new DateTime(2000, 1, 1), actual.AsOf);
        //}

                
        //[TestMethod]
        //public void Balance_Amount_Should_DoSomething()
        //{
        //	var actual = new Balance() { Amount = 9 };
        //
        //	Assert.AreEqual(9, actual.Amount);
        //}


        [TestMethod]
        public void Balance_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Balance() { CreatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.CreatedAt);
        }


        [TestMethod]
        public void Balance_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Balance() { UpdatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.UpdatedAt);
        }


        #endregion

        #region Collection Tests

        [TestMethod]
        public void BalanceCollection_Should_AssignAccountId()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var balance = Builder<Balance>.CreateNew().Build();

            //  act
            account.Balances.Add(balance);

            //  assert
            Assert.AreEqual(account.Id, balance.AccountId);
        }

        [TestMethod]
        public void BalanceCollection_Should_LookupUsingAsOf()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var b1 = Builder<Balance>.CreateNew().Build();

            //  act
            account.Balances.Add(b1);

            //  assert
            Assert.AreEqual(b1, account.Balances[b1.AsOf]);
        }

        #endregion
    }
}