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
        public void CategoryCollection_Should_AssignAccountId()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var category = Builder<Category>.CreateNew().Build();

            //  act
            account.Categories.Add(category);

            //  assert
            Assert.AreEqual(account.Id, category.AccountId);
        }

        [TestMethod]
        public void CategoryCollection_Should_Resort()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var c1 = Builder<Category>.CreateNew()
                .With(x => x.Id = "C1")
                .With(x => x.Sequence = 99)
                .Build();

            var c2 = Builder<Category>.CreateNew()
                .With(x => x.Id = "C2")
                .With(x => x.Sequence = 11)
                .Build();

            //  act
            account.Categories.Add(c1);
            account.Categories.Add(c2);

            account.Categories.Resort();

            //  assert
            Assert.AreEqual(c2, account.Categories[0]);
            Assert.AreEqual(c1, account.Categories[1]);
        }

        [TestMethod]
        public void CategoryCollection_Should_LookupUsingId()
        {
            //  assign 
            var account = Builder<Account>.CreateNew().Build();

            var c1 = Builder<Category>.CreateNew().Build();

            //  act
            account.Categories.Add(c1);

            //  assert
            Assert.AreEqual(c1, account.Categories[c1.Id]);
        }

        #endregion
    }
}