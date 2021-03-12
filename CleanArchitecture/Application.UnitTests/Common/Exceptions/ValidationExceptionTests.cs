using Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Test]
        public void SingleValidationFailureCreateASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Age", "Must Be Over 18"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "Age" });
            actual["Age"].Should().BeEquivalentTo(new string[] { "Must Be Over 18" });
        }

        [Test]
        public void MultipleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultiplevalues()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Age", "Must be 18 or Older"),
                new ValidationFailure("Age", "Must be 18 or Younger"),
                new ValidationFailure("Password", "must contain at least 8 characters"),
                new ValidationFailure("Password", "must contain a digit"),
                new ValidationFailure("Password", "must contain upper case letter"),
                new ValidationFailure("Password", "must contain lower case letter"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

            actual["Age"].Should().BeEquivalentTo(new string[]
            {
                "Must be 18 or Older",
                "Must be 18 or Younger"
            });

            actual["Password"].Should().BeEquivalentTo(new string[]
            {
                "must contain at least 8 characters",
                "must contain a digit",
                "must contain upper case letter",
                "must contain lower case letter",
            });
        }
    }
}
