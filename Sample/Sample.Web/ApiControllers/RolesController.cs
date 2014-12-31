using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FluentValidation;
using FluentValidation.Results;
using Sample.Core.Models;
using Sample.Core.Services;
using Sample.Core.Validators;

namespace Sample.Web.ApiControllers
{
	///	<summary>
	/// Represents a basic controller for Role
	///	</summary>
	[RoutePrefix("api/roles")]
	public class RolesController : ApiController
	{
		#region Members
	
		private IRoleService _service;
		private IValidator<Role> _validator;
		
		#endregion
		
		#region Contructors

		public RolesController(IRoleService service, IValidator<Role> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Verb Actions

		// GET: api/role
		[HttpGet, Route("")]
		public IEnumerable<Role> GetAll()
		{
			return _service.Get();
		}

		// GET: api/role/5
		[HttpGet, Route("{id}"), ResponseType(typeof(Role))]
		public IHttpActionResult Get(int id)
		{
			Role role = _service.Get(id);
			
			if (role == null)
			{
				return NotFound();
			}

			return Ok(role);
		}

		// POST: api/role
		[HttpPost, Route("")]
		public IHttpActionResult Post([FromBody]Role role)
		{
			ValidationResult vr = _validator.Validate(role);

			if (vr.IsValid)
			{
				_service.Insert(role);

				return Ok();
			}
			
			return BadRequest(vr);
		}

		// PUT: api/role/5
		[HttpPut, Route("{id}")]
		public IHttpActionResult Put(int id, [FromBody]Role role)
		{
			Role model = _service.Get(id);

			if (model == null)
			{
				return NotFound();
			}
			
			model.Name = role.Name;
			model.CreatedAt = role.CreatedAt;
			model.UpdatedAt = role.UpdatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Update(model);

				return Ok();
			}
			
			return BadRequest(vr);
		}

		// DELETE: api/role/5
		[HttpDelete, Route("{id}")]
		public IHttpActionResult Delete(int id)
		{
			Role model = _service.Get(id);

			if (model == null)
			{
				return NotFound();
			}

			_service.Delete(model);
			
			return Ok();
		}
		
		#endregion

		#region Helpers

		private IHttpActionResult BadRequest(ValidationResult vr)
		{
			foreach (ValidationFailure vf in vr.Errors)
			{
				ModelState.AddModelError(vf.PropertyName, vf.ErrorMessage);
			}
		
			return BadRequest(ModelState);
		}
		
		#endregion
	}
}
