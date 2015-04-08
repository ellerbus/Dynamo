using System.Collections.Generic;
using System.Linq;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class ImportViewModel
    {
        public ImportViewModel(Account account)
        {
            Account = account;

            Budgets = account.Categories.SelectMany(x => x.Budgets).ToList();

            Ledger = account.Ledgers
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Sequence)
                .LastOrDefault();
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddPayload<Ledger>("Date,Description,Amount,Balance")
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public Account Account { get; private set; }

        public IList<Budget> Budgets { get; private set; }

        public Ledger Ledger { get; private set; }
    }
}