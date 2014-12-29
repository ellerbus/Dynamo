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
	/// Represents a basic controller for Member
	///	</summary>
	[RoutePrefix("api/members")]
	public class MembersController : ApiController
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
		
		#region Verb Actions

		// GET: api/member
		[HttpGet, Route("")]
		public IEnumerable<Member> GetAll()
		{
			return _service.Get();
		}

		// GET: api/member/5
		[HttpGet, Route("{id}")]
		public Member Get(int id)
		{
			Member member = _service.Get(id);
			
			if (member == null)
			{
			
			}

			return member;
		}

		// POST: api/member
		[HttpPost, Route("")]
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

		// PUT: api/member/5
		[HttpPut, Route("{id}")]
		public void Put(int id, [FromBody]Member member)
		{
			Member model = _service.Get(id);
			
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

		// DELETE: api/member/5
		[HttpDelete, Route("{id}")]
		public void Delete(int id)
		{
			Member model = _service.Get(id);

			_service.Delete(model);
		}
		
		#endregion
	}
}
