using System;
using System.Diagnostics;
using Augment;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Category : Entities.CategoryEntity
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
            set { base.CreatedAt = value.ToUniversalTime(); }
        }

        public override DateTime? UpdatedAt
        {
            get { return base.UpdatedAt; }
            set { base.UpdatedAt = value == null ? null as DateTime? : value.Value.ToUniversalTime(); }
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


        #endregion
    }
}