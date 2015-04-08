using NerdBudget.Core.Models;
using Newtonsoft.Json;

namespace NerdBudget.Web.ViewModels
{
    public class AnalysisViewModel
    {
        public AnalysisViewModel(Account account)
        {
            Account = account;
        }

        public string ToJson()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .ToSettings();

            string json = JsonConvert.SerializeObject(this, jss);

            return json;
        }

        public Account Account { get; private set; }
    }
}