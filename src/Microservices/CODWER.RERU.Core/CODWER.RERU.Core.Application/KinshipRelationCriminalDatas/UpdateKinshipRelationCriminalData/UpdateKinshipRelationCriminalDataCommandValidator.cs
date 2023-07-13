using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.UpdateKinshipRelationCriminalData
{
    public class UpdateKinshipRelationCriminalDataCommandValidator : AbstractValidator<UpdateKinshipRelationCriminalDataCommand>
    {
        public UpdateKinshipRelationCriminalDataCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<KinshipRelationCriminalData>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationCriminalDataValidator(appDbContext, currentUserProvider));
        }
    }
}
