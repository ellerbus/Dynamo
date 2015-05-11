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
    public class MapsControllerTests : ControllerTestHelper<Map>
    {
        #region Helpers/Test Initializers

        private AutoMoqer Mocker { get; set; }
        private Mock<IAccountService> MockService { get; set; }
        private MapsController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<MapsController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<IAccountService>();
        }

        private Account GetAccount()
        {
            var account = Builder<Account>.CreateNew().With(x => x.Id = "x").Build();

            var category = Builder<Category>.CreateNew().Build();

            account.Categories.Add(category);

            var budget = Builder<Budget>.CreateNew().Build();

            category.Budgets.Add(budget);

            var po = new PrivateObject(account, "Budgets");

            BudgetCollection budgets = po.Target as BudgetCollection;

            foreach (var b in account.Categories.SelectMany(x => x.Budgets))
            {
                budgets.Add(b);

                Map m = new Map { RegexPattern = "A+" + b.Id };

                m.BudgetId = b.Id;

                account.Maps.Add(m);
            }

            return account;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddStandardPayload<Map>()
                .ToSettings();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void MapsController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var map = account.Maps.Last();

            map.BudgetId = account.Categories.First().Budgets.First().Id;

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Save(account.Maps));

            //		act
            var msg = SubjectUnderTest.Put(map.AccountId, map.Id, map).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void MapsController_PutOne_Should_SendNotFound_Account()
        {
            //		arrange
            var account = GetAccount();

            var map = account.Maps.Last();

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Put(map.AccountId, map.Id, map).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void MapsController_PutOne_Should_SendNotFound_Map()
        {
            //		arrange
            var account = GetAccount();

            var map = account.Maps.Last();

            account.Maps.Clear();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(map.AccountId, map.Id, map).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}