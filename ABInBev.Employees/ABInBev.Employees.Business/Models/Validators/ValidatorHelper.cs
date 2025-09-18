using FluentValidation;
using FluentValidation.Results;

namespace ABInBev.Employees.Business.Models.Validators
{
    public static class ValidatorHelper
    {
        public static void Validate<T>(AbstractValidator<T> validator, T entity)
        {
            var results = validator.Validate(entity);

            var errors = new Dictionary<string, string>();
            foreach (var error in results.Errors)
            {
                errors.Add(error.PropertyName, error.ErrorMessage);
            }

            throw new InvalidOperationException(errors.ToString());
        }

        public static Dictionary<string, string> GetErrors(ValidationResult result)
        {
            var errors = new Dictionary<string, string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.PropertyName, error.ErrorMessage);
            }

            return errors;
        }
    }
}
