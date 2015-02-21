using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Augment;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class BudgetCollection : KeyedCollection<string, Budget>
    {
        #region Constructors

        internal BudgetCollection(Account account, IEnumerable<Budget> budgets)
        {
            Account = account;

            if (budgets != null)
            {
                AddRange(budgets.OrderBy(x => x.Sequence));
            }
        }

        internal BudgetCollection(Category category, IEnumerable<Budget> budgets)
        {
            Account = category.Account;

            Category = category;

            if (budgets != null)
            {
                AddRange(budgets.OrderBy(x => x.Sequence));
            }
        }

        #endregion

        #region ToString/DebuggerDisplay

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get { return "Count={0}".FormatArgs(Count); }
        }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'account_id'
        ///	</summary>
        public Account Account { get; private set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'category_id'
        ///	</summary>
        public Category Category { get; private set; }

        #endregion

        #region Methods

        public void AddRange(IEnumerable<Budget> budgets)
        {
            foreach (Budget bgt in budgets)
            {
                Add(bgt);
            }
        }

        public void Resort()
        {
            IList<Budget> budget = this.OrderBy(x => x.Sequence).ToList();

            Clear();

            foreach (Budget bgt in budget)
            {
                Add(bgt);
            }
        }

        protected override string GetKeyForItem(Budget item)
        {
            return item.Id;
        }

        protected override void InsertItem(int index, Budget item)
        {
            base.InsertItem(index, item);

            SetOwner(item);
        }

        protected override void SetItem(int index, Budget item)
        {
            base.SetItem(index, item);

            SetOwner(item);
        }

        private void SetOwner(Budget bgt)
        {
            if (Category == null)
            {
                bgt.Account = Account;
            }
            else
            {
                bgt.Category = Category;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// IF true this collection is considered the mother load for
        /// the Account
        /// </summary>
        public bool ContainsAllBudgets { get { return Category == null; } }

        #endregion
    }
}