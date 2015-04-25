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
    public class AdjustmentsControllerTests : ControllerTestHelper<Adjustment>
    {
        #region Helpers/Test Initializers

        private AutoMoqer Mocker { get; set; }
        private Mock<IAdjustmentService> MockService { get; set; }
        private Mock<IAccountService> MockAccountService { get; set; }
        private AdjustmentsController SubjectUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mocker = new AutoMoqer();

            SubjectUnderTest = Mocker.Create<AdjustmentsController>();

            SubjectUnderTest.Request = new HttpRequestMessage();
            SubjectUnderTest.Configuration = new HttpConfiguration();

            MockService = Mocker.GetMock<IAdjustmentService>();

            MockAccountService = Mocker.GetMock<IAccountService>();
        }

        private Account GetAccount(bool includeAdjustments)
        {
            var account = Builder<Account>.CreateNew().Build();

            var categories = Builder<Category>.CreateListOfSize(3)
                .Build();

            account.Categories.AddRange(categories);

            foreach (var c in account.Categories)
            {
                var budgets = Builder<Budget>.CreateListOfSize(3).Build();

                foreach (var b in budgets)
                {
                    b.Id = c.Id + b.Id;

                    c.Budgets.Add(b);

                    if (includeAdjustments)
                    {
                        var adjustments = Builder<Adjustment>.CreateListOfSize(3).Build();

                        foreach (var a in adjustments)
                        {
                            a.Id = b.Id + a.Id;

                            b.Adjustments.Add(a);
                        }
                    }
                }
            }

            return account;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddBasicPayload<Adjustment>()
                .ToSettings();
        }

        #endregion

        #region Tests - Get Many

        [TestMethod]
        public void AdjustmentsController_GetMany_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var budget = account.Categories.First().Budgets.First();

            var adjustment = budget.Adjustments.Last();

            var settings = GetPayloadSettings();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.GetAdjustments(adjustment.AccountId, adjustment.BudgetId, adjustment.Date.Value).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(budget.Adjustments, settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_GetMany_Should_SendEmptySet()
        {
            //		arrange
            var account = GetAccount(true);

            var budget = account.Categories.First().Budgets.First();

            var adjustment = budget.Adjustments.Last();

            var settings = GetPayloadSettings();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.GetAdjustments(adjustment.AccountId, adjustment.BudgetId, adjustment.Date.Value.AddDays(7)).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            msg.Content.AssertJsonObjectEquality(new Adjustment[0], settings);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_GetMany_Should_SendNotFound_Account()
        {
            //		arrange
            var account = GetAccount(true);

            var adjustment = account.Categories.Last().Budgets.Last().Adjustments.Last();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.GetAdjustments(adjustment.AccountId, adjustment.Id, adjustment.Date.Value).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Post One

        [TestMethod]
        public void AdjustmentsController_PostOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(false);

            var budget = account.Categories.Last().Budgets.Last();

            var adjustment = Builder<Adjustment>.CreateNew().Build();

            adjustment.BudgetId = budget.Id;

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Insert(budget, adjustment));

            //		act
            var msg = SubjectUnderTest.Post(account.Id, adjustment).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_PostOne_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount(false);

            var budget = account.Categories.Last().Budgets.Last();

            var adjustment = Builder<Adjustment>.CreateNew().Build();

            adjustment.BudgetId = budget.Id;

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Insert(budget, adjustment)).Throws(new ValidationException(new[] { new ValidationFailure("", "") }));

            //		act
            var msg = SubjectUnderTest.Post(account.Id, adjustment).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_PostOne_Should_SendBadRequest_MissingBudget()
        {
            //		arrange
            var account = GetAccount(false);

            var budget = account.Categories.Last().Budgets.Last();

            var adjustment = Builder<Adjustment>.CreateNew().Build();

            adjustment.BudgetId = null;

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            //		act
            var msg = SubjectUnderTest.Post(account.Id, adjustment).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Put One

        [TestMethod]
        public void AdjustmentsController_PutOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var adjustment = account.Categories.Last().Budgets.Last().Adjustments.Last();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Update(adjustment));

            //		act
            var msg = SubjectUnderTest.Put(adjustment.AccountId, adjustment.Id, adjustment).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_PutOne_Should_SendBadRequest()
        {
            //		arrange
            var account = GetAccount(true);

            var adjustment = account.Categories.Last().Budgets.Last().Adjustments.Last();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Update(adjustment)).Throws(new ValidationException(new[] { new ValidationFailure("", "") }));

            //		act
            var msg = SubjectUnderTest.Put(adjustment.AccountId, adjustment.Id, adjustment).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.BadRequest);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_PutOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount(true);

            var adjustment = account.Categories.Last().Budgets.Last().Adjustments.Last();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Put(adjustment.AccountId, adjustment.Id, adjustment).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion

        #region Tests - Delete One

        [TestMethod]
        public void AdjustmentsController_DeleteOne_Should_SendOk()
        {
            //		arrange
            var account = GetAccount(true);

            var adjustment = account.Categories.Last().Budgets.Last().Adjustments.Last();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(account);

            MockService.Setup(x => x.Delete(adjustment));

            //		act
            var msg = SubjectUnderTest.Delete(adjustment.AccountId, adjustment.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.IsSuccessStatusCode);

            MockService.VerifyAll();
        }

        [TestMethod]
        public void AdjustmentsController_DeleteOne_Should_SendNotFound()
        {
            //		arrange
            var account = GetAccount(true);

            var adjustment = account.Categories.Last().Budgets.Last().Adjustments.Last();

            MockAccountService.Setup(x => x.Get(account.Id)).Returns(null as Account);

            //		act
            var msg = SubjectUnderTest.Delete(adjustment.AccountId, adjustment.Id).ToMessage();

            //		assert
            Assert.IsTrue(msg.StatusCode == HttpStatusCode.NotFound);

            MockService.VerifyAll();
        }

        #endregion
    }
}