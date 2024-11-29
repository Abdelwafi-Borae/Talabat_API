using Data.Common.Extensions;
using FluentValidation.Results;

namespace Data.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string key, string message)
          : base("One or more validation failures have occurred.")
        {
            var result = new Dictionary<string, string[]>();
            result.Add(key.ToCamelCase(), new string[] { message });

            Errors = result;
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key.ToCamelCase(), failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}