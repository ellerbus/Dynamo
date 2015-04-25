using System;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;
using Augment;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class BudgetModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Budget_AccountId_Should_UpperCase()
        {
            var actual = new Budget() { AccountId = "aa" };

            Assert.AreEqual("AA", actual.AccountId);
        }

        [TestMethod]
        public void Budget_CategoryId_Should_UpperCase()
        {
            var actual = new Budget() { CategoryId = "aa" };

            Assert.AreEqual("AA", actual.CategoryId);
        }

        [TestMethod]
        public void Budget_Id_Should_UpperCase()
        {
            var actual = new Budget() { Id = "aa" };

            Assert.AreEqual("AA", actual.Id);
        }

        [TestMethod]
        public void Budget_Name_Should_UpperCase()
        {
            var actual = new Budget() { Name = "aa" };

            Assert.AreEqual("AA", actual.Name);
        }


        //[TestMethod]
        //public void Budget_Frequency_Should_DoSomething()
        //{
        //	var actual = new Budget() { Frequency = "aa" };
        //
        //	Assert.AreEqual("aa", actual.Frequency);
        //}


        //[TestMethod]
        //public void Budget_Sequence_Should_DoSomething()
        //{
        //	var actual = new Budget() { Sequence = 9 };
        //
        //	Assert.AreEqual(9, actual.Sequence);
        //}


        //[TestMethod]
        //public void Budget_StartDate_Should_DoSomething()
        //{
        //	var actual = new Budget() { StartDate =  };
        //
        //	Assert.AreEqual(, actual.StartDate);
        //}


        //[TestMethod]
        //public void Budget_EndDate_Should_DoSomething()
        //{
        //	var actual = new Budget() { EndDate =  };
        //
        //	Assert.AreEqual(, actual.EndDate);
        //}


        //[TestMethod]
        //public void Budget_Amount_Should_DoSomething()
        //{
        //	var actual = new Budget() { Amount = 9 };
        //
        //	Assert.AreEqual(9, actual.Amount);
        //}


        [TestMethod]
        public void Budget_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Budget() { CreatedAt = dt };

            Assert.AreEqual(dt.EnsureUtc(), actual.CreatedAt);
        }


        [TestMethod]
        public void Budget_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Budget() { UpdatedAt = dt };

            Assert.AreEqual(dt.EnsureUtc(), actual.UpdatedAt);
        }


        #endregion

        #region Collection Tests

        [TestMethod]
        public void BudgetCollection_Should_AssignAccountId()
        {
            var account = Builder<Account>.CreateNew().With(x => x.Id = "AB").Build();

            var categories = Builder<Category>.CreateListOfSize(10).Build();

            account.Categories.AddRange(categories);

            var category = categories.Last();

            var budget = Builder<Budget>.CreateNew().Build();

            category.Budgets.Add(budget);

            Assert.AreEqual(category.AccountId, budget.AccountId);
            Assert.AreEqual(category.Id, budget.CategoryId);
        }

        [TestMethod]
        public void BudgetCollection_Should_Resort()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().With(x => x.Id = "AB").Build();

            var categories = Builder<Category>.CreateListOfSize(10).Build();

            account.Categories.AddRange(categories);

            var category = categories.Last();

            var b1 = Builder<Budget>.CreateNew()
                .With(x => x.Id = "B1")
                .With(x => x.Sequence = 99)
                .Build();

            var b2 = Builder<Budget>.CreateNew()
                .With(x => x.Id = "B2")
                .With(x => x.Sequence = 11)
                .Build();

            //  act
            category.Budgets.Add(b1);
            category.Budgets.Add(b2);

            category.Budgets.Resort();

            //  assert
            Assert.AreEqual(b2, category.Budgets[0]);
            Assert.AreEqual(b1, category.Budgets[1]);
        }

        [TestMethod]
        public void BudgetCollection_Should_LookupUsingId()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().With(x => x.Id = "AB").Build();

            var categories = Builder<Category>.CreateListOfSize(10).Build();

            account.Categories.AddRange(categories);

            var category = categories.Last();

            var b1 = Builder<Budget>.CreateNew().Build();

            //  act
            category.Budgets.Add(b1);

            //  assert
            Assert.AreEqual(b1, category.Budgets[b1.Id]);
        }

        #endregion

        #region Calculations

        [TestMethod]
        public void Budget_Should_CalculateAmountsCorrectly()
        {
            const double amount = 100;

            foreach (BudgetFrequencies b in Enum.GetValues(typeof(BudgetFrequencies)))
            {
                Budget bgt = new Budget() { BudgetFrequency = b, Amount = amount };

                Assert.AreEqual(bgt.MonthlyAmount * 12.0, bgt.YearlyAmount);
                Assert.AreEqual(bgt.MonthlyAmount * 12.0 / 52.0, bgt.WeeklyAmount);

                switch (b)
                {
                    case BudgetFrequencies.W1:
                        //case BudgetFrequencies.XM:
                        //case BudgetFrequencies.XT:
                        //case BudgetFrequencies.XW:
                        //case BudgetFrequencies.XR:
                        //case BudgetFrequencies.XF:
                        //case BudgetFrequencies.XS:
                        //case BudgetFrequencies.XN:
                        Assert.AreEqual(amount * 52.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.W2:
                        Assert.AreEqual(amount * 52.0 / 2.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.W3:
                        Assert.AreEqual(amount * 52.0 / 3.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.W4:
                        Assert.AreEqual(amount * 52.0 / 4.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.W5:
                        Assert.AreEqual(amount * 52.0 / 5.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.W6:
                        Assert.AreEqual(amount * 52.0 / 6.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.M1:
                        Assert.AreEqual(amount, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.M2:
                        Assert.AreEqual(amount * 6.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.MT:
                        Assert.AreEqual(amount * 24.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.Q1:
                        Assert.AreEqual(amount * 4.0 / 12.0, bgt.MonthlyAmount);
                        break;

                    case BudgetFrequencies.Y1:
                        Assert.AreEqual(amount / 12.0, bgt.MonthlyAmount);
                        break;
                }
            }
        }

        #endregion
    }
}