using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;
using Augment;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class MemberModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Member_Name_Should_LowerCase()
        {
            var actual = new Member() { Name = "AA" };

            Assert.AreEqual("aa", actual.Name);
        }


        //[TestMethod]
        //public void Member_Password_Should_DoSomething()
        //{
        //	var actual = new Member() { Password = "aa" };
        //
        //	Assert.AreEqual("aa", actual.Password);
        //}


        //[TestMethod]
        //public void Member_LoggedInAt_Should_DoSomething()
        //{
        //	var actual = new Member() { LoggedInAt = new DateTime(2000, 1, 1) };
        //
        //	Assert.AreEqual(new DateTime(2000, 1, 1), actual.LoggedInAt);
        //}


        [TestMethod]
        public void Member_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Member() { CreatedAt = dt };

            Assert.AreEqual(dt.EnsureUtc(), actual.CreatedAt);
        }


        [TestMethod]
        public void Member_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Member() { UpdatedAt = dt };

            Assert.AreEqual(dt.EnsureUtc(), actual.UpdatedAt);
        }


        #endregion
    }
}