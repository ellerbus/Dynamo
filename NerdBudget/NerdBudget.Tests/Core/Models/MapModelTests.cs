using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core;
using NerdBudget.Core.Models;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class MapModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Map_AccountId_Should_UpperCase()
        {
            var actual = new Map() { AccountId = "aa" };

            Assert.AreEqual("AA", actual.AccountId);
        }

        [TestMethod]
        public void Map_Id_Should_UpperCase()
        {
            var actual = new Map() { Id = "aa" };

            Assert.AreEqual("AA", actual.Id);
        }

        [TestMethod]
        public void Map_RegexPattern_Should_AssignId()
        {
            var actual = new Map() { RegexPattern = "aa" };

            Assert.AreEqual(Crc32.Hash("aa").ToUpper(), actual.Id);
        }

        [TestMethod]
        public void Map_RegexPattern_Should_BeSameAsAssignment()
        {
            var actual = new Map() { RegexPattern = "aa" };

            Assert.AreEqual("aa", actual.RegexPattern);
        }


        [TestMethod]
        public void Map_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Map() { CreatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.CreatedAt);
        }


        [TestMethod]
        public void Map_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Map() { UpdatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.UpdatedAt);
        }


        #endregion

        #region Method Tests

        [TestMethod]
        public void Map_Should_MatchUsingRegex()
        {
            var ledger = new Ledger { OriginalText = "03/09/2015\tNNNNNNNN : NNNNNNNN NN: 9999999999NN: NNNNNNNN NNN NNNNN 999999999999999\t\t$1,406.99\t$2,548.34\tNNNNNNNNNNN NNNNNNN" };

            var map = new Map { RegexPattern = ledger.RegexMap };

            Assert.IsTrue(map.IsMatchFor(ledger));
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void MapCollection_Should_CreateMapForLedger()
        {
            //  assign 
            var account = CreateAccount();

            var budget = account.Categories.First().Budgets.First();

            var ledger = new Ledger { BudgetId = budget.Id, OriginalText = Helpers.OriginalLedgerText };

            //  act

            //  assert
            account.Maps.CreateFor(ledger);

            Assert.AreEqual(1, account.Maps.Count);

            Assert.AreEqual("X", account.Maps[0].BudgetId);
        }

        [TestMethod]
        public void MapCollection_Should_Not_CreateMapForLedger()
        {
            //  assign 
            var account = CreateAccount();

            var budget = account.Categories.First().Budgets.First();

            var map = Builder<Map>.CreateNew()
                .With(x => x.RegexPattern = "AB")
                .With(x => x.BudgetId = budget.Id)
                .Build();

            var ledger = new Ledger { BudgetId = budget.Id, OriginalText = Helpers.OriginalLedgerText };

            //  act
            account.Maps.Add(map);

            //  assert
            account.Maps.CreateFor(ledger);

            Assert.AreEqual(1, account.Maps.Count);

            Assert.AreEqual("X", account.Maps[0].BudgetId);
        }

        private Account CreateAccount()
        {
            var account = Builder<Account>.CreateNew().Build();

            var category = Builder<Category>.CreateNew().Build();

            var budget = new Budget { CategoryId = category.Id, Id = "X" };

            var po = new PrivateObject(account);

            po.SetField("_categories", null);
            po.SetProperty("AllCategories", new List<Category> { category });

            po.SetField("_budgets", null);
            po.SetProperty("AllBudgets", new List<Budget> { budget });

            return account;
        }

        #endregion
    }
}