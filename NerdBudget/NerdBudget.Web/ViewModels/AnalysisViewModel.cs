using System;
using System.Collections.Generic;
using System.Linq;
using Augment;
using NerdBudget.Core;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class AnalysisViewModel
    {
        #region Classes

        private const int Weeks = 5;

        public class Header
        {
            public Header(int index, DateTime start, DateTime end, string title)
            {
                Range = new Range<DateTime>(start, end);

                Index = index;

                Title = title;
            }

            internal Range<DateTime> Range { get; private set; }

            public int Index { get; private set; }

            public string Title { get; private set; }

            public bool IsCurrent { get { return Range.Contains(DateTime.Now.Date); } }
            public bool IsProjection { get { return IsCurrent || Range.End >= DateTime.Now.Date; } }
            public bool IsHistory { get { return !IsCurrent && !IsProjection; } }
            public bool IsFuture { get { return !IsCurrent && !IsHistory; } }

            public DateTime Start { get { return Range.Start; } }
            public DateTime End { get { return Range.End; } }

            public double Balance { get; set; }
        }

        public class Detail
        {
            public Detail(Category c)
                : this(c.Id, c.Name)
            {
                IsCategory = true;
            }

            public Detail(Budget b)
                : this(b.Id, b.Name)
            {
                Multiplier = b.Category.Multiplier;
            }

            private Detail(string id, string title)
            {
                Id = id;
                Title = title;

                Values = new[] { new Value(0), new Value(1), new Value(2), new Value(3), new Value(4) };
            }

            public bool IsCategory { get; private set; }
            public string Id { get; private set; }
            public string Title { get; private set; }
            public int Multiplier { get; private set; }
            public Value[] Values { get; private set; }
        }

        public class Value
        {
            public Value(int index) { Index = index; }
            public int Index { get; private set; }
            public double Actual { get; set; }
            public double Budget { get; set; }
            //public double Variance { get { return Budget - Actual; } }
        }

        #endregion

        #region Constructors

        public AnalysisViewModel(Account account)
        {
            Account = account;

            Details = new List<Detail>();

            SetupDateRanges(DateTime.Now.Date);
            SetupHeaderBalances();
            SetupDetails();
        }

        #endregion

        #region Setup

        private void SetupDateRanges(DateTime date)
        {
            BudgetMonth = date.ToMonthlyBudgetRange();

            Headers = new Header[Weeks];

            Headers[0] = new Header(0, BudgetMonth.Start, BudgetMonth.Start.AddDays(6), "W-2");
            Headers[1] = new Header(1, Headers[0].End.AddDays(1), Headers[0].End.AddDays(7), "W-1");
            Headers[2] = new Header(2, Headers[1].End.AddDays(1), Headers[1].End.AddDays(7), "NOW");
            Headers[3] = new Header(3, Headers[2].End.AddDays(1), Headers[2].End.AddDays(7), "W+1");
            Headers[4] = new Header(4, Headers[3].End.AddDays(1), Headers[3].End.AddDays(7), "W+2");
        }

        private void SetupHeaderBalances()
        {
            for (int i = 0; i < Headers.Length; i++)
            {
                Header h = Headers[i];

                h.Balance = Account.Balances.GetBalanceAmount(h.Start);
            }
        }

        private void SetupDetails()
        {
            foreach (Category c in Account.Categories.OrderBy(x => x.Sequence))
            {
                Details.Add(new Detail(c));

                foreach (Budget b in c.Budgets.OrderBy(x => x.Sequence))
                {
                    Detail d = new Detail(b);

                    for (int i = 0; i < Headers.Length; i++)
                    {
                        Header h = Headers[i];

                        Value v = d.Values[i];

                        if (b.IsValidFor(h.Range))
                        {
                            v.Budget = b.Amount * c.Multiplier;
                        }

                        foreach (Adjustment adj in b.Adjustments)
                        {
                            if (adj.Date == null || h.Range.Contains(adj.Date.Value))
                            {
                                v.Budget += adj.Amount * c.Multiplier;
                            }
                        }

                        IEnumerable<Ledger> ledgers = Account.Ledgers.Where(x => x.BudgetId == b.Id);

                        foreach (Ledger l in ledgers)
                        {
                            if (h.Range.Contains(l.Date))
                            {
                                v.Actual += l.Amount;
                            }
                        }
                    }

                    Details.Add(d);
                }
            }
        }

        #endregion

        #region Methods

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        #endregion

        #region Properties

        public Account Account { get; private set; }

        public Range<DateTime> BudgetMonth { get; private set; }

        public Header[] Headers { get; private set; }

        public List<Detail> Details { get; private set; }

        #endregion
    }
}