using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMoq;
using FizzWare.NBuilder;
using FluentValidation;
using FluentValidation.Results;
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
    public class AccountsControllerTests : ControllerTestHelper<Account>
    {
        #region Helpers/Test Initializers

        private AutoMoqer Mocker { get; set; }
        private Mock<IAccountService> MockService { get; set; }
        private AccountsController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<AccountsController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<IAccountService>();
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .ToSettings();
        }

        #endregion

        #region Tests - Post One

        [TestMethod]
        public void AccountsController_PostOne_Should_SendOk()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            var settings = GetPayloadSettings();

            MockService.Setup(x => x.Insert(account));

            //		act
            var msg = SubjectUnderTest.Post(account).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(account, settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AccountsController_PostOne_Should_SendBadRequest()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            MockService.Setup(x => x.Insert(account)).Throws(new ValidationException(ValidationFailure.Errors));

            //		act
            var msg = SubjectUnderTest.Post(account).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void AccountsController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            var settings = GetPayloadSettings();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Update(account));

            //		act
            var msg = SubjectUnderTest.Put(account.Id, account).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(account, settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AccountsController_PutOne_Should_SendBadRequest()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Update(account)).Throws(new ValidationException(ValidationFailure.Errors));

            //		act
            var msg = SubjectUnderTest.Put(account.Id, account).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AccountsController_PutOne_Should_SendNotFound()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, account).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Delete One

        [TestMethod]
        public void AccountsController_DeleteOne_Should_SendOk()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            MockService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Delete(account));

            //		act
            var msg = SubjectUnderTest.Delete(account.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AccountsController_DeleteOne_Should_SendNotFound()
        {
            //		arrange
            var account = Builder<Account>.CreateNew().Build();

            MockService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Delete(account.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}