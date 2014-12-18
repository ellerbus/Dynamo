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
	/// Represents a basic controller for Member
	///	</summary>
	public class MemberController : ApiController
	{
		#region Members
	
		private MemberService _service;
		private IValidator<Member> _validator;
		
		#endregion
		
		#region Contructors

		public MemberController(MemberService service, IValidator<Member> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Verb Actions

		// GET: api/Member
		public IEnumerable<Member> Get()
		{
			return _service.GetList();
		}

		// GET: api/Member/5
		public Member Get(int memberId)
		{
			Member member = _service.Get(memberId);
			
			return member;
		}

		// POST: api/Member
		public void Post([FromBody]Member member)
		{
			ValidationResult vr = _validator.Validate(member);

			if (vr.IsValid)
			{
				_service.Save(member);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/Member/5
		public void Put(int memberId, [FromBody]Member member)
		{
			Member model = _service.Get(memberId);
			
			model.Name = member.Name;
			model.CreatedAt = member.CreatedAt;
			model.UpdatedAt = member.UpdatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/Member/5
		public void Delete(int memberId)
		{
			Member model = _service.Get(memberId);

			_service.Delete(model);
		}
		
		#endregion
	}
}
