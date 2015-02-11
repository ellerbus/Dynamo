using System;
using System.Collections.Generic;
using System.Diagnostics;
using Augment;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Account : Entities.AccountEntity
    {
        #region Constructors

        public Account() : base() { }

        public Account(string id, string name, string type, DateTime startedAt, DateTime createdAt, DateTime? updatedAt)
            : base(id, name, type, startedAt, createdAt, updatedAt)
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
                string pk = "[" + Id + "]";

                string uq = "[" + Name + "]";

                return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
            }
        }

        #endregion

        #region Properties

        public override string Id
        {
            get { return base.Id; }
            set { base.Id = value.AssertNotNull().ToUpper(); }
        }

        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value.AssertNotNull().ToUpper(); }
        }

        public override DateTime StartedAt
        {
            get { return base.StartedAt; }
            set { base.StartedAt = value.ToUniversalTime().BeginningOfMonth(); }
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

        #endregion

        #region Collections/Children/Foreign Keys

        /// <summary>
        /// Stub for Insight.Database to load MARS
        /// </summary>
        private IList<Balance> AllBalances { get; set; }

        /// <summary>
        /// List of all balances for this Account
        /// </summary>
        public BalanceCollection Balances
        {
            get
            {
                if (_balances == null)
                {
                    _balances = new BalanceCollection(this, AllBalances);
                }
                return _balances;
            }
        }
        private BalanceCollection _balances;

        /// <summary>
        /// Stub for Insight.Database to load MARS
        /// </summary>
        private IList<Category> AllCategories { get; set; }

        /// <summary>
        /// List of all categories for this Account
        /// </summary>
        public CategoryCollection Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoryCollection(this, AllCategories);
                }
                return _categories;
            }
        }
        private CategoryCollection _categories;

        /// <summary>
        /// Stub for Insight.Database to load MARS
        /// </summary>
        private IList<Budget> AllBudgets { get; set; }

        /// <summary>
        /// List of all budgets for this Account
        /// </summary>
        internal BudgetCollection Budgets
        {
            get
            {
                if (_budgets == null)
                {
                    _budgets = new BudgetCollection(this, AllBudgets);
                }
                return _budgets;
            }
        }
        private BudgetCollection _budgets;

        ///// <summary>
        ///// Stub for Insight.Database to load MARS
        ///// </summary>
        //private IList<Adjustment> AllAdjustments { get; set; }

        ///// <summary>
        ///// List of all adjustments for this Account
        ///// </summary>
        //internal AdjustmentCollection Adjustments
        //{
        //    get
        //    {
        //        if (_adjustments == null)
        //        {
        //            _adjustments = new AdjustmentCollection(this, AllAdjustments);
        //        }
        //        return _adjustments;
        //    }
        //}
        //private AdjustmentCollection _adjustments;

        ///// <summary>
        ///// Stub for Insight.Database to load MARS
        ///// </summary>
        //private IList<Map> AllMaps { get; set; }

        ///// <summary>
        ///// List of all maps for this Account
        ///// </summary>
        //public MapCollection Maps
        //{
        //    get
        //    {
        //        if (_maps == null)
        //        {
        //            _maps = new MapCollection(this, AllMaps);
        //        }
        //        return _maps;
        //    }
        //}
        //private MapCollection _maps;

        /// <summary>
        /// Stub for Insight.Database to load MARS
        /// </summary>
        private IList<Ledger> AllLedgers { get; set; }

        /// <summary>
        /// List of all ledgers for this Account
        /// (Collection of Ledgers, since Transaction is a reserved DB word)
        /// </summary>
        public LedgerCollection Ledgers
        {
            get
            {
                if (_ledgers == null)
                {
                    _ledgers = new LedgerCollection(this, AllLedgers);
                }
                return _ledgers;
            }
        }
        private LedgerCollection _ledgers;

        #endregion

        #region Amounts

        /// <summary>
        /// Total variance of weekly budget
        /// </summary>
        public double WeeklyAmount
        {
            get
            {
                double amount = 0;

                foreach (Category cat in Categories)
                {
                    foreach (Budget bud in cat.Budgets)
                    {
                        amount += bud.WeeklyAmount * cat.Multiplier;
                    }
                }

                return amount;
            }
        }

        /// <summary>
        /// Total variance of monthly budget
        /// </summary>
        public double MonthlyAmount
        {
            get
            {
                double amount = 0;

                foreach (Category cat in Categories)
                {
                    foreach (Budget bud in cat.Budgets)
                    {
                        amount += bud.MonthlyAmount * cat.Multiplier;
                    }
                }

                return amount;
            }
        }

        /// <summary>
        /// Total variance of yearly budget
        /// </summary>
        public double YearlyAmount
        {
            get
            {
                double amount = 0;

                foreach (Category cat in Categories)
                {
                    foreach (Budget bud in cat.Budgets)
                    {
                        amount += bud.YearlyAmount * cat.Multiplier;
                    }
                }

                return amount;
            }
        }

        #endregion
    }
}