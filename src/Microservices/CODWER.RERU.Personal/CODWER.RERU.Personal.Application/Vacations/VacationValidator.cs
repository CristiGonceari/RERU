using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Vacations
{
    public class VacationValidator : AbstractValidator<AddEditVacationDto>
    {
        public VacationValidator(AppDbContext appDbContext)
        {
            //RuleFor(x => x.Reason).NotEmpty()
            //    .WithMessage(ValidationMessages.InvalidInput)
            //    .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.FromDate).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.ToDate).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

        }
    }
}
