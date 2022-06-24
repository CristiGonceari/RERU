using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelations.AddKinshipRelation
{
    public class AddKinshipRelationCommandValidator : AbstractValidator<AddKinshipRelationCommand>
    {
        public AddKinshipRelationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new KinshipRelationValidator(appDbContext));
        }
    }
}
