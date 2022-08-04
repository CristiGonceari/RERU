using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelations.RemoveKinshipRelation
{
    public class RemoveKinshipRelationCommandValidator : AbstractValidator<RemoveKinshipRelationCommand>
    {
        public RemoveKinshipRelationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<KinshipRelation>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
