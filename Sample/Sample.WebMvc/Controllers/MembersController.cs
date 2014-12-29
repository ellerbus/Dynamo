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
	/// Represents a basic controller for Member
	///	</summary>
	public class MembersController : Controller
	{
		#region Members
	
		private MemberService _service;
		private IValidator<Member> _validator;
		
		#endregion
		
		#region Contructors

		public MembersController(MemberService service, IValidator<Member> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Index

		//
		// GET: /Member/Index
		public ActionResult Index()
		{
			return View(_service.Get());
		}
		
		#endregion
		
		#region Create

		//
		// GET: /Member/Create
		public ActionResult Create()
		{			
			return View(new Member());
		}

		//
		// POST: /Member/Create
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(Member model)
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
		// GET: /Member/Edit/5
		public ActionResult Edit(int id)
		{
			Member model = _service.Get(id);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /Member/Edit/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(Member model)
		{
			Member member = _service.Get(model.Id);
			
			member.Name = model.Name;
			member.CreatedAt = model.CreatedAt;
			member.UpdatedAt = model.UpdatedAt;

			ValidationResult vr = _validator.Validate(member);

			if (vr.IsValid)
			{
				_service.Save(member);

				return RedirectToAction("Index");
			}
			
			//	TODO	ModelState.AddValidationResults(vr);

			return View(model);
		}
		
		#endregion
		
		#region Delete

		//
		// GET: /Member/Delete/5
		public ActionResult Delete(int id)
		{
			Member model = _service.Get(id);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /Member/Delete/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(Member model)
		{
			_service.Delete(model);

			return RedirectToAction("Index");
		}
		
		#endregion
	}
}