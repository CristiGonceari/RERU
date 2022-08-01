using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.UpdateKinshipRelationWithCriminalData
{
    public class UpdateKinshipRelationWithCriminalDataCommandValidator : AbstractValidator<UpdateKinshipRelationWithCriminalDataCommand>
    {
        public UpdateKinshipRelationWithCriminalDataCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<KinshipRelationCriminalData>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationWithCriminalDataValidator(appDbContext));
        }
    }
}
