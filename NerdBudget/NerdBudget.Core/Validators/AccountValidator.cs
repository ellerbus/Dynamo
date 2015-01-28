using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Validators
{
	///	<summary>
	///	Represents a basic validator for Account
	///	</summary>
	public class AccountValidator : AbstractValidator<Account>
	{
		public AccountValidator()
		{
			CascadeMode = CascadeMode.Continue;

			//	strings
			
			RuleFor(x => x.Id).NotNull().NotEmpty().Length(0, 2);
			
			RuleFor(x => x.Name).NotNull().NotEmpty().Length(0, 30);
			
			RuleFor(x => x.Type).NotNull().NotEmpty().Length(0, 1);
					
			//	unique keys
					
			//	foreign keys
			
		}
	}
}