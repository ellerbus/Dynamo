using System;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;

namespace NerdBudget.Tests.Core.Models
{
    [TestClass]
    public class CategoryModelTests
    {
        #region Property Tests

        [TestMethod]
        public void Category_AccountId_Should_UpperCase()
        {
            var actual = new Category() { AccountId = "aa" };

            Assert.AreEqual("AA", actual.AccountId);
        }

        [TestMethod]
        public void Category_Id_Should_UpperCase()
        {
            var actual = new Category() { Id = "aa" };

            Assert.AreEqual("AA", actual.Id);
        }

        [TestMethod]
        public void Category_Name_Should_UpperCase()
        {
            var actual = new Category() { Name = "aa" };

            Assert.AreEqual("AA", actual.Name);
        }


        [TestMethod]
        public void Category_Multiplier_Should_BePosOne()
        {
            var actual = new Category() { Multiplier = 9 };

            Assert.AreEqual(1, actual.Multiplier);
        }


        [TestMethod]
        public void Category_Multiplier_Should_BeNegOne()
        {
            var actual = new Category() { Multiplier = -9 };

            Assert.AreEqual(-1, actual.Multiplier);
        }


        //[TestMethod]
        //public void Category_Sequence_Should_DoSomething()
        //{
        //	var actual = new Category() { Sequence = 9 };
        //
        //	Assert.AreEqual(9, actual.Sequence);
        //}


        [TestMethod]
        public void Category_CreatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Category() { CreatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.CreatedAt);
        }


        [TestMethod]
        public void Category_UpdatedAt_Should_BeUtc()
        {
            var dt = new DateTime(2000, 1, 1);

            var actual = new Category() { UpdatedAt = dt };

            Assert.AreEqual(dt.ToUniversalTime(), actual.UpdatedAt);
        }


        #endregion

        #region Collection Tests

        [TestMethod]
        public void Category_Account_Should_AssignAccountId()
        {
            var account = Builder<Account>.CreateNew().Build();

            var collection = new CategoryCollection(account, new Category[0]);

            var actual = Builder<Category>.CreateNew().Build();

            collection.Add(actual);

            Assert.AreEqual(account.Id, actual.AccountId);
        }

        #endregion
    }
}