using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMoq;
using FizzWare.NBuilder;
using FluentValidation;
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
    public class BudgetsControllerTests : ControllerTestHelper<Budget>
    {
        #region Helpers/Test Initializers

        private AutoMoqer Mocker { get; set; }
        private Mock<IBudgetService> MockService { get; set; }
        private Mock<IAccountService> MockAccountService { get; set; }
        private BudgetsController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<BudgetsController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<IBudgetService>();

            MockAccountService = Mocker.GetMock<IAccountService>();
        }

        private Account GetAccount()
        {
            var account = Builder<Account>.CreateNew().Build();

            var categories = Builder<Category>.CreateListOfSize(10)
                .Build();

            account.Categories.AddRange(categories);

            foreach (var c in account.Categories)
            {
                var budgets = Builder<Budget>.CreateListOfSize(3).Build();

                foreach (var b in budgets)
                {
                    b.Id = c.Id + b.Id;

                    c.Budgets.Add(b);
                }
            }

            return account;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Category>("Id,AccountId,Name")
                .AddPayload<Budget>("Id,AccountId,CategoryId,Name,StartDate,EndDate,Amount,Frequency")
                .ToSettings();
        }

        #endregion

        #region Tests - Post One

        [TestMethod]
        public void BudgetsController_PostOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var budget = Builder<Budget>.CreateNew()
                .With(x => x.CategoryId = category.Id)
                .Build();

            MockService.Setup(x => x.Insert(category, budget));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Post(account.Id, budget).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PostOne_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var budget = Builder<Budget>.CreateNew()
                .With(x => x.CategoryId = category.Id)
                .Build();

            MockService.Setup(x => x.Insert(category, budget)).Throws(new ValidationException(ValidationFailure.Errors));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Post(account.Id, budget).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PostOne_Should_SendBadRequest_MissingCategory()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var budget = Builder<Budget>.CreateNew().Build();

            budget.CategoryId = null;

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Post(account.Id, budget).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void BudgetsController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            MockService.Setup(x => x.Update(budget));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, budget.Id, budget).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PutOne_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            MockService.Setup(x => x.Update(budget)).Throws(new ValidationException(ValidationFailure.Errors));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, budget.Id, budget).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PutOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount();

            var budget = Builder<Budget>.CreateNew().With(x => x.Id = "X").Build();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Put(account.Id, budget.Id, budget).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PutSequences_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var ids = category.Budgets.Select(x => x.Id).OrderBy(x => x).ToList();

            var bs = new BudgetsController.Seq { Sequence = ids.ToArray() };

            MockService.Setup(x => x.Update(Any.Enumerable));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.PutSequences(account.Id, bs).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            for (int i = 0; i < ids.Count; i++)
            {
                string id = ids[i];

                var cat = category.Budgets[id];

                Assert.AreEqual(i * 10, cat.Sequence);
            }

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PutSequences_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var ids = category.Budgets.Select(x => x.Id).OrderBy(x => x).ToList();

            var bs = new BudgetsController.Seq { Sequence = ids.ToArray() };

            MockService.Setup(x => x.Update(Any.Enumerable)).Throws(new ValidationException(ValidationFailure.Errors));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.PutSequences(account.Id, bs).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_PutSequences_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount();

            var category = account.Categories.Last();

            var ids = category.Budgets.Select(x => x.Id).OrderBy(x => x).ToList();

            var bs = new BudgetsController.Seq { Sequence = ids.ToArray() };

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.PutSequences(account.Id, bs).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Delete One

        [TestMethod]
        public void BudgetsController_DeleteOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount();

            var budget = account.Categories.Last().Budgets.Last();

            MockService.Setup(x => x.Delete(budget));

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Delete(account.Id, budget.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void BudgetsController_DeleteOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount();

            var budget = Builder<Category>.CreateNew().With(x => x.Id = "X").Build();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Delete(account.Id, budget.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}