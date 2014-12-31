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
	/// Represents a basic controller for MemberRole
	///	</summary>
	[RoutePrefix("api/memberRoles")]
	public class MemberRolesController : ApiController
	{
		#region Members
	
		private IMemberRoleService _service;
		private IValidator<MemberRole> _validator;
		
		#endregion
		
		#region Contructors

		public MemberRolesController(IMemberRoleService service, IValidator<MemberRole> validator)
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
		[HttpGet, Route("{memberId}/{roleId}"), ResponseType(typeof(MemberRole))]
		public IHttpActionResult Get(int memberId, int roleId)
		{
			MemberRole memberRole = _service.Get(memberId, roleId);
			
			if (memberRole == null)
			{
				return NotFound();
			}

			return Ok(memberRole);
		}

		// POST: api/memberRole
		[HttpPost, Route("")]
		public IHttpActionResult Post([FromBody]MemberRole memberRole)
		{
			ValidationResult vr = _validator.Validate(memberRole);

			if (vr.IsValid)
			{
				_service.Insert(memberRole);

				return Ok();
			}
			
			return BadRequest(vr);
		}

		// PUT: api/memberRole/5
		[HttpPut, Route("{memberId}/{roleId}")]
		public IHttpActionResult Put(int memberId, int roleId, [FromBody]MemberRole memberRole)
		{
			MemberRole model = _service.Get(memberId, roleId);

			if (model == null)
			{
				return NotFound();
			}
			
			model.CreatedAt = memberRole.CreatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Update(model);

				return Ok();
			}
			
			return BadRequest(vr);
		}

		// DELETE: api/memberRole/5
		[HttpDelete, Route("{memberId}/{roleId}")]
		public IHttpActionResult Delete(int memberId, int roleId)
		{
			MemberRole model = _service.Get(memberId, roleId);

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
