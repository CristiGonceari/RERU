using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.Bulletins.AddBulletin
{
    public class AddBulletinCommandValidator : AbstractValidator<AddBulletinCommand>
    {
        public readonly AppDbContext _appDbContext;

        public AddBulletinCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .SetValidator(new BulletinValidator());

            RuleFor(x => x.Data.UserProfileId)
                .Custom(CheckIfContractorHasBulletin);
        }
        private void CheckIfContractorHasBulletin(int userProfileId, CustomContext context)
        {
            var exist = _appDbContext.Bulletins.Any(x => x.UserProfileId == userProfileId);

            if (exist)
            {
                context.AddFail(ValidationCodes.CONTRACTOR_HAS_BULLETIN, ValidationMessages.InvalidReference);
            }
        }
    }
}
