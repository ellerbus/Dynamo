using System.Web.Mvc;
using NerdBudget.Core.Services;
using NerdBudget.Web.ViewModels;

namespace NerdBudget.Web.Controllers
{
    [Authorize]
    public class DisplayController : Controller
    {
        #region Members

        private IAccountService _service;

        #endregion

        #region Constructors

        public DisplayController(IAccountService service)
        {
            _service = service;
        }

        #endregion

        #region Methods

        //
        // GET: /Member/Login
        [Route("~/Accounts")]
        public ActionResult Accounts()
        {
            return View(new AccountsViewModel(_service.GetList()));
        }

        //
        // GET: /Member/Login
        [Route("~/Categories/{id}")]
        public ActionResult Categories(string id)
        {
            return View(new CategoriesViewModel(_service.Get(id)));
        }

        #endregion
    }
}