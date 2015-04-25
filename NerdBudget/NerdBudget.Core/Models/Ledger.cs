using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Augment;
using Insight.Database;

namespace NerdBudget.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Ledger : Entities.LedgerEntity
    {
        #region Constructors

        public Ledger() { }

        [SqlConstructor]
        public Ledger(string accountId, string id, DateTime date, string budgetId, string originalText, string description, double amount, double balance, int sequence, DateTime createdAt, DateTime? updatedAt)
            : base(accountId, id, date, budgetId, originalText, description, amount, balance, sequence, createdAt, updatedAt)
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
                string pk = "[" + AccountId + "," + Id + "," + Date.ToString("yyyy-MM-dd") + "]";

                string txt = "[" + OriginalText + "]";

                return "{0}, pk={1}, txt={2}".FormatArgs(GetType().Name, pk, txt);
            }
        }

        #endregion

        #region Parser

        private void Parse()
        {
            if (OriginalText.IsNotEmpty())
            {
                //  0           1           2           3       4       5
                //  DATE        MEMO        $W/D		$DEP    $BAL    {junk}
                List<string> parts = Regex.Split(OriginalText, @"\t", RegexOptions.Compiled).ToList();

                Date = parts[0].ToDate();

                Description = Regex.Replace(parts[1], @"\s+", " ").Trim();

                Amount = CleanAmount(parts[2], parts[3]);

                Balance = parts[4].AssertNotNull().ToDouble();

                Id = OriginalText.ToCrc32();
            }
        }

        private static double CleanAmount(string wd, string dep)
        {
            if (wd.IsNotEmpty())
            {
                return wd.ToDouble() * -1;
            }

            return dep.ToDouble();
        }

        #endregion

        #region Properties

        public override string Id
        {
            get { return base.Id; }
            set { base.Id = value.AssertNotNull().ToUpper().PadLeft(8, 'X'); }
        }

        public override DateTime Date
        {
            get { return base.Date; }
            set { base.Date = value.BeginningOfDay(); }
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

        public override string OriginalText
        {
            get { return base.OriginalText; }
            set
            {
                base.OriginalText = value.AssertNotNull().ToUpper();

                Parse();
            }
        }

        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value.AssertNotNull().ToUpper(); }
        }

        internal string CleanDescription
        {
            get
            {
                if (Description.IsNullOrEmpty())
                {
                    return "";
                }

                //  remove anything but numbers and letters
                string tmp = Regex.Replace(Description, @"[^0-9A-Z#/]", "", RegexOptions.Compiled);

                //  replace date numbers with [DATE]
                tmp = Regex.Replace(tmp, "[0-9]{2}/[0-9]{2}/[0-9]{2,4}", "(MDY)", RegexOptions.Compiled);

                //  replace date numbers with [PHONE]
                tmp = Regex.Replace(tmp, "[0-9]{3}-[0-9]{3}-[0-9]{4}", "(PHONE)", RegexOptions.Compiled);

                //  replace date numbers with [PHONE]
                tmp = Regex.Replace(tmp, "ACHTRACE[0-9]+", "(ACHTRACE)", RegexOptions.Compiled);

                return tmp;
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

        public string RegexMap
        {
            get
            {
                string tmp = CleanDescription;

                if (tmp.IsNullOrEmpty())
                {
                    return "";
                }

                //  replace ref numbers with [0-9]+
                tmp = Regex.Replace(CleanDescription, "#[0-9]+", "#[0-9]+", RegexOptions.Compiled);

                tmp = Regex.Replace(tmp, @"\(", @"\(", RegexOptions.Compiled);

                tmp = Regex.Replace(tmp, @"\)", @"\)", RegexOptions.Compiled);

                return tmp;
            }
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