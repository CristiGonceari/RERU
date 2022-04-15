using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.AssignLocationComputer
{
    public class AssignLocationComputerCommandValidator : AbstractValidator<AssignLocationComputerCommand>
    {
       public AssignLocationComputerCommandValidator(AppDbContext appDbContext) 
        {
            RuleFor(r => r.Data.LocationId)
                .NotNull()
                .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

            RuleFor(r => r.Data.Number)
                .GreaterThan(0)
                .WithErrorCode(ValidationCodes.INVALID_COMPUTER_NUMBER);
        }
    }
}
