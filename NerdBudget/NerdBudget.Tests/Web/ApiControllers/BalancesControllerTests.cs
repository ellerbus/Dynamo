using System;
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
    public class BalancesControllerTests : ControllerTestHelper<Balance>
    {
        #region Helpers/Test Initializers

        private AutoMoqer Mocker { get; set; }
        private Mock<IAccountService> MockService { get; set; }
        private BalancesController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<BalancesController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<IAccountService>();
        }

        private Account GetAccount(bool includeBalances)
        {
            var account = Builder<Account>.CreateNew().With(x => x.Id = "x").Build();

            if (includeBalances)
            {
                var balances = new[] { new Balance { AsOf = DateTime.Now, Amount = 99 } };

                account.Balances.AddRange(balances);
            }

            return account;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Balance>("AccountId,AsOf,Amount")
                .ToSettings();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void BalancesController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var balance = account.Balances.Last();

            MockService.Setup(x => x.Get(balance.AccountId)).Returns(account);

            MockService.Setup(x => x.Save(Any.Enumerable));

            //		act
            var msg = SubjectUnderTest.Put(balance.AccountId, balance.AsOf, balance).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BalancesController_PutOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount(true);

            var balance = account.Balances.Last();

            MockService.Setup(x => x.Get(balance.AccountId)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Put(balance.AccountId, balance.AsOf, balance).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}