using System;
using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdBudget.Core.Models;
using NerdBudget.Core.Validators;

namespace NerdBudget.Tests.Core.Validators
{
    [TestClass]
    public class BudgetValidatorTests
    {
        #region Members

        private BudgetValidator _validator = new BudgetValidator();

        #endregion

        #region Validation Rule Tests

        [TestMethod]
        public void BudgetValidator_RuleFor_AccountId()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.AccountId, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.AccountId, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.AccountId, new string('0', 2));
            _validator.ShouldHaveValidationErrorFor(v => v.AccountId, new string('0', 2 + 1));

        }


        [TestMethod]
        public void BudgetValidator_RuleFor_Id()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Id, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.Id, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.Id, new string('0', 2));
            _validator.ShouldHaveValidationErrorFor(v => v.Id, new string('0', 2 + 1));

        }


        [TestMethod]
        public void BudgetValidator_RuleFor_CategoryId()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.CategoryId, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.CategoryId, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.CategoryId, new string('0', 2));
            _validator.ShouldHaveValidationErrorFor(v => v.CategoryId, new string('0', 2 + 1));

        }


        [TestMethod]
        public void BudgetValidator_RuleFor_Name()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Name, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.Name, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.Name, new string('0', 30));
            _validator.ShouldHaveValidationErrorFor(v => v.Name, new string('0', 30 + 1));

        }


        [TestMethod]
        public void BudgetValidator_RuleFor_Frequency()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Frequency, default(string));


            _validator.ShouldHaveValidationErrorFor(v => v.Frequency, "");
            _validator.ShouldNotHaveValidationErrorFor(v => v.Frequency, new string('0', 2));
            _validator.ShouldHaveValidationErrorFor(v => v.Frequency, new string('0', 2 + 1));

        }


        [TestMethod]
        public void BudgetValidator_RuleFor_Sequence()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Sequence, default(int));


        }


        [TestMethod]
        public void BudgetValidator_RuleFor_StartDate()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.StartDate, default(DateTime?));

            var bud = Builder<Budget>.CreateNew().Build();

            //  --
            //  RQD'd START DATES WITH A FREQUENCY
            //
            bud.StartDate = DateTime.UtcNow;

            bud.BudgetFrequency = BudgetFrequencies.W1;

            _validator.ShouldNotHaveValidationErrorFor(v => v.StartDate, bud);

            //  --
            bud.StartDate = null;

            bud.BudgetFrequency = BudgetFrequencies.W1;

            _validator.ShouldHaveValidationErrorFor(v => v.StartDate, bud);
        }


        [TestMethod]
        public void BudgetValidator_RuleFor_EndDate()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.EndDate, default(DateTime?));


        }


        [TestMethod]
        public void BudgetValidator_RuleFor_Amount()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.Amount, default(double));

            var bud = Builder<Budget>.CreateNew().Build();

            //  --
            //  RQD'd AMOUNT WITH A FREQUENCY
            //
            bud.Amount = 1;

            bud.BudgetFrequency = BudgetFrequencies.W1;

            _validator.ShouldNotHaveValidationErrorFor(v => v.Amount, bud);

            //  --
            bud.Amount = 0;

            bud.BudgetFrequency = BudgetFrequencies.W1;

            _validator.ShouldHaveValidationErrorFor(v => v.Amount, bud);

            //  --
            bud.Amount = 0;

            bud.BudgetFrequency = BudgetFrequencies.NO;

            _validator.ShouldNotHaveValidationErrorFor(v => v.Amount, bud);


        }


        [TestMethod]
        public void BudgetValidator_RuleFor_CreatedAt()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.CreatedAt, default(DateTime));


        }


        [TestMethod]
        public void BudgetValidator_RuleFor_UpdatedAt()
        {
            //	TODO
            //_validator.ShouldHaveValidationErrorFor(v => v.UpdatedAt, default(DateTime?));


        }


        #endregion
    }
}