using System.Collections.Generic;
using System.Linq;
using NerdBudget.Core;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class BudgetsViewModel
    {
        public BudgetsViewModel(Account account)
        {
            Account = account;
            Categories = account.Categories.ToList();
            Frequencies = IdNamePair.CreateFromEnum<BudgetFrequencies>().ToList();
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Category>("Id,AccountId,Name,Budgets")
                .AddBasicPayload<Budget>()
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public Account Account { get; private set; }

        public IList<Category> Categories { get; private set; }

        public IList<IdNamePair> Frequencies { get; private set; }
    }
}