using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Validators;

namespace NerdBudget.Tests.Core.Validators
{
    [TestClass]
    public class CategoryValidatorTests
    {
        #region Members

        private CategoryValidator _validator = new CategoryValidator();

        #endregion

        #region Validation Rule Tests

        [TestMethod]
        public void CategoryValidator_RuleFor_AccountId()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.AccountId, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.AccountId, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.AccountId, new string('0', 2));
            _validator.ShouldHaveValidationErrorFor(v => v.AccountId, new string('0', 2 + 1));

        }


        [TestMethod]
        public void CategoryValidator_RuleFor_Id()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Id, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.Id, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.Id, new string('0', 2));
            _validator.ShouldHaveValidationErrorFor(v => v.Id, new string('0', 2 + 1));

        }


        [TestMethod]
        public void CategoryValidator_RuleFor_Name()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Name, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.Name, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.Name, new string('0', 30));
            _validator.ShouldHaveValidationErrorFor(v => v.Name, new string('0', 30 + 1));

        }


        [TestMethod]
        public void CategoryValidator_RuleFor_Multiplier()
        {
            //	TODO
            _validator.ShouldHaveValidationErrorFor(v => v.Multiplier, default(int));

            _validator.ShouldNotHaveValidationErrorFor(v => v.Multiplier, -1);
            _validator.ShouldNotHaveValidationErrorFor(v => v.Multiplier, 1);
        }


        [TestMethod]
        public void CategoryValidator_RuleFor_Sequence()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Sequence, default(int));


        }


        [TestMethod]
        public void CategoryValidator_RuleFor_CreatedAt()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.CreatedAt, default(DateTime));


        }


        [TestMethod]
        public void CategoryValidator_RuleFor_UpdatedAt()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.UpdatedAt, default(DateTime?));


        }


        #endregion
    }
}