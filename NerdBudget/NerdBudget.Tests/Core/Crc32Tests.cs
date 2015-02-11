using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core;

namespace NerdBudget.Tests.Core
{
    [TestClass]
    public class Crc32Tests
    {
        [TestMethod]
        public void Crc32_Should_CalculateCorrectly()
        {
            var a1 = Crc32.Hash("123456789");

            Assert.AreEqual("cbf43926", a1);

            var a2 = Crc32.Hash("987654321");

            Assert.AreEqual("15f0201", a2);
        }
    }
}
