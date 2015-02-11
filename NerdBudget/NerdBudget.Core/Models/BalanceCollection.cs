using System;
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
    public partial class BalanceCollection : KeyedCollection<DateTime, Balance>
    {
        #region Constructors

        internal BalanceCollection(Account account, IEnumerable<Balance> balances)
        {
            Account = account;

            if (balances != null)
            {
                foreach (Balance b in balances.OrderBy(x => x.AsOf))
                {
                    Add(b);
                }
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

        #endregion

        #region Methods

        public void Update()
        {
            foreach (Ledger led in Account.Ledgers.OrderBy(x => x.Date).ThenBy(x => x.Sequence))
            {
                UpdateBalance(led.Balance, led.Date.ToMonthDate());
                UpdateBalance(led.Balance, led.Date.ToWeekDate());
            }
        }

        private void UpdateBalance(double amount, DateTime dt)
        {
            Balance b = null;

            if (Contains(dt))
            {
                this[dt].Amount = amount;
            }
            else
            {
                b = new Balance { AsOf = dt, Amount = amount };

                Add(b);
            }
        }

        public void AddRange(IEnumerable<Balance> balances)
        {
            foreach (Balance b in balances)
            {
                Add(b);
            }
        }

        protected override DateTime GetKeyForItem(Balance item)
        {
            return item.AsOf;
        }

        protected override void InsertItem(int index, Balance item)
        {
            base.InsertItem(index, item);

            item.Account = Account;
        }

        protected override void SetItem(int index, Balance item)
        {
            base.SetItem(index, item);

            item.Account = Account;
        }

        #endregion
    }
}