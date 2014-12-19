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
	/// Represents a basic controller for MemberRole
	///	</summary>
	public class MemberRoleController : ApiController
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
		
		#region Verb Actions

		// GET: api/Member
		public IEnumerable<MemberRole> Get()
		{
			return _service.GetList();
		}

		// GET: api/Member/5
		public MemberRole Get(int memberId, int roleId)
		{
			MemberRole memberRole = _service.Get(memberId, roleId);
			
			return memberRole;
		}

		// POST: api/Member
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

		// PUT: api/Member/5
		public void Put(int memberId, int roleId, [FromBody]MemberRole memberRole)
		{
			MemberRole model = _service.Get(memberId, roleId);
			

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/Member/5
		public void Delete(int memberId, int roleId)
		{
			MemberRole model = _service.Get(memberId, roleId);

			_service.Delete(model);
		}
		
		#endregion
	}
}
