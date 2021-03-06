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
    public partial class LedgerCollection : Collection<Ledger>
    {
        #region Constructors

        internal LedgerCollection(Account account, IEnumerable<Ledger> ledgers)
        {
            Account = account;

            if (ledgers != null)
            {
                foreach (Ledger ld in ledgers.OrderBy(x => x.Date).ThenBy(x => x.Sequence))
                {
                    Add(ld);
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

        public IEnumerable<Ledger> MissingBudget()
        {
            return this.Where(x => x.BudgetId.IsNullOrEmpty());
        }

        public void Import(string input)
        {
            if (input.IsNotEmpty())
            {
                IList<Map> maps = Account.Maps.ToMatchList();

                int trxCount = Count;

                string[] lines = input.AssertNotNull().Split('\r', '\n');

                foreach (string line in lines)
                {
                    string details = line.AssertNotNull().Trim();

                    if (details.IsNotEmpty())
                    {
                        Ledger led = new Ledger
                        {
                            OriginalText = details
                        };

                        Ledger match = Find(led.Id, led.Date);

                        if (match == null)
                        {
                            led.Sequence = Count;

                            Map map = maps.FindMatchFor(led);

                            if (map != null)
                            {
                                led.BudgetId = map.BudgetId;
                            }

                            Add(led);
                        }
                    }
                }

                if (Count > trxCount)
                {
                    Account.Balances.Update(this);
                }
            }
        }

        public Ledger Find(string id, DateTime date)
        {
            id = id.AssertNotNull().ToUpper();

            return this.FirstOrDefault(x => x.Id == id && x.Date == date);
        }

        public void AddRange(IEnumerable<Ledger> ledgers)
        {
            foreach (Ledger ld in ledgers)
            {
                Add(ld);
            }
        }

        protected override void InsertItem(int index, Ledger item)
        {
            base.InsertItem(index, item);

            item.Account = Account;
        }

        protected override void SetItem(int index, Ledger item)
        {
            base.SetItem(index, item);

            item.Account = Account;
        }

        #endregion
    }
}