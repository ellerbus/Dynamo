using System;
using Augment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core;

namespace NerdBudget.Tests.Core
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void DateTime_Should_CalculateCorrectMonth()
        {
            var dt = new DateTime(2014, 12, 1);

            for (int i = 0; i < 2; i++)
            {
                dt = dt.AddYears(1);

                Assert.AreEqual(1, dt.ToMonthDate().Day, "Monthly Misfire for {0:MM/dd/yyyy}".FormatArgs(dt));
            }
        }

        [TestMethod]
        public void DateTime_Should_CalculateCorrectWeek()
        {
            var dt = new DateTime(2014, 6, 1);

            for (int i = 0; i < 75; i++)
            {
                dt = dt.AddDays(7);

                Assert.AreEqual(DayOfWeek.Friday, dt.ToWeekDate().DayOfWeek, "Weekly Misfire for {0:MM/dd/yyyy}".FormatArgs(dt));
            }
        }
    }
}
