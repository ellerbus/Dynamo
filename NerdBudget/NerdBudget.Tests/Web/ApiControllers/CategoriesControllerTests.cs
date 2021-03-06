using System.Linq;
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
    public class CategoriesControllerTests : ControllerTestHelper<Category>
    {
        #region Helpers/Test Initializers

        private const int CountOfCategories = 10;

        private AutoMoqer Mocker { get; set; }
        private Mock<ICategoryService> MockService { get; set; }
        private Mock<IAccountService> MockAccountService { get; set; }
        private CategoriesController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<CategoriesController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<ICategoryService>();

            MockAccountService = Mocker.GetMock<IAccountService>();
        }

        private Account GetAccount()
        {
            var account = Builder<Account>.CreateNew().With(x => x.Id = "x").Build();

            var categories = Builder<Category>.CreateListOfSize(CountOfCategories).Build();

            account.Categories.AddRange(categories);

            return account;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Category>("Id,Name,Multiplier")
                .ToSettings();
        }

        #endregion

        #region Tests - Post One

        [TestMethod]
        public void CategoriesController_PostOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var category = Builder<Category>.CreateNew().Build();

            var settings = GetPayloadSettings();

            MockService.Setup(x => x.Insert(account, category));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Post(account.Id, category).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(category, settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_PostOne_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount();

            var category = Builder<Category>.CreateNew().Build();

            MockService.Setup(x => x.Insert(account, category)).Throws(new ValidationException(ValidationFailure.Errors));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Post(account.Id, category).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void CategoriesController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var settings = GetPayloadSettings();

            MockService.Setup(x => x.Update(category));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, category.Id, category).ToMessage();

            msg.Content.AssertJsonObjectEquality(category, settings);

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_PutOne_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            MockService.Setup(x => x.Update(category)).Throws(new ValidationException(ValidationFailure.Errors));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, category.Id, category).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_PutOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount();

            var category = Builder<Category>.CreateNew().With(x => x.Id = "X").Build();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, category.Id, category).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_PutSequences_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var ids = account.Categories.Select(x => x.Id).OrderBy(x => x).ToList();

            var cs = new CategoriesController.Seq { Sequence = ids.ToArray() };

            MockService.Setup(x => x.Update(Any.Enumerable));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.PutSequences(account.Id, cs).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            for (int i = 0; i < ids.Count; i++)
            {
                string id = ids[i];

                var cat = account.Categories[id];

                Assert.AreEqual(i * 10, cat.Sequence);
            }

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_PutSequences_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount();

            var ids = account.Categories.Select(x => x.Id).OrderBy(x => x).ToList();

            var cs = new CategoriesController.Seq { Sequence = ids.ToArray() };

            MockService.Setup(x => x.Update(Any.Enumerable)).Throws(new ValidationException(ValidationFailure.Errors));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.PutSequences(account.Id, cs).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_PutSequences_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount();

            var ids = account.Categories.Select(x => x.Id).OrderBy(x => x).ToList();

            var cs = new CategoriesController.Seq { Sequence = ids.ToArray() };

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.PutSequences(account.Id, cs).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Delete One

        [TestMethod]
        public void CategoriesController_DeleteOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            MockService.Setup(x => x.Delete(category));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Delete(account.Id, category.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void CategoriesController_DeleteOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount();

            var category = Builder<Category>.CreateNew().With(x => x.Id = "X").Build();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Delete(account.Id, category.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}