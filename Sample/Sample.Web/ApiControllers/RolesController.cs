using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
		
		#region Verb Actions

		// GET: api/role
		[HttpGet, Route("")]
		public IEnumerable<Role> GetAll()
		{
			return _service.Get();
		}

		// GET: api/role/5
		[HttpGet, Route("{id}")]
		public Role Get(int id)
		{
			Role role = _service.Get(id);
			
			if (role == null)
			{
			
			}

			return role;
		}

		// POST: api/role
		[HttpPost, Route("")]
		public void Post([FromBody]Role role)
		{
			ValidationResult vr = _validator.Validate(role);

			if (vr.IsValid)
			{
				_service.Save(role);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/role/5
		[HttpPut, Route("{id}")]
		public void Put(int id, [FromBody]Role role)
		{
			Role model = _service.Get(id);
			
			model.Name = role.Name;
			model.CreatedAt = role.CreatedAt;
			model.UpdatedAt = role.UpdatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/role/5
		[HttpDelete, Route("{id}")]
		public void Delete(int id)
		{
			Role model = _service.Get(id);

			_service.Delete(model);
		}
		
		#endregion
	}
}
