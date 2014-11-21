using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Sample.Core.Models;

namespace Sample.Core.Validators
{
	///	<summary>
	///	Represents a basic validator for MemberVisitHistory
	///	</summary>
	public class MemberVisitHistoryValidator : AbstractValidator<MemberVisitHistory>
	{
		public MemberVisitHistoryValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;
			
			RuleFor(x => x.PageUrl).NotNull().NotEmpty().Length(0, 255);
					
			//	unique keys
					
			//	foreign keys
			
			//RuleFor(x => x.Member).NotEmpty();
			
		}
	}
}