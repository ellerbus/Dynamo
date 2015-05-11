using System;
using System.Linq;
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

        [Route("~/")]
        public ActionResult Index()
        {
            return RedirectToAction("Accounts");
        }

        [Route("~/Accounts")]
        public ActionResult Accounts()
        {
            return View(new AccountsViewModel(_service.GetList().OrderBy(x => x.Name)));
        }

        [Route("~/Categories/{id}")]
        public ActionResult Categories(string id)
        {
            return View(new CategoriesViewModel(_service.Get(id)));
        }

        [Route("~/Budgets/{id}")]
        public ActionResult Budgets(string id)
        {
            return View(new BudgetsViewModel(_service.Get(id)));
        }

        [Route("~/Adjustments/{id}")]
        public ActionResult Adjustments(string id)
        {
            return View(new AdjustmentsViewModel(_service.Get(id)));
        }

        [Route("~/Maps/{id}")]
        public ActionResult Maps(string id)
        {
            return View(new MapsViewModel(_service.Get(id)));
        }

        [Route("~/Import/{id}")]
        public ActionResult Import(string id)
        {
            return View(new ImportViewModel(_service.Get(id)));
        }

        [Route("~/Analysis/{id}")]
        public ActionResult Analysis(string id, DateTime? start)
        {
            return View(new AnalysisViewModel(_service.Get(id)));
        }

        #endregion
    }
}