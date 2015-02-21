using System;
using System.Diagnostics;
using Augment;
using Insight.Database;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Balance : Entities.BalanceEntity
    {
        #region Constructors

        public Balance() { }

        [SqlConstructor]
        public Balance(string accountId, DateTime asOf, double amount, DateTime createdAt, DateTime? updatedAt)
            : base(accountId, asOf, amount, createdAt, updatedAt)
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
                string pk = "[" + AccountId + "," + AsOf.ToString("yyyy-MM-dd") + "]";

                string uq = "[" + "]";

                return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
            }
        }

        #endregion

        #region Properties

        public override string AccountId
        {
            get { return base.AccountId; }
            set { base.AccountId = value.AssertNotNull().ToUpper(); }
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

        #region Foreign Key Properties

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


        #endregion
    }
}