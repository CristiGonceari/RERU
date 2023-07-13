using CVU.ERP.Common;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelations.AddKinshipRelation
{
    public class AddKinshipRelationCommandValidator : AbstractValidator<AddKinshipRelationCommand>
    {
        public AddKinshipRelationCommandValidator(AppDbContext appDbContext, IDateTime dateTime, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data).SetValidator(new KinshipRelationValidator(appDbContext, dateTime, currentUserProvider));
        }
    }
}
