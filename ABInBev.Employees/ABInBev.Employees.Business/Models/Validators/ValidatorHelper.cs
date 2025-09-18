using FluentValidation;
using FluentValidation.Results;

namespace ABInBev.Employees.Business.Models.Validators
{
    public static class ValidatorHelper
    {
        public static async Task ValidateAsync<T>(AbstractValidator<T> validator, T entity)
        {
            var results = await validator.ValidateAsync(entity);

            if (results.Errors.Count != 0)
            {
                var errorMessage = string.Empty;
                results.Errors.ForEach(x => errorMessage += $"{x.PropertyName}: {x.ErrorMessage}");

                throw new InvalidOperationException(errorMessage);
            }
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
