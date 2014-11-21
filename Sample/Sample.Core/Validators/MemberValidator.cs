using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Sample.Core.Models;

namespace Sample.Core.Validators
{
	///	<summary>
	///	Represents a basic validator for Member
	///	</summary>
	public class MemberValidator : AbstractValidator<Member>
	{
		public MemberValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;
			
			RuleFor(x => x.Name).NotNull().NotEmpty().Length(0, 50);
					
			//	unique keys
			
			RuleFor(x => x.Name).NotEmpty();
					
			//	foreign keys
			
		}
	}
}