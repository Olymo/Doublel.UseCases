using System;
using System.Collections.Generic;

namespace Doublel.UseCases.Exceptions
{
    public class UseCaseValidationException : Exception
    {
        public UseCaseValidationException(IEnumerable<UseCaseValidationError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<UseCaseValidationError> Errors { get; }

    }

    public class UseCaseValidationError
    {
        public string PropertyName { get; set; }
        public string Error { get; set; }
    }
}
