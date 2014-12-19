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

namespace Sample.WebApi.Controllers
{
	///	<summary>
	/// Represents a basic controller for Role
	///	</summary>
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

		// GET: api/Member
		public IEnumerable<Role> Get()
		{
			return _service.GetList();
		}

		// GET: api/Member/5
		public Role Get(int roleId)
		{
			Role role = _service.Get(roleId);
			
			return role;
		}

		// POST: api/Member
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

		// PUT: api/Member/5
		public void Put(int roleId, [FromBody]Role role)
		{
			Role model = _service.Get(roleId);
			
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

		// DELETE: api/Member/5
		public void Delete(int roleId)
		{
			Role model = _service.Get(roleId);

			_service.Delete(model);
		}
		
		#endregion
	}
}
