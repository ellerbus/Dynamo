using System;
using Dynamo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Tests.Core
{
    [TestClass]
    public class DotLiquidFiltersTests
    {
        [TestMethod]
        public void DotLiquidFilters_Should_LPad(string input, int pad)
        {
            Assert.AreEqual("   abc", DotLiquidFilters.LPad("abc", 6));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_RPad(string input, int pad)
        {
            Assert.AreEqual("abc   ", DotLiquidFilters.RPad("abc", 6));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Pascal(string input)
        {
            Assert.AreEqual("AbcDef", DotLiquidFilters.Pascal("abc_def"));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Camel(string input)
        {
            Assert.AreEqual("abcDef", DotLiquidFilters.Camel("abc_def"));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Label(string input)
        {
            Assert.AreEqual("Abc Def", DotLiquidFilters.Label("abc_def"));
        }

        [TestMethod]
        public void DotLiquidFilters_Should_Title(string input)
        {
            Assert.AreEqual("Abc_def", DotLiquidFilters.Title("abc_def"));
        }
    }
}
