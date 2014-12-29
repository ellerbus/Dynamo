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
	/// Represents a basic controller for Role
	///	</summary>
	public class RolesController : Controller
	{
		#region Members
	
		private RoleService _service;
		private IValidator<Role> _validator;
		
		#endregion
		
		#region Contructors

		public RolesController(RoleService service, IValidator<Role> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Index

		//
		// GET: /Role/Index
		public ActionResult Index()
		{
			return View(_service.Get());
		}
		
		#endregion
		
		#region Create

		//
		// GET: /Role/Create
		public ActionResult Create()
		{			
			return View(new Role());
		}

		//
		// POST: /Role/Create
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(Role model)
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
		// GET: /Role/Edit/5
		public ActionResult Edit(int id)
		{
			Role model = _service.Get(id);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /Role/Edit/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(Role model)
		{
			Role role = _service.Get(model.Id);
			
			role.Name = model.Name;
			role.CreatedAt = model.CreatedAt;
			role.UpdatedAt = model.UpdatedAt;

			ValidationResult vr = _validator.Validate(role);

			if (vr.IsValid)
			{
				_service.Save(role);

				return RedirectToAction("Index");
			}
			
			//	TODO	ModelState.AddValidationResults(vr);

			return View(model);
		}
		
		#endregion
		
		#region Delete

		//
		// GET: /Role/Delete/5
		public ActionResult Delete(int id)
		{
			Role model = _service.Get(id);
			
			if (model == null)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		//
		// POST: /Role/Delete/5
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(Role model)
		{
			_service.Delete(model);

			return RedirectToAction("Index");
		}
		
		#endregion
	}
}