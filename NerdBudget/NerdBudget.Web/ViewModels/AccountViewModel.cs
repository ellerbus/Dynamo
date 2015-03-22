using NerdBudget.Core.Models;

namespace NerdBudget.Web.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel(Account account)
        {
            Account = account;
        }

        public Account Account { get; private set; }
    }
}