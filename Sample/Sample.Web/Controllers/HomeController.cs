using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Sample.Core.Models;
using Sample.Core.Services;
using Sample.Core.Validators;

namespace Sample.Web.Controllers
{
    ///	<summary>
    /// Represents a basic controller for MemberRole
    ///	</summary>
    public class HomeController : Controller
    {
        #region Index

        //
        // GET: /MemberRole/Index
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}