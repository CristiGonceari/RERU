using CVU.ERP.Common;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelations.AddKinshipRelation
{
    public class AddKinshipRelationCommandValidator : AbstractValidator<AddKinshipRelationCommand>
    {
        public AddKinshipRelationCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            RuleFor(x => x.Data).SetValidator(new KinshipRelationValidator(appDbContext, dateTime));
        }
    }
}
