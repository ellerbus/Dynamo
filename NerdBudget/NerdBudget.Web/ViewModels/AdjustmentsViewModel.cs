using System.Collections.Generic;
using System.Linq;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class AdjustmentsViewModel
    {
        public AdjustmentsViewModel(Account account)
        {
            Account = account;

            Budgets = account.Categories.SelectMany(x => x.Budgets).OrderBy(x => x.Name).ToList();

            Adjustments = Budgets.SelectMany(x => x.Adjustments)
                .OrderByDescending(x => x.Date).ThenBy(x => x.FullName)
                .ToList();
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddBasicPayload<Adjustment>()
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public Account Account { get; private set; }

        public IList<Budget> Budgets { get; private set; }

        public IList<Adjustment> Adjustments { get; private set; }
    }
}