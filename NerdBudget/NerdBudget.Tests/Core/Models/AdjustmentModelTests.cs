using System;
using Augment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class AdjustmentModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Adjustment_AccountId_Should_UpperCase()
        {
            var actual = new Adjustment() { AccountId = "aa" };

            Assert.AreEqual("AA", actual.AccountId);
        }

        [TestMethod]
        public void Adjustment_BudgetId_Should_UpperCase()
        {
            var actual = new Adjustment() { BudgetId = "aa" };

            Assert.AreEqual("AA", actual.BudgetId);
        }

        [TestMethod]
        public void Adjustment_Id_Should_UpperCase()
        {
            var actual = new Adjustment() { Id = "aa" };

            Assert.AreEqual("AA", actual.Id);
        }

        [TestMethod]
        public void Adjustment_Name_Should_UpperCase()
        {
            var actual = new Adjustment() { Name = "aa" };

            Assert.AreEqual("AA", actual.Name);
        }


        [TestMethod]
        public void Adjustment_Date_Should_DoSomething()
        {
            var actual = new Adjustment() { Date = DateTime.Now };

            Assert.AreEqual(DateTime.Now.EnsureUtc().BeginningOfDay(), actual.Date);
        }


        [TestMethod]
        public void Adjustment_Date_ShouldNot_DoSomething()
        {
            var actual = new Adjustment() { Date = null };

            Assert.IsNull(actual.Date);
        }


        //[TestMethod]
        //public void Adjustment_Amount_Should_DoSomething()
        //{
        //	var actual = new Adjustment() { Amount = 9 };
        //
        //	Assert.AreEqual(9, actual.Amount);
        //}


        [TestMethod]
        public void Adjustment_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Adjustment() { CreatedAt = dt };

            Assert.AreEqual(dt.EnsureUtc(), actual.CreatedAt);
        }


        [TestMethod]
        public void Adjustment_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Adjustment() { UpdatedAt = dt };

            Assert.AreEqual(dt.EnsureUtc(), actual.UpdatedAt);
        }


        #endregion
    }
}