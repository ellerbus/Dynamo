using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Augment;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class AdjustmentCollection : KeyedCollection<string, Adjustment>
    {
        #region Constructors

        internal AdjustmentCollection(Account account, IEnumerable<Adjustment> adjustments)
        {
            Account = account;

            if (adjustments != null)
            {
                AddRange(adjustments);
            }
        }

        internal AdjustmentCollection(Budget budget, IEnumerable<Adjustment> adjustments)
        {
            Account = budget.Account;

            Budget = budget;

            if (adjustments != null)
            {
                AddRange(adjustments);
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
        ///	Gets / Sets the foreign key to 'budget_id'
        ///	</summary>
        public Budget Budget { get; private set; }

        #endregion

        #region Methods

        public void AddRange(IEnumerable<Adjustment> adjustments)
        {
            foreach (Adjustment adj in adjustments)
            {
                Add(adj);
            }
        }

        protected override string GetKeyForItem(Adjustment item)
        {
            return item.Id;
        }

        protected override void InsertItem(int index, Adjustment item)
        {
            base.InsertItem(index, item);

            SetOwner(item);
        }

        protected override void SetItem(int index, Adjustment item)
        {
            base.SetItem(index, item);

            SetOwner(item);
        }

        private void SetOwner(Adjustment bgt)
        {
            if (Budget == null)
            {
                bgt.Account = Account;
            }
            else
            {
                bgt.Budget = Budget;
            }
        }

        #endregion
    }
}