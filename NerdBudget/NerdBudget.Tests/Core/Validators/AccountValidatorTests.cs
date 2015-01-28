using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Validators;

namespace NerdBudget.Tests.Core.Validators
{
	[TestClass]
	public class AccountValidatorTests
	{
		#region Members

		private AccountValidator _validator = new AccountValidator();

		#endregion
		
		#region Validation Rule Tests
		
		[TestMethod]
		public void AccountValidator_RuleFor_Id()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Id, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.Id, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.Id, new string('0', 2));
			_validator.ShouldHaveValidationErrorFor(v => v.Id, new string('0', 2 + 1));
			
		}
		
		
		[TestMethod]
		public void AccountValidator_RuleFor_Name()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Name, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.Name, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.Name, new string('0', 30));
			_validator.ShouldHaveValidationErrorFor(v => v.Name, new string('0', 30 + 1));
			
		}
		
		
		[TestMethod]
		public void AccountValidator_RuleFor_Type()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Type, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.Type, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.Type, new string('0', 1));
			_validator.ShouldHaveValidationErrorFor(v => v.Type, new string('0', 1 + 1));
			
		}
		
		
		[TestMethod]
		public void AccountValidator_RuleFor_StartedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.StartedAt, default(DateTime));
			
			
		}
		
		
		[TestMethod]
		public void AccountValidator_RuleFor_CreatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.CreatedAt, default(DateTime));
			
			
		}
		
		
		[TestMethod]
		public void AccountValidator_RuleFor_UpdatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.UpdatedAt, default(DateTime?));
			
			
		}
		
		
		#endregion
	}
}