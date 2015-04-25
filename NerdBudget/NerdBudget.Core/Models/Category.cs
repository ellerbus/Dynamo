using System;
using System.Diagnostics;
using System.Linq;
using Augment;
using Insight.Database;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Category : Entities.CategoryEntity
    {
        #region Constructors

        public Category() : base() { }

        [SqlConstructor]
        public Category(string accountId, string id, string name, int multiplier, int sequence, DateTime createdAt, DateTime? updatedAt)
            : base(accountId, id, name, multiplier, sequence, createdAt, updatedAt)
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

        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value.AssertNotNull().ToUpper(); }
        }

        public override int Multiplier
        {
            get { return base.Multiplier; }
            set { base.Multiplier = Math.Sign(value); }
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

        #endregion

        #region Collections/Children/Foreign Keys

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

        /// <summary>
        /// List of Budgets for this Category
        /// </summary>
        public BudgetCollection Budgets
        {
            get
            {
                if (_budgets == null && Account != null)
                {
                    _budgets = new BudgetCollection(this, Account.Budgets.Where(x => x.CategoryId == Id));
                }

                return _budgets;
            }
        }
        private BudgetCollection _budgets;


        #endregion
    }
}