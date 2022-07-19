using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMember
{
    public class GetFamilyMemberQueryValidator : AbstractValidator<GetFamilyMemberQuery>
    {
        public GetFamilyMemberQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<FamilyMember>(appDbContext, ValidationCodes.FAMILY_MEMBER_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
