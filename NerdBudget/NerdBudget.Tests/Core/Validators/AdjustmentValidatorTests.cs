using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Validators;

namespace NerdBudget.Tests.Core.Validators
{
	[TestClass]
	public class AdjustmentValidatorTests
	{
		#region Members

		private AdjustmentValidator _validator = new AdjustmentValidator();

		#endregion
		
		#region Validation Rule Tests
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_AccountId()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.AccountId, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.AccountId, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.AccountId, new string('0', 2));
			_validator.ShouldHaveValidationErrorFor(v => v.AccountId, new string('0', 2 + 1));
			
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_Id()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Id, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.Id, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.Id, new string('0', 2));
			_validator.ShouldHaveValidationErrorFor(v => v.Id, new string('0', 2 + 1));
			
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_BudgetId()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.BudgetId, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.BudgetId, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.BudgetId, new string('0', 2));
			_validator.ShouldHaveValidationErrorFor(v => v.BudgetId, new string('0', 2 + 1));
			
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_Name()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Name, default(string));
			
			
			_validator.ShouldHaveValidationErrorFor(v => v.Name, "");
			_validator.ShouldNotHaveValidationErrorFor(v => v.Name, new string('0', 60));
			_validator.ShouldHaveValidationErrorFor(v => v.Name, new string('0', 60 + 1));
			
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_Date()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.Date, default(DateTime?));
			
			
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_Amount()
		{
			//	TODO
            _validator.ShouldHaveValidationErrorFor(v => v.Amount, default(double));

            _validator.ShouldNotHaveValidationErrorFor(v => v.Amount, -1);
            _validator.ShouldNotHaveValidationErrorFor(v => v.Amount, 1);
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_CreatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.CreatedAt, default(DateTime));
			
			
		}
		
		
		[TestMethod]
		public void AdjustmentValidator_RuleFor_UpdatedAt()
		{
			//	TODO
			//_validator.ShouldHaveValidationErrorFor(v => v.UpdatedAt, default(DateTime?));
			
			
		}
		
		
		#endregion
	}
}