using System.Collections.Generic;
using System.Linq;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class BalancesViewModel
    {
        public BalancesViewModel(Account account)
        {
            Account = account;

            Balances = account.Balances.OrderByDescending(x => x.AsOf).ToList();
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddStandardPayload<Balance>()
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public Account Account { get; private set; }

        public IList<Balance> Balances { get; private set; }
    }
}