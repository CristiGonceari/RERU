using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorTimeSheetTable
{
   public class GetTimeSheetTableQueryValidator : AbstractValidator<GetTimeSheetTableQuery>
    {
        public GetTimeSheetTableQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(_ => new ContractorLocalPermission { GetTimeSheetTableData = true })
                .SetValidator(new LocalPermissionValidator(userProfileService, appDbContext));
        }
    }
}
