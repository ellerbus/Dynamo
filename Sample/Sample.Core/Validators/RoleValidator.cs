using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Sample.Core.Models;

namespace Sample.Core.Validators
{
	///	<summary>
	///	Represents a basic validator for Role
	///	</summary>
	public class RoleValidator : AbstractValidator<Role>
	{
		public RoleValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;
			
			RuleFor(x => x.Name).NotNull().NotEmpty().Length(0, 50);
					
			//	unique keys
			
			RuleFor(x => x.Name).NotEmpty();
					
			//	foreign keys
			
		}
	}
}