using System;
using System.Diagnostics;
using Augment;
using Insight.Database;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Budget : Entities.BudgetEntity
    {
        #region Constructors

        public Budget() : base() { }

        [SqlConstructor]
        public Budget(string accountId, string id, string categoryId, string name, string frequency, int sequence, DateTime? startDate, DateTime? endDate, double amount, DateTime createdAt, DateTime? updatedAt)
            : base(accountId, id, categoryId, name, frequency, sequence, startDate, endDate, amount, createdAt, updatedAt)
        {
        }

        #endregion

        #region ToString/DebuggerDisplay

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get
            {
                string pk = "[" + AccountId + "," + Id + "]";

                string uq = "[" + AccountId + "," + Name + "]";

                return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
            }
        }

        #endregion

        #region Properties

        public string FullName
        {
            get
            {
                return "{0} ({1})".FormatArgs(Name, Category.Name.ToLower());
            }
        }

        public override string Id
        {
            get { return base.Id; }
            set { base.Id = value.AssertNotNull().ToUpper(); }
        }

        public override string AccountId
        {
            get { return base.AccountId; }
            set { base.AccountId = value.AssertNotNull().ToUpper(); }
        }

        public override string CategoryId
        {
            get { return base.CategoryId; }
            set { base.CategoryId = value.AssertNotNull().ToUpper(); }
        }

        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value.AssertNotNull().ToUpper(); }
        }

        public override DateTime CreatedAt
        {
            get { return base.CreatedAt; }
            set { base.CreatedAt = value.ToUniversalTime(); }
        }

        public override DateTime? UpdatedAt
        {
            get { return base.UpdatedAt; }
            set { base.UpdatedAt = value == null ? null as DateTime? : value.Value.ToUniversalTime(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public BudgetFrequencies BudgetFrequency
        {
            get { return Frequency.AssertNotNull(BudgetFrequencies.NO.ToString()).ToEnum<BudgetFrequencies>(); }
            set { Frequency = value.ToString(); }
        }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'account_id'
        ///	</summary>
        public Account Account
        {
            get
            {
                return _account;
            }
            internal set
            {
                _account = value;

                AccountId = value == null ? default(string) : value.Id;
            }
        }
        private Account _account;

        ///	<summary>
        ///	Gets / Sets the foreign key to 'category_id'
        ///	</summary>
        public Category Category
        {
            get
            {
                return _category;
            }
            internal set
            {
                _category = value;

                CategoryId = value == null ? default(string) : value.Id;

                Account = value == null ? null : value.Account;
            }
        }
        private Category _category;


        #endregion

        #region Amount Conversions

        /// <summary>
        /// 
        /// </summary>
        public double MonthlyAmount
        {
            get
            {
                switch (BudgetFrequency)
                {
                    case BudgetFrequencies.W1:
                        //case BudgetFrequencies.XM:
                        //case BudgetFrequencies.XT:
                        //case BudgetFrequencies.XW:
                        //case BudgetFrequencies.XR:
                        //case BudgetFrequencies.XF:
                        //case BudgetFrequencies.XS:
                        //case BudgetFrequencies.XN:
                        return Amount * 52.0 / 12.0;

                    case BudgetFrequencies.W2:
                        return Amount * 52.0 / 2.0 / 12.0;

                    case BudgetFrequencies.W3:
                        return Amount * 52.0 / 3.0 / 12.0;

                    case BudgetFrequencies.W4:
                        return Amount * 52.0 / 4.0 / 12.0;

                    case BudgetFrequencies.W5:
                        return Amount * 52.0 / 5.0 / 12.0;

                    case BudgetFrequencies.W6:
                        return Amount * 52.0 / 6.0 / 12.0;

                    case BudgetFrequencies.M1:
                        return Amount;

                    case BudgetFrequencies.M2:
                        return Amount * 6.0 / 12.0;

                    case BudgetFrequencies.MT:
                        return Amount * 24.0 / 12.0;

                    case BudgetFrequencies.Q1:
                        return Amount * 4.0 / 12.0;

                    case BudgetFrequencies.Y1:
                        return Amount / 12.0;
                }

                return 0.0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double WeeklyAmount
        {
            get { return MonthlyAmount * 12.0 / 52.0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double YearlyAmount
        {
            get { return MonthlyAmount * 12.0; }
        }

        #endregion

        #region Date Range Helper

        public bool IsValidFor(Range<DateTime> range)
        {
            if (BudgetFrequency == BudgetFrequencies.NO || Amount == 0)
            {
                return true;
            }

            if (EndDate != null)
            {
                if (range.End < StartDate || range.Start > EndDate)
                {
                    return false;
                }
            }

            DateTime date = StartDate.Value;

            while (date <= range.End)
            {
                if (range.Start <= date && date <= range.End)
                {
                    return true;
                }

                date = GetNextDate(date);
            }

            return false;
        }

        private DateTime GetNextDate(DateTime date)
        {
            switch (BudgetFrequency)
            {
                case BudgetFrequencies.W1:
                    //case BudgetFrequencies.XM:
                    //case BudgetFrequencies.XT:
                    //case BudgetFrequencies.XW:
                    //case BudgetFrequencies.XR:
                    //case BudgetFrequencies.XF:
                    //case BudgetFrequencies.XS:
                    //case BudgetFrequencies.XN:
                    return date.AddDays(7);

                case BudgetFrequencies.W2:
                    return date.AddDays(7 * 2);

                case BudgetFrequencies.W3:
                    return date.AddDays(7 * 3);

                case BudgetFrequencies.W4:
                    return date.AddDays(7 * 4);

                case BudgetFrequencies.W5:
                    return date.AddDays(7 * 5);

                case BudgetFrequencies.W6:
                    return date.AddDays(7 * 6);

                case BudgetFrequencies.M1:
                    return date.AddMonths(1);

                case BudgetFrequencies.M2:
                    return date.AddMonths(2);

                case BudgetFrequencies.MT:
                    if (date.Day != 15)
                    {
                        if (date == date.EndOfMonth().Date)
                        {
                            date = date.AddMonths(1);
                        }

                        return new DateTime(date.Year, date.Month, 15);
                    }

                    return date.EndOfMonth().Date;

                case BudgetFrequencies.Q1:
                    return date.AddMonths(3);

                case BudgetFrequencies.Y1:
                    return date.AddYears(1);
            }

            return date;
        }

        #endregion
    }
}