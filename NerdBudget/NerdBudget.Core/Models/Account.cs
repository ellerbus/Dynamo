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

        #endregion
    }
}