using System;
using Dynamo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Tests.Core
{
    [TestClass]
    public class DotLiquidFiltersTests
    {
        [TestMethod]
        public void DotLiquidFilters_Should_LPad()
        {
            Assert.AreEqual("   abc", DotLiquidFilters.LPad("abc", 6));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_RPad()
        {
            Assert.AreEqual("abc   ", DotLiquidFilters.RPad("abc", 6));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Pascal()
        {
            Assert.AreEqual("AbcDef", DotLiquidFilters.Pascal("abc_def"));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Camel()
        {
            Assert.AreEqual("abcDef", DotLiquidFilters.Camel("abc_def"));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Label()
        {
            Assert.AreEqual("Abc Def", DotLiquidFilters.Label("abc_def"));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Title()
        {
            Assert.AreEqual("Abc_def", DotLiquidFilters.Title("abc_def"));
        }
    }
}
