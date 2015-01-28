using FluentValidation.Results;

namespace NerdBudget.Tests.Web.ApiControllers
{
    public class ControllerTestHelper<TModel>
        where TModel : class
    {
        protected AnyHelper<TModel> Any = new AnyHelper<TModel>();

        public ValidationResult ValidationSuccess { get { return new ValidationResult(); } }

        public ValidationResult ValidationFailure
        {
            get
            {
                ValidationFailure[] failures = new[] { new ValidationFailure("", "Oh NO!") };

                return new ValidationResult(failures);
            }
        }
    }
}
