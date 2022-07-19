using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.TimeSheetTables
{
   public class TimeSheetTableValidator : AbstractValidator<AddEditTimeSheetTableDto>
    {
        public TimeSheetTableValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Date).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));

            When(x => x.ValueId != null, () =>
            {
                RuleFor(x => (int) x.ValueId)
                    .SetValidator(new ExistInEnumValidator<TimeSheetValueEnum>());
            });
        }
    }
}
