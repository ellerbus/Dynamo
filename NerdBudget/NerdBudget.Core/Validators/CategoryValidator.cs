using FluentValidation;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Validators
{
    ///	<summary>
    ///	Represents a basic validator for Category
    ///	</summary>
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            CascadeMode = CascadeMode.Continue;

            //	strings

            RuleFor(x => x.AccountId).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.Id).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.Name).NotNull().NotEmpty().Length(0, 30);

            RuleFor(x => x.Multiplier).Must(BeSignOnly);

            //	unique keys

            //	foreign keys

            //RuleFor(x => x.AccountId).NotEmpty();

        }

        private bool BeSignOnly(int arg)
        {
            if (arg == -1 || arg == 1)
            {
                return true;
            }

            return false;
        }
    }
}