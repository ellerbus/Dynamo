using FluentValidation;
using FluentValidation.Results;

namespace ShortFuze.Core
{
    static class ValidationExtensions
    {
        public static void That(bool condition, string message, string property = "")
        {
            if (!condition)
            {
                ValidationFailure f = new ValidationFailure(property, message);

                throw new ValidationException(new[] { f });
            }
        }

        public static void ValidateAndThrow<T>(this IValidator<T> validator, T instance)
        {
            ValidationResult result = validator.Validate(instance);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
