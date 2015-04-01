using System;
using System.Collections.Generic;
using System.Linq;
using Augment;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class LedgerModelTests
    {
        #region Helpers

        private static Ledger CreateEmptyLedger()
        {
            return new Ledger() { OriginalText = "01/01/15\taa\t$12\t\t$12\taa\taa" };
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Ledger_AccountId_Should_UpperCase()
        {
            var actual = new Ledger() { AccountId = "aa" };

            Assert.AreEqual("AA", actual.AccountId);
        }


        [TestMethod]
        public void Ledger_Id_Should_UpperCase()
        {
            var actual = new Ledger() { Id = "aa" };

            Assert.AreEqual("AA", actual.Id);
        }


        [TestMethod]
        public void Ledger_Date_Should_BeDate()
        {
            var actual = new Ledger() { Date = new DateTime(2000, 1, 1) };

            Assert.AreEqual(new DateTime(2000, 1, 1).BeginningOfDay(), actual.Date);
        }


        [TestMethod]
        public void Ledger_BudgetId_Should_UpperCase()
        {
            var actual = new Ledger() { BudgetId = "aa" };

            Assert.AreEqual("AA", actual.BudgetId);
        }


        [TestMethod]
        public void Ledger_OriginalText_Should_UpperCase()
        {
            var actual = CreateEmptyLedger();

            Assert.AreEqual("01/01/15\tAA\t$12\t\t$12\tAA\tAA", actual.OriginalText);
        }


        [TestMethod]
        public void Ledger_Description_Should_UpperCase()
        {
            var actual = new Ledger() { Description = "aa" };

            Assert.AreEqual("AA", actual.Description);
        }


        //[TestMethod]
        //public void Ledger_Amount_Should_DoSomething()
        //{
        //	var actual = new Ledger() { Amount = 9 };
        //
        //	Assert.AreEqual(9, actual.Amount);
        //}


        //[TestMethod]
        //public void Ledger_Balance_Should_DoSomething()
        //{
        //	var actual = new Ledger() { Balance = 9 };
        //
        //	Assert.AreEqual(9, actual.Balance);
        //}


        //[TestMethod]
        //public void Ledger_Sequence_Should_DoSomething()
        //{
        //	var actual = new Ledger() { Sequence = 9 };
        //
        //	Assert.AreEqual(9, actual.Sequence);
        //}


        [TestMethod]
        public void Ledger_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Ledger() { CreatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.CreatedAt);
        }


        [TestMethod]
        public void Ledger_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Ledger() { UpdatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.UpdatedAt);
        }


        #endregion

        #region Parser Tests

        [TestMethod]
        public void Ledger_Should_Parse_000()
        {
            var ledger = new Ledger
            {
                OriginalText = "02/09/2015	NN NNNNN 0010 NNN# 9999999 99999 NNNNNNN NNNN	$1,400.00		$1,138.52	NNNNNNNNNNN NNNNNNN"
            };

            Assert.AreEqual("1A18A3B2", ledger.Id);
            Assert.AreEqual(new DateTime(2015, 2, 9), ledger.Date);
            Assert.AreEqual("NN NNNNN 0010 NNN# 9999999 99999 NNNNNNN NNNN", ledger.Description);
            Assert.AreEqual(-1400, ledger.Amount);
            Assert.AreEqual(1138.52, ledger.Balance);
            Assert.AreEqual(@"NNNNNNN[0-9]+NNN[0-9]+NNNNNNNNNNN", ledger.RegexMap);
        }

        [TestMethod]
        public void Ledger_Should_Parse_001()
        {
            var ledger = new Ledger
            {
                OriginalText = "02/09/2015	NNNNNNNN'N N99999 NNNNNNN NN 02/08/15 NNN 9999	$9.82		$2,538.52	NNNNNNNNNNN NNNNNNN"
            };

            Assert.AreEqual("10D9DAF6", ledger.Id);
            Assert.AreEqual(new DateTime(2015, 2, 9), ledger.Date);
            Assert.AreEqual("NNNNNNNN'N N99999 NNNNNNN NN 02/08/15 NNN 9999", ledger.Description);
            Assert.AreEqual(-9.82, ledger.Amount);
            Assert.AreEqual(2538.52, ledger.Balance);
            Assert.AreEqual(@"NNNNNNNNNN[0-9]+NNNNNNNNN[0-9]+NNN[0-9]+", ledger.RegexMap);
        }

        [TestMethod]
        public void Ledger_Should_Parse_002()
        {
            var ledger = new Ledger
            {
                OriginalText = "02/09/2015	NNNNNNNN : NNNNNNNN NN: 99999999NN: NNNNNNNN NNN NNNNN 99999999		$1,406.00	$2,548.34	NNNNNNNNNNN NNNNNNN"
            };

            Assert.AreEqual("999BEDC8", ledger.Id);
            Assert.AreEqual(new DateTime(2015, 2, 9), ledger.Date);
            Assert.AreEqual("NNNNNNNN : NNNNNNNN NN: 99999999NN: NNNNNNNN NNN NNNNN 99999999", ledger.Description);
            Assert.AreEqual(1406, ledger.Amount);
            Assert.AreEqual(2548.34, ledger.Balance);
            Assert.AreEqual(@"NNNNNNNNNNNNNNNNNN[0-9]+NNNNNNNNNNNNNNNNNN[0-9]+", ledger.RegexMap);
        }

        [TestMethod]
        public void Ledger_Should_Parse_003()
        {
            var ledger = new Ledger
            {
                OriginalText = "02/09/2015	NNNNNNN NNNNNNNN #9999 NNNNNNN NN 02/08/15 NNN 9999	$17.81		$1,142.34	NNNNNNNNNNN NNNNNNN"
            };

            Assert.AreEqual("98AEE42E", ledger.Id);
            Assert.AreEqual(new DateTime(2015, 2, 9), ledger.Date);
            Assert.AreEqual("NNNNNNN NNNNNNNN #9999 NNNNNNN NN 02/08/15 NNN 9999", ledger.Description);
            Assert.AreEqual(-17.81, ledger.Amount);
            Assert.AreEqual(1142.34, ledger.Balance);
            Assert.AreEqual(@"NNNNNNNNNNNNNNN[0-9]+NNNNNNNNN[0-9]+NNN[0-9]+", ledger.RegexMap);
        }

        [TestMethod]
        public void Ledger_Should_Parse_004()
        {
            var ledger = new Ledger
            {
                OriginalText = "02/07/2015	9999-NNNN NNN NNN N-9999, 9999 NN NNNNNNN NN NNN 9999	$68.16		$1,218.82	NNNNNNNNNNN NNNNNNN"
            };

            Assert.AreEqual("6A9645D", ledger.Id);
            Assert.AreEqual(new DateTime(2015, 2, 7), ledger.Date);
            Assert.AreEqual("9999-NNNN NNN NNN N-9999, 9999 NN NNNNNNN NN NNN 9999", ledger.Description);
            Assert.AreEqual(-68.16, ledger.Amount);
            Assert.AreEqual(1218.82, ledger.Balance);
            Assert.AreEqual(@"[0-9]+NNNNNNNNNNN[0-9]+NNNNNNNNNNNNNN[0-9]+", ledger.RegexMap);
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void LedgerCollection_Should_FindMissingBudget()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var ledger = CreateEmptyLedger();

            //  act
            account.Ledgers.Add(ledger);

            //  assert
            Assert.AreEqual(1, account.Ledgers.MissingBudget().Count());
        }

        [TestMethod]
        public void LedgerCollection_Should_FindLedger()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var ledger = CreateEmptyLedger();

            //  act
            account.Ledgers.Add(ledger);

            //  assert
            var x = account.Ledgers.Find(ledger.Id.ToLower(), ledger.Date);

            Assert.AreSame(ledger, x);
        }

        [TestMethod]
        public void LedgerCollection_Should_FindMap()
        {
            //  assign 
            var account = CreateAccount();

            var ledger = CreateEmptyLedger();

            var map = new Map { RegexPattern = ledger.RegexMap, BudgetId = account.Categories.First().Budgets.First().Id };

            account.Maps.Add(map);

            //  act
            account.Ledgers.Import(ledger.OriginalText);

            //  assert
            Assert.AreEqual(map.BudgetId, account.Ledgers.First().BudgetId);
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

        [TestMethod]
        public void LedgerCollection_Should_AssignAccountId()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var ledger = CreateEmptyLedger();

            //  act
            account.Ledgers.Add(ledger);

            //  assert
            Assert.AreEqual(account.Id, ledger.AccountId);
        }

        [TestMethod]
        public void LedgerCollection_Should_LookupUsingId()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var l1 = CreateEmptyLedger();

            //  act
            account.Ledgers.Add(l1);

            //  assert
            Assert.AreEqual(l1, account.Ledgers.First());
        }

        [TestMethod]
        public void LedgerCollection_Should_ParseInputAndUpdateBalances()
        {
            //  assign
            var account = Builder<Account>.CreateNew().Build();

            var input = @"02/09/2015	NN NNNNN 0010 NNN# 9999999 99999 NNNNNNN NNNN	$1,400.00		$1,138.52	NNNNNNNNNNN NNNNNNN
02/09/2015	NNNNNNNN'N N99999 NNNNNNN NN 02/08/15 NNN 9999	$9.82		$2,538.52	NNNNNNNNNNN NNNNNNN
02/09/2015	NNNNNNNN : NNNNNNNN NN: 99999999NN: NNNNNNNN NNN NNNNN 99999999		$1,406.00	$2,548.34	NNNNNNNNNNN NNNNNNN
02/09/2015	NNNNNNN NNNNNNNN #2189 NNNNNNN NN 02/08/15 NNN 5231	$17.81		$1,142.34	NNNNNNNNNNN NNNNNNN
02/08/2015	NNNNNNNN'N N99999 NNNNNNN NN 02/07/15 NNN 9999	$13.80		$1,160.15	NNNNNNNNNNN NNNNNNN
02/08/2015	NNNN'N NNN NNNN #3 NNNNNNN NN 02/07/15 NNN 7542	$10.00		$1,173.95	NNNNNNNNNNN NNNNNNN
02/08/2015	NNNNNN 10250 NNNNN NNNN N NNNNNNN NN NNN 5411	$17.47		$1,183.95	NNNNNNNNNNN NNNNNNN
02/07/2015	NNNNNN 10250 NNNNN NNNN N NNNNNNN NN NNN 5411	$17.40		$1,201.42	NNNNNNNNNNN NNNNNNN
02/07/2015	10726-NNNN NNN NNN N-260, 4200 NN NNNNNNN NN NNN 5691	$68.16		$1,218.82	NNNNNNNNNNN NNNNNNN
02/07/2015	NNN - N-NNNN N/N 407-690-5000 NN 02/07/15 NNN 4784	$55.00		$1,286.98	NNNNNNNNNNN NNNNNNN";

            //  act
            account.Ledgers.Import(input);

            //  assert
            Assert.AreEqual(10, account.Ledgers.Count);

            Assert.AreEqual(2, account.Balances.Count);

            Assert.IsTrue(account.Balances.Contains(new DateTime(2015, 2, 1)));

            Assert.IsTrue(account.Balances.Contains(new DateTime(2015, 2, 6)));
        }

        [TestMethod]
        public void LedgerCollection_Should_ParseEmptyInput()
        {
            //  assign
            var account = Builder<Account>.CreateNew().Build();

            var input = @"";

            //  act
            account.Ledgers.Import(input);

            //  assert
            Assert.AreEqual(0, account.Ledgers.Count);
        }

        [TestMethod]
        public void LedgerCollection_Should_ParseDuplicateInput()
        {
            //  assign
            var account = Builder<Account>.CreateNew().Build();

            var input = @"02/09/2015	NN NNNNN 0010 NNN# 9999999 99999 NNNNNNN NNNN	$1,400.00		$1,138.52	NNNNNNNNNNN NNNNNNN
02/09/2015	NN NNNNN 0010 NNN# 9999999 99999 NNNNNNN NNNN	$1,400.00		$1,138.52	NNNNNNNNNNN NNNNNNN";

            //  act
            account.Ledgers.Import(input);

            //  assert
            Assert.AreEqual(1, account.Ledgers.Count);
        }

        #endregion
    }
}