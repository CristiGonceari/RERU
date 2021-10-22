using FluentValidation.Validators;
using FluentValidation.Results;

namespace CVU.ERP.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddFail(this CustomContext context, string errorCode, string message = "")
        {
            context.AddFailure(new ValidationFailure($"{context.PropertyName}", message)
            {
                ErrorCode = errorCode
            });
        }
    }
}
