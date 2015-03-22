using System.Collections.Generic;
using System.Linq;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class AccountsViewModel
    {
        public AccountsViewModel(IEnumerable<Account> accounts)
        {
            Accounts = accounts.ToList();
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public IList<Account> Accounts { get; private set; }
    }
}