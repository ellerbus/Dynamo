using System;
using System.Diagnostics;
using Augment;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Adjustment : Entities.AdjustmentEntity
    {
        #region ToString/DebuggerDisplay

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get
            {
                string pk = "[" + AccountId + "," + Id + "]";

                string uq = "[" + Name + "]";

                return "{0}, pk={1}, {2}".FormatArgs(GetType().Name, pk, uq);
            }
        }

        #endregion

        #region Properties

        public string FullName
        {
            get { return "{0} {1}".FormatArgs(Budget == null ? "?" : Budget.Name.ToLower(), Name); }
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

        public override string BudgetId
        {
            get { return base.BudgetId; }
            set { base.BudgetId = value.AssertNotNull().ToUpper(); }
        }

        public override DateTime? Date
        {
            get { return base.Date; }
            set
            {
                base.Date = value == null ? null as DateTime? : value.Value.EnsureUtc().BeginningOfDay();
            }
        }

        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value.AssertNotNull().ToUpper(); }
        }

        public override DateTime CreatedAt
        {
            get { return base.CreatedAt; }
            set { base.CreatedAt = value.EnsureUtc(); }
        }

        public override DateTime? UpdatedAt
        {
            get { return base.UpdatedAt; }
            set { base.UpdatedAt = value == null ? null as DateTime? : value.Value.EnsureUtc(); }
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
        ///	Gets / Sets the foreign key to 'budget_id'
        ///	</summary>
        public Budget Budget
        {
            get
            {
                return _budget;
            }
            internal set
            {
                _budget = value;

                BudgetId = value == null ? default(string) : value.Id;

                Account = value == null ? null : value.Account;
            }
        }
        private Budget _budget;


        #endregion
    }
}