using System.ComponentModel.DataAnnotations;
using NerdBudget.Core.Models;

namespace NerdBudget.Web.ViewModels
{
    public class LoginViewModel
    {
        private Member _member;

        public LoginViewModel()
        {
            _member = new Member();
        }

        [Required]
        public string Name
        {
            get { return _member.Name; }
            set { _member.Name = value; }
        }

        [Required]
        public string Password
        {
            get { return _member.Password; }
            set { _member.Password = value; }
        }

        public bool RememberMe { get; set; }
    }
}