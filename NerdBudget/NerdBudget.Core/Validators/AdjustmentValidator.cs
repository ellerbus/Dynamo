using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Validators
{
    ///	<summary>
    ///	Represents a basic validator for Adjustment
    ///	</summary>
    public class AdjustmentValidator : AbstractValidator<Adjustment>
    {
        public AdjustmentValidator()
        {
            CascadeMode = CascadeMode.Continue;

            //	strings
            
            RuleFor(x => x.AccountId).NotNull().NotEmpty().Length(0, 2);
            
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.BudgetId).NotNull().NotEmpty().Length(0, 2);

            RuleFor(x => x.Name).NotNull().NotEmpty().Length(0, 60);

            RuleFor(x => x.Amount).NotEqual(0);
                    
            //	unique keys
                    
            //	foreign keys
            
            //RuleFor(x => x.AccountId).NotEmpty();
            
            //RuleFor(x => x.BudgetId).NotEmpty();
            
        }
    }
}