using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions
{
    public class EmployeeFunctionsValidator : AbstractValidator<EmployeeFunctionDto>
    {
        public EmployeeFunctionsValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
