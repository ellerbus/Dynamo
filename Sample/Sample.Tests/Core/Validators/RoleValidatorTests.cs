using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Core.Validators;

namespace Sample.Tests.Core.Validators
{
	[TestClass]
	public class RoleValidatorTests
	{
		#region Members

		private RoleValidator _validator = new RoleValidator();

		#endregion
		
		#region Validation Rule Tests
		
		[TestMethod]
		public void RoleValidator_RuleFor_Id()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Id, default(int));
			
			
		}
		
		
		[TestMethod]
		public void RoleValidator_RuleFor_Name()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Name, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.Name, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.Name, new string('0', 50));
			_validator.ShouldHaveValidationErrorFor(v => v.Name, new string('0', 50 + 1));
			
		}
		
		
		[TestMethod]
		public void RoleValidator_RuleFor_CreatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.CreatedAt, default(DateTime));
			
			
		}
		
		
		[TestMethod]
		public void RoleValidator_RuleFor_UpdatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.UpdatedAt, default(DateTime?));
			
			
		}
		
		
		#endregion
	}
}