using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Augment;
using EnsureThat;

namespace NerdBudget.Core.Models
{
    internal static class IEnumerableMapExtenions
    {
        public static IList<Map> ToMatchList(this IEnumerable<Map> maps)
        {
            return maps.OrderByDescending(x => x.RegexPattern.Length).ToArray();
        }

        public static Map FindMatchFor(this IEnumerable<Map> maps, Ledger led)
        {
            return maps.FirstOrDefault(x => x.IsMatchFor(led));
        }
    }

    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class MapCollection : Collection<Map>
    {
        #region Constructors

        internal MapCollection(Account account, IEnumerable<Map> maps)
        {
            Account = account;

            if (maps != null)
            {
                foreach (Map map in maps)
                {
                    Add(map);
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

        public Map CreateFor(Ledger ledger)
        {
            //  does a map already exist (make sure one pattern per budget)
            Map map = this.SingleOrDefault(x => x.RegexPattern == ledger.RegexMap);

            if (map == null)
            {
                map = new Map()
                {
                    AccountId = ledger.AccountId,
                    BudgetId = ledger.BudgetId,
                    RegexPattern = ledger.RegexMap,
                };

                Add(map);
            }
            else
            {
                //  ok we have one, so update the ledger
                ledger.BudgetId = map.BudgetId;
            }

            return map;
        }

        protected override void InsertItem(int index, Map item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, Map item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(Map item)
        {
            item.Account = Account;

            Ensure.That(Account.Budgets.Contains(item.BudgetId))
                .WithExtraMessageOf(() => "Budget ID '{0}' could not be found".FormatArgs(item.BudgetId))
                .IsTrue();

            item.Budget = Account.Budgets[item.BudgetId];
        }

        #endregion
    }
}