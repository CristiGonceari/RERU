using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas
{
    public class KinshipRelationCriminalDataValidator : AbstractValidator<KinshipRelationCriminalDataDto>
    {
        public KinshipRelationCriminalDataValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }
    }
}
