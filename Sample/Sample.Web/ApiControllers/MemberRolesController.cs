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
	/// Represents a basic controller for MemberRole
	///	</summary>
	[RoutePrefix("api/memberRoles")]
	public class MemberRolesController : ApiController
	{
		#region Members
	
		private MemberRoleService _service;
		private IValidator<MemberRole> _validator;
		
		#endregion
		
		#region Contructors

		public MemberRolesController(MemberRoleService service, IValidator<MemberRole> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Verb Actions

		// GET: api/memberRole
		[HttpGet, Route("")]
		public IEnumerable<MemberRole> GetAll()
		{
			return _service.Get();
		}

		// GET: api/memberRole/5
		[HttpGet, Route("{memberId}/{roleId}")]
		public MemberRole Get(int memberId, int roleId)
		{
			MemberRole memberRole = _service.Get(memberId, roleId);
			
			if (memberRole == null)
			{
			
			}

			return memberRole;
		}

		// POST: api/memberRole
		[HttpPost, Route("")]
		public void Post([FromBody]MemberRole memberRole)
		{
			ValidationResult vr = _validator.Validate(memberRole);

			if (vr.IsValid)
			{
				_service.Save(memberRole);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/memberRole/5
		[HttpPut, Route("{memberId}/{roleId}")]
		public void Put(int memberId, int roleId, [FromBody]MemberRole memberRole)
		{
			MemberRole model = _service.Get(memberId, roleId);
			
			model.CreatedAt = memberRole.CreatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/memberRole/5
		[HttpDelete, Route("{memberId}/{roleId}")]
		public void Delete(int memberId, int roleId)
		{
			MemberRole model = _service.Get(memberId, roleId);

			_service.Delete(model);
		}
		
		#endregion
	}
}
