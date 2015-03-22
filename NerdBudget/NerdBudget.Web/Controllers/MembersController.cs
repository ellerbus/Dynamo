using System.Web.Mvc;
using System.Web.Security;
using Augment;
using NerdBudget.Core.Services;
using NerdBudget.Web.ViewModels;

namespace NerdBudget.Web.Controllers
{
    public class MembersController : Controller
    {
        #region Members

        private IMemberService _service;

        #endregion

        #region Constructors

        public MembersController(IMemberService service)
        {
            _service = service;
        }

        #endregion

        #region Login/Logout

        //
        // GET: /Login
        [AllowAnonymous, Route("~/Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        //
        // GET: /Login
        [AllowAnonymous, Route("~/Login")]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Login
        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous, Route("~/Login")]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && _service.VerifyLogin(model.Name, model.Password))
            {
                if (returnUrl.IsNullOrEmpty())
                {
                    returnUrl = "/";
                }

                //  So that the user can be referred back to where they were when they click logon
                if (returnUrl.IsNullOrEmpty() && Request != null && Request.UrlReferrer != null)
                {
                    returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);
                }

                FormsAuthentication.SetAuthCookie(model.Name, model.RememberMe);

                return Redirect(returnUrl);
            }

            ModelState.AddModelError("Name", "Invalid Login");

            return View();
        }

        #endregion
    }
}