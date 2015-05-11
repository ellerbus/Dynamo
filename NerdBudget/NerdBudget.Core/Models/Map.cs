using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Augment;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Map : Entities.MapEntity
    {
        #region Constructors

        public Map() : base() { }

        public Map(string accountId, string id, string budgetId, string regexPattern, DateTime createdAt, DateTime? updatedAt)
            : base(accountId, id, budgetId, regexPattern, createdAt, updatedAt)
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
                string pk = "[" + AccountId + "," + BudgetId + "," + Id + "]";

                string uq = "[" + "]";

                return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
            }
        }

        #endregion

        #region Properties

        public string BudgetName
        {
            get { return "{0}".FormatArgs(Budget == null ? "?" : Budget.Name.ToLower()); }
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

        public override string Id
        {
            get { return base.Id; }
            set { base.Id = value.AssertNotNull().ToUpper().PadLeft(8, 'X'); }
        }

        public override string RegexPattern
        {
            get { return base.RegexPattern; }
            set
            {
                string pattern = value.AssertNotNull().Trim();

                base.RegexPattern = pattern;

                Id = pattern.ToCrc32();
            }
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

        #region Methods

        public bool IsMatchFor(Ledger ledger)
        {
            string descr = ledger.CleanDescription.Trim();

            return Regex.IsMatch(descr, RegexPattern, RegexOptions.Compiled);
        }

        #endregion
    }
}