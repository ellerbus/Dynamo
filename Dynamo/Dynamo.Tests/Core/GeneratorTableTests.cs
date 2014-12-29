using System;
using Dynamo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Tests.Core
{
    [TestClass]
    public class GeneratorTableTests
    {
        [TestMethod]
        public void GeneratorTable_PropertyName_Test_Underscored()
        {
            GeneratorTable gt = new GeneratorTable("TABLE_NAME");

            Assert.AreEqual("TableName", gt.ClassName);
        }

        [TestMethod]
        public void GeneratorTable_PropertyName_Test_Pascal()
        {
            GeneratorTable gt = new GeneratorTable("TableName");

            Assert.AreEqual("TableName", gt.ClassName);
        }
    }
}
