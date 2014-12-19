using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Sample.Core.Models;

namespace Sample.Core.Validators
{
	///	<summary>
	///	Represents a basic validator for MemberRole
	///	</summary>
	public class MemberRoleValidator : AbstractValidator<MemberRole>
	{
		public MemberRoleValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;
					
			//	unique keys
					
			//	foreign keys
			
			//RuleFor(x => x.Member).NotEmpty();
			
			//RuleFor(x => x.Role).NotEmpty();
			
		}
	}
}