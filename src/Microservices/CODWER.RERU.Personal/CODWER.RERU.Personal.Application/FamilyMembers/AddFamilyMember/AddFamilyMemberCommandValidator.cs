using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.FamilyMembers.AddFamilyMember
{
    class AddFamilyMemberCommandValidator : AbstractValidator<AddFamilyMemberCommand>
    {
        public AddFamilyMemberCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new FamilyMemberValidator(appDbContext));
        }
    }
}
