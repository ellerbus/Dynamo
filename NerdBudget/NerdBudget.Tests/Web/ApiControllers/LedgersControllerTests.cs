using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMoq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using NerdBudget.Web;
using NerdBudget.Web.ApiControllers;
using Newtonsoft.Json;

namespace NerdBudget.Tests.Web.ApiControllers
{
    [TestClass]
    public class LedgersControllerTests : ControllerTestHelper<Ledger>
    {
        #region Helpers/Test Initializers

        private AutoMoqer Mocker { get; set; }
        private Mock<IAccountService> MockService { get; set; }
        private LedgersController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<LedgersController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<IAccountService>();
        }

        private Account GetAccount(bool includeLedgers)
        {
            var account = Builder<Account>.CreateNew().With(x => x.Id = "x").Build();

            if (includeLedgers)
            {
                var ledgers = new[] { new Ledger { OriginalText = Helpers.OriginalLedgerText } };

                account.Ledgers.AddRange(ledgers);
            }

            var category = Builder<Category>.CreateNew().Build();

            account.Categories.Add(category);

            var budget = Builder<Budget>.CreateNew().Build();

            category.Budgets.Add(budget);

            return account;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddBasicPayload<Ledger>()
                .ToSettings();
        }

        #endregion

        #region Tests - Importing

        [TestMethod]
        public void LedgersController_PostImport_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(false);

            var text = "02/09/2015	NN NNNNN 9999 NNN# 9999993 99999 NNNNNNN NNNN	$1,499.99		$1,138.52	NNNNNNNNNNN NNNNNNN";

            var trx = new LedgersController.Trx { Transactions = text };

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Save(account.Ledgers));

            MockService.Setup(x => x.Save(account.Balances));

            //		act
            var msg = SubjectUnderTest.PostImport(account.Id, trx).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            Assert.AreEqual(1, account.Ledgers.Count);

            //  month-end, and week-end for 2/9
            Assert.AreEqual(2, account.Balances.Count);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_PostImport_Should_NotFound()
        {
            //		arrange
            var account = GetAccount(false);

            var text = "02/09/2015	NN NNNNN 9999 NNN# 9999993 99999 NNNNNNN NNNN	$1,499.99		$1,138.52	NNNNNNNNNNN NNNNNNN";

            var trx = new LedgersController.Trx { Transactions = text };

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.PostImport(account.Id, trx).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Get Many

        [TestMethod]
        public void LedgersController_GetMany_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            ledger.BudgetId = account.Categories.First().Budgets.First().Id;

            var settings = GetPayloadSettings();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.GetLedgers(ledger.AccountId, ledger.BudgetId, ledger.Date).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(account.Ledgers, settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_GetMany_Should_SendEmptySet()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            ledger.BudgetId = account.Categories.First().Budgets.First().Id;

            var settings = GetPayloadSettings();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.GetLedgers(ledger.AccountId, ledger.BudgetId, ledger.Date.AddDays(7)).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(new Ledger[0], settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_GetMany_Should_SendNotFound_Account()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Get(ledger.AccountId, ledger.Id, ledger.Date).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Get One

        [TestMethod]
        public void LedgersController_GetOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            var settings = GetPayloadSettings();

            var model = new
            {
                account = account,
                budgets = account.Categories.SelectMany(x => x.Budgets),
                ledger = ledger
            };

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Get(ledger.AccountId, ledger.Id, ledger.Date).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(model, settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_GetOne_Should_SendNotFound_Account()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Get(ledger.AccountId, ledger.Id, ledger.Date).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_GetOne_Should_SendNotFound_Ledger()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            account.Ledgers.Clear();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Get(ledger.AccountId, ledger.Id, ledger.Date).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void LedgersController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            ledger.BudgetId = account.Categories.First().Budgets.First().Id;

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Save(account.Ledgers));

            MockService.Setup(x => x.Save(account.Maps));

            //		act
            var msg = SubjectUnderTest.Put(ledger.AccountId, ledger.Id, ledger.Date, ledger).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_PutOne_Should_SendNotFound_Account()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Put(ledger.AccountId, ledger.Id, ledger.Date, ledger).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void LedgersController_PutOne_Should_SendNotFound_Ledger()
        {
            //		arrange
            var account = GetAccount(true);

            var ledger = account.Ledgers.Last();

            account.Ledgers.Clear();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(ledger.AccountId, ledger.Id, ledger.Date, ledger).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}