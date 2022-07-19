using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.FamilyMembers.RemoveFamilyMember
{
    public class RemoveFamilyMemberCommandValidator : AbstractValidator<RemoveFamilyMemberCommand>
    {
        public RemoveFamilyMemberCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<FamilyMember>(appDbContext, ValidationCodes.FAMILY_MEMBER_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
