using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelations.UpdateKinshipRelation
{
    public class UpdateKinshipRelationCommandValidator : AbstractValidator<UpdateKinshipRelationCommand> 
    {
        public UpdateKinshipRelationCommandValidator(AppDbContext appDbContext, IDateTime dateTime, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<KinshipRelation>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationValidator(appDbContext, dateTime, currentUserProvider));
        }
    }
}
