using System;
using Augment;

namespace NerdBudget.Core
{
    public static class DateExtensions
    {
        public static DateTime ToMonthDate(this DateTime dt)
        {
            return dt.BeginningOfMonth().Date;
        }

        public static DateTime ToWeekDate(this DateTime dt)
        {
            return dt.BeginningOfWeek(DayOfWeek.Friday).Date;
        }
    }
}
