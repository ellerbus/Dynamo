using Dynamo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Tests.Core
{
    [TestClass]
    public class GeneratorColumnTests
    {
        [TestMethod]
        public void GeneratorColumn_PropertyName_Test_Underscored()
        {
            GeneratorColumn gc = new GeneratorColumn("property_name");

            Assert.AreEqual("PropertyName", gc.PropertyName);
        }

        [TestMethod]
        public void GeneratorColumn_PropertyName_Test_Camel()
        {
            GeneratorColumn gc = new GeneratorColumn("propertyName");

            Assert.AreEqual("PropertyName", gc.PropertyName);
        }

        [TestMethod]
        public void GeneratorColumn_ParameterName_Test_Underscored()
        {
            GeneratorColumn gc = new GeneratorColumn("parameter_name");

            Assert.AreEqual("parameterName", gc.ParameterName);
        }

        [TestMethod]
        public void GeneratorColumn_ParameterName_Test_Camel()
        {
            GeneratorColumn gc = new GeneratorColumn("parameterName");

            Assert.AreEqual("parameterName", gc.ParameterName);
        }
    }
}
