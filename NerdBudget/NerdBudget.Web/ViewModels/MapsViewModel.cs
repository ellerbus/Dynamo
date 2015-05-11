using System.Collections.Generic;
using System.Linq;
using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class MapsViewModel
    {
        public MapsViewModel(Account account)
        {
            Account = account;

            Budgets = account.Categories.SelectMany(x => x.Budgets).OrderBy(x => x.Name).ToList();

            Maps = account.Maps.ToMatchList();
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddStandardPayload<Map>()
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public Account Account { get; private set; }

        public IList<Budget> Budgets { get; private set; }

        public IList<Map> Maps { get; private set; }
    }
}