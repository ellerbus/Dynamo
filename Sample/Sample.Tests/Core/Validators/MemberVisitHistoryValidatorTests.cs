using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Core.Validators;

namespace Sample.Tests.Core.Validators
{
	[TestClass]
	public class MemberVisitHistoryValidatorTests
	{
		#region Members

		private MemberVisitHistoryValidator _validator = new MemberVisitHistoryValidator();

		#endregion
		
		#region Validation Rule Tests
		
		[TestMethod]
		public void MemberVisitHistoryValidator_RuleFor_MemberId()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.MemberId, default(int));
			
			
		}
		
		
		[TestMethod]
		public void MemberVisitHistoryValidator_RuleFor_VisitedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.VisitedAt, default(DateTime));
			
			
		}
		
		
		[TestMethod]
		public void MemberVisitHistoryValidator_RuleFor_PageUrl()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.PageUrl, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.PageUrl, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.PageUrl, new string('0', 255));
			_validator.ShouldHaveValidationErrorFor(v => v.PageUrl, new string('0', 255 + 1));
			
		}
		
		
		#endregion
	}
}