using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.Files;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Vacations.UpdateVacationFile
{
    public class UpdateVacationFileCommandValidator : AbstractValidator<UpdateVacationFileCommand>
    {
        public UpdateVacationFileCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.VacationId)
                .SetValidator(new ItemMustExistValidator<Vacation>(appDbContext, ValidationCodes.VACATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data.NewFile).SetValidator(new FormFileValidator(ValidationMessages.InvalidInput));
        }
    }
}
