using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelations.AddKinshipRelation
{
    public class AddKinshipRelationCommandValidator : AbstractValidator<AddKinshipRelationCommand>
    {
        public AddKinshipRelationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new KinshipRelationValidator(appDbContext));
        }
    }
}
