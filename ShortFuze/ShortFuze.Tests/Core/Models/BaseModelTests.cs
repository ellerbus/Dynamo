using System;
using Augment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortFuze.Tests.Core.Models
{
    [TestClass]
    public class BaseModelTests<T> where T : class, new()
    {
        [TestMethod]
        public void T_CreatedAt_Should_EnsureUtc()
        {
            var item = new T();

            if (ReflectionHelper.HasProperty(item, "CreatedAt"))
            {
                DateTime dt = DateTime.Now;

                ReflectionHelper.SetValueOfProperty(item, "CreatedAt", dt);

                DateTime actual = (DateTime)ReflectionHelper.GetValueOfProperty(item, "CreatedAt");

                Assert.AreEqual(dt.EnsureUtc(), actual, "{0}.CreatedAt".FormatArgs(typeof(T).Name));
            }
        }


        [TestMethod]
        public void T_CreatedBy_ShouldBe_LowerCased()
        {
            var item = new T();

            if (ReflectionHelper.HasProperty(item, "CreatedBy"))
            {
                string id = "AA";

                ReflectionHelper.SetValueOfProperty(item, "CreatedBy", id);

                string actual = (string)ReflectionHelper.GetValueOfProperty(item, "CreatedBy");

                Assert.AreEqual("aa", actual, "{0}.CreatedBy".FormatArgs(typeof(T).Name));
            }
        }


        [TestMethod]
        public void T_UpdatedAt_Should_EnsureUtc()
        {
            var item = new T();

            if (ReflectionHelper.HasProperty(item, "UpdatedAt"))
            {
                DateTime dt = DateTime.Now;

                ReflectionHelper.SetValueOfProperty(item, "UpdatedAt", dt);

                DateTime? actual = (DateTime?)ReflectionHelper.GetValueOfProperty(item, "UpdatedAt");

                Assert.AreEqual(dt.EnsureUtc(), actual.Value, "{0}.UpdatedAt".FormatArgs(typeof(T).Name));
            }
        }


        [TestMethod]
        public void T_UpdatedAt_Should_EnsureNull()
        {
            var item = new T();

            if (ReflectionHelper.HasProperty(item, "UpdatedAt"))
            {
                ReflectionHelper.SetValueOfProperty(item, "UpdatedAt", null);

                Assert.IsNull(ReflectionHelper.GetValueOfProperty(item, "UpdatedAt"));
            }
        }


        [TestMethod]
        public void T_UpdatedBy_ShouldBe_LowerCased()
        {
            var item = new T();

            if (ReflectionHelper.HasProperty(item, "UpdatedBy"))
            {
                string id = "AA";

                ReflectionHelper.SetValueOfProperty(item, "UpdatedBy", id);

                string actual = (string)ReflectionHelper.GetValueOfProperty(item, "UpdatedBy");

                Assert.AreEqual("aa", actual, "{0}.UpdatedBy".FormatArgs(typeof(T).Name));
            }
        }
    }

}
