using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ShortFuze.Tests
{
    public class AnyValidation
    {
        public static ValidationResult Success { get { return new ValidationResult(); } }

        public static ValidationResult Failure { get { return new ValidationResult(new[] { new ValidationFailure("", "Failed!") }); } }

        public static ValidationException FailureException { get { return new ValidationException(new[] { new ValidationFailure("", "Failed!") }); } }
    }

    public class Any
    {
        public static DateTime DateTime { get { return It.IsAny<DateTime>(); } }

        public static string String { get { return It.IsAny<string>(); } }

        public static IEnumerable<string> Strings { get { return It.IsAny<IEnumerable<string>>(); } }

        public static int Integer { get { return It.IsAny<int>(); } }
    }
}
