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
	/// Represents a basic controller for MemberVisitHistory
	///	</summary>
	public class MemberVisitHistoryController : ApiController
	{
		#region Members
	
		private MemberVisitHistoryService _service;
		private IValidator<MemberVisitHistory> _validator;
		
		#endregion
		
		#region Contructors

		public MemberVisitHistoryController(MemberVisitHistoryService service, IValidator<MemberVisitHistory> validator)
		{
			_service = service;
			_validator = validator;
		}
		
		#endregion
		
		#region Verb Actions

		// GET: api/Member
		public IEnumerable<MemberVisitHistory> Get()
		{
			return _service.GetList();
		}

		// GET: api/Member/5
		public MemberVisitHistory Get(int memberId, DateTime visitedAt)
		{
			MemberVisitHistory memberVisitHistory = _service.Get(memberId, visitedAt);
			
			return memberVisitHistory;
		}

		// POST: api/Member
		public void Post([FromBody]MemberVisitHistory memberVisitHistory)
		{
			ValidationResult vr = _validator.Validate(memberVisitHistory);

			if (vr.IsValid)
			{
				_service.Save(memberVisitHistory);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/Member/5
		public void Put(int memberId, DateTime visitedAt, [FromBody]MemberVisitHistory memberVisitHistory)
		{
			MemberVisitHistory model = _service.Get(memberId, visitedAt);
			
			model.PageUrl = memberVisitHistory.PageUrl;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/Member/5
		public void Delete(int memberId, DateTime visitedAt)
		{
			MemberVisitHistory model = _service.Get(memberId, visitedAt);

			_service.Delete(model);
		}
		
		#endregion
	}
}
