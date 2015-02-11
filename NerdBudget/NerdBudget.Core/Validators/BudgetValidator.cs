using FluentValidation;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Validators
{
    ///	<summary>
    ///	Represents a basic validator for Budget
    ///	</summary>
    public class BudgetValidator : AbstractValidator<Budget>
    {
        public BudgetValidator()
        {
            CascadeMode = CascadeMode.Continue;

            //	strings

            RuleFor(x => x.AccountId).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.Id).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.CategoryId).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.Name).NotNull().NotEmpty().Length(0, 30);

            RuleFor(x => x.Frequency).NotNull().NotEmpty().Length(0, 2);

            //	unique keys

            RuleFor(x => x.AccountId).NotEmpty();

            //  causes issue with ng-repeat on duplicate messages
            //  RuleFor(x => x.Name).NotEmpty();

            //	foreign keys

            //RuleFor(x => x.AccountId).NotEmpty();

            //RuleFor(x => x.CategoryId).NotEmpty();

            //  to allow for dummy buckets
            RuleFor(x => x.Amount).NotEqual(0).When(x => x.Frequency != BudgetFrequencies.NO.ToString());

            RuleFor(x => x.Amount).Equal(0).When(x => x.Frequency == BudgetFrequencies.NO.ToString());

            RuleFor(x => x.StartDate).NotNull().When(x => x.Frequency != BudgetFrequencies.NO.ToString());

        }
    }
}