using System;
using Augment;
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
    }
}