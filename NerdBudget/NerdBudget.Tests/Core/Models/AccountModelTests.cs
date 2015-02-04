using System;
using Augment;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class AccountModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Account_Id_Should_UpperCase()
        {
            var actual = new Account() { Id = "aa" };

            Assert.AreEqual("AA", actual.Id);
        }

        [TestMethod]
        public void Account_Name_Should_UpperCase()
        {
            var actual = new Account() { Name = "aa" };

            Assert.AreEqual("AA", actual.Name);
        }

        [TestMethod]
        public void Account_StartedAt_Should_StartAtBegOfMonth()
        {
            var actual = new Account() { StartedAt = DateTime.UtcNow };

            Assert.AreEqual(DateTime.UtcNow.BeginningOfMonth(), actual.StartedAt);
        }


        //[TestMethod]
        //public void Account_Name_Should_DoSomething()
        //{
        //	var actual = new Account() { Name = "aa" };
        //
        //	Assert.AreEqual("aa", actual.Name);
        //}


        //[TestMethod]
        //public void Account_Type_Should_DoSomething()
        //{
        //	var actual = new Account() { Type = "aa" };
        //
        //	Assert.AreEqual("aa", actual.Type);
        //}


        //[TestMethod]
        //public void Account_StartedAt_Should_DoSomething()
        //{
        //	var actual = new Account() { StartedAt = new DateTime(2000, 1, 1) };
        //
        //	Assert.AreEqual(new DateTime(2000, 1, 1), actual.StartedAt);
        //}


        [TestMethod]
        public void Account_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Account() { CreatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.CreatedAt);
        }


        [TestMethod]
        public void Account_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Account() { UpdatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.UpdatedAt);
        }


        #endregion

        #region Amounts

        [TestMethod]
        public void Account_Should_CalculateZeroAmounts()
        {
            //  arrange
            var account = Builder<Account>.CreateNew().Build();

            //  act

            //  assert
            Assert.AreEqual(0, account.WeeklyAmount);
            Assert.AreEqual(0, account.MonthlyAmount);
            Assert.AreEqual(0, account.YearlyAmount);
        }

        [TestMethod]
        public void Account_Should_CalculateSummaryAmounts()
        {
            //  arrange
            var account = Builder<Account>.CreateNew().Build();

            var categories = Builder<Category>.CreateListOfSize(2)
                .Build();

            account.Categories.AddRange(categories);

            categories[0].Multiplier = 1;
            categories[1].Multiplier = -1;

            int amount = 100;

            foreach (var c in account.Categories)
            {
                var budgets = Builder<Budget>.CreateListOfSize(2).Build();

                foreach (var b in budgets)
                {
                    b.Id = c.Id + b.Id;

                    c.Budgets.Add(b);

                    b.Amount = amount;

                    b.BudgetFrequency = BudgetFrequencies.M1;

                    amount -= 10;
                }
            }

            //  expected budget values
            double variance = (100 + 90) + (-80 + -70);

            Assert.AreEqual((variance * 12 / 52).RoundTo(2), account.WeeklyAmount.RoundTo(2));
            Assert.AreEqual(variance, account.MonthlyAmount);
            Assert.AreEqual(variance * 12, account.YearlyAmount);

        }

        #endregion
    }
}