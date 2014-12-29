using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Core.Validators;

namespace Sample.Tests.Core.Validators
{
	[TestClass]
	public class MemberRoleValidatorTests
	{
		#region Members

		private MemberRoleValidator _validator = new MemberRoleValidator();

		#endregion
		
		#region Validation Rule Tests
		
		[TestMethod]
		public void MemberRoleValidator_RuleFor_MemberId()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.MemberId, default(int));
			
			
		}
		
		
		[TestMethod]
		public void MemberRoleValidator_RuleFor_RoleId()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.RoleId, default(int));
			
			
		}
		
		
		[TestMethod]
		public void MemberRoleValidator_RuleFor_CreatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.CreatedAt, default(DateTime));
			
			
		}
		
		
		#endregion
	}
}