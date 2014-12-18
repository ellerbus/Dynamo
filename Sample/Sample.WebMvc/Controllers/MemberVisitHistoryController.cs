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

namespace Sample.WebMvc.Controllers
{
	///	<summary>
	/// Represents a basic controller for MemberVisitHistory
	///	</summary>
	public class MemberVisitHistoryController : Controller
	{
		#region Members
	
		private MemberVisitHistoryService _service;
		private IValidator<MemberVisitHistory> _validator;
		
		#endregion
		
		#region Contructors

		public MemberVisitHistoryController(MemberVisitHistoryService service, IValidator<MemberVisitHistory> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Index

		//
		// GET: /MemberVisitHistory/Index
		public ActionResult Index()
		{
			return View(_service.GetList());
		}
		
		#endregion
		
		#region Create

		//
		// GET: /MemberVisitHistory/Create
		public ActionResult Create()
		{			
			return View(new MemberVisitHistory());
		}

		//
		// POST: /MemberVisitHistory/Create
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(MemberVisitHistory model)
		{
			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return RedirectToAction("Index");
			}
			
			//	TODO	ModelState.AddValidationResults(vr);

			return View(model);
		}
		
		#endregion
		
		#region Edit

		//
		// GET: /MemberVisitHistory/Edit/5
		public ActionResult Edit(int memberId, DateTime visitedAt)
		{
			MemberVisitHistory model = _service.Get(memberId, visitedAt);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /MemberVisitHistory/Edit/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(MemberVisitHistory model)
		{
			MemberVisitHistory memberVisitHistory = _service.Get(model.MemberId, VisitedAt);
			
			memberVisitHistory.PageUrl = model.PageUrl;

			ValidationResult vr = _validator.Validate(memberVisitHistory);

			if (vr.IsValid)
			{
				_service.Save(memberVisitHistory);

				return RedirectToAction("Index");
			}
			
			//	TODO	ModelState.AddValidationResults(vr);

			return View(model);
		}
		
		#endregion
		
		#region Delete

		//
		// GET: /MemberVisitHistory/Delete/5
		public ActionResult Delete(int memberId, DateTime visitedAt)
		{
			MemberVisitHistory model = _service.Get(memberId, visitedAt);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /MemberVisitHistory/Delete/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(MemberVisitHistory model)
		{
			_service.Delete(model);

			return RedirectToAction("Index");
		}
		
		#endregion
	}
}