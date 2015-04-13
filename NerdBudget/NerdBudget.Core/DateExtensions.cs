using System;
using Augment;

namespace NerdBudget.Core
{
    public static class DateExtensions
    {
        /// <summary>
        /// The Starting point with which a budget week starts
        /// </summary>
        public const DayOfWeek StartingDayOfWeek = DayOfWeek.Friday;
        /// <summary>
        /// The Starting point with which a budget week starts
        /// </summary>
        public const DayOfWeek EndingDayOfWeek = DayOfWeek.Thursday;

        public static DateTime ToMonthlyBudgetDate(this DateTime dt)
        {
            return dt.BeginningOfMonth().Date;
        }

        public static DateTime ToWeeklyBudgetDate(this DateTime dt)
        {
            return dt.BeginningOfWeek(StartingDayOfWeek).Date;
        }

        /// <summary>
        /// Display range for a budget week to view
        /// </summary>
        public static Range<DateTime> ToMonthlyBudgetRange(this DateTime dt)
        {
            Range<DateTime> range = dt.ToWeeklyBudgetRange();

            DateTime beg = range.Start.AddDays(-14);

            DateTime end = range.End.AddDays(14);

            return new Range<DateTime>(beg, end);
        }

        public static Range<DateTime> ToWeeklyBudgetRange(this DateTime dt)
        {
            DateTime beg = dt.GetDayOfWeek(StartingDayOfWeek, -1);

            DateTime end = beg.GetDayOfWeek(EndingDayOfWeek, 1);

            Range<DateTime> range = new Range<DateTime>(beg, end);

            return range;
        }

        private static DateTime GetDayOfWeek(this DateTime dt, DayOfWeek dow, int dir)
        {
            dt = dt.Date;

            while (dt.DayOfWeek != dow)
            {
                dt = dt.AddDays(dir);
            }

            return dt;
        }



        //public static class DateExtensions
        //{
        //    public static string ToShortSuffix(this DateTime date)
        //    {
        //        string suffix = "th";

        //        switch (date.Day)
        //        {
        //            case 1:
        //            case 21:
        //            case 31:
        //                suffix = "st";
        //                break;
        //            case 2:
        //            case 22:
        //                suffix = "nd";
        //                break;
        //            case 3:
        //            case 23:
        //                suffix = "rd";
        //                break;
        //        }

        //        return date.ToString("MMM d") + suffix;
        //    }

        //    public static int Weeks(this Range<DateTime> range)
        //    {
        //        TimeSpan ts = range.End - range.Start;

        //        double days = ts.TotalDays;

        //        return (int)Math.Floor(days / 7);
        //    }
    }
}
