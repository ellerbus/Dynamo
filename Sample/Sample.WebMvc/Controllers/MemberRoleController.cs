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
	/// Represents a basic controller for MemberRole
	///	</summary>
	public class MemberRoleController : Controller
	{
		#region Members
	
		private MemberRoleService _service;
		private IValidator<MemberRole> _validator;
		
		#endregion
		
		#region Contructors

		public MemberRoleController(MemberRoleService service, IValidator<MemberRole> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Index

		//
		// GET: /MemberRole/Index
		public ActionResult Index()
		{
			return View(_service.GetList());
		}
		
		#endregion
		
		#region Create

		//
		// GET: /MemberRole/Create
		public ActionResult Create()
		{			
			return View(new MemberRole());
		}

		//
		// POST: /MemberRole/Create
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(MemberRole model)
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
		// GET: /MemberRole/Edit/5
		public ActionResult Edit(int memberId, int roleId)
		{
			MemberRole model = _service.Get(memberId, roleId);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /MemberRole/Edit/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(MemberRole model)
		{
			MemberRole memberRole = _service.Get(model.MemberId, model.RoleId);
			

			ValidationResult vr = _validator.Validate(memberRole);

			if (vr.IsValid)
			{
				_service.Save(memberRole);

				return RedirectToAction("Index");
			}
			
			//	TODO	ModelState.AddValidationResults(vr);

			return View(model);
		}
		
		#endregion
		
		#region Delete

		//
		// GET: /MemberRole/Delete/5
		public ActionResult Delete(int memberId, int roleId)
		{
			MemberRole model = _service.Get(memberId, roleId);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /MemberRole/Delete/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(MemberRole model)
		{
			_service.Delete(model);

			return RedirectToAction("Index");
		}
		
		#endregion
	}
}