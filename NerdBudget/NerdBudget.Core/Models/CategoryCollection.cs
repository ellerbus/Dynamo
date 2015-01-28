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
    public partial class CategoryCollection : Collection<Category>
    {
        #region Constructors

        public CategoryCollection(Account account)
        {
            Account = account;
        }

        public CategoryCollection(Account account, IEnumerable<Category> categories)
            : this(account)
        {
            if (categories != null)
            {
                foreach (Category c in categories.OrderBy(x => x.Sequence))
                {
                    Add(c);
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

        public void AddRange(IList<Category> categories)
        {
            foreach (Category c in categories)
            {
                Add(c);
            }
        }

        public void Resort()
        {
            IList<Category> categories = this.OrderBy(x => x.Sequence).ToList();

            Clear();

            foreach (Category cat in categories)
            {
                Add(cat);
            }
        }

        protected override void InsertItem(int index, NerdBudget.Core.Models.Category item)
        {
            base.InsertItem(index, item);

            item.Account = Account;
        }

        protected override void SetItem(int index, Category item)
        {
            base.SetItem(index, item);

            item.Account = Account;
        }

        #endregion
    }
}