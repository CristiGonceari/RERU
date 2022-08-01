using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas
{
    public class KinshipRelationWithCriminalDataValidator : AbstractValidator<KinshipRelationWithCriminalDataDto>
    {
        public KinshipRelationWithCriminalDataValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }
    }
}
