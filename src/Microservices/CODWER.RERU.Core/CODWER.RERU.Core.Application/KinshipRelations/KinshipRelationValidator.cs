using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelations
{
    public class KinshipRelationValidator : AbstractValidator<KinshipRelationDto>
    {
        public KinshipRelationValidator(AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.KinshipDegree)
               .SetValidator(new ExistInEnumValidator<KinshipDegreeEnum>());

            RuleFor(x => x.Name)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.LastName)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_LAST_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.BirthDate)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.INVALID_INPUT)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.BirthLocation)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_BIRTH_LOCATION)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Function)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_FUNCTION)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.WorkLocation)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_WORK_LOCATION)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.ResidenceAddress)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_RESIDENCE_ADDRESS)
                   .WithMessage(ValidationMessages.InvalidInput);

        }
    }
}
