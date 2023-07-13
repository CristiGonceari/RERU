using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.Bulletins.AddBulletin
{
    public class AddBulletinCommandValidator : AbstractValidator<AddBulletinCommand>
    {
        public readonly AppDbContext _appDbContext;

        public AddBulletinCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data)
                .SetValidator(new BulletinValidator(appDbContext, currentUserProvider));

            RuleFor(x => x.Data.ContractorId)
                .Custom(CheckIfContractorHasBulletin);
        }
        private void CheckIfContractorHasBulletin(int contractorId, CustomContext context)
        {
            var exist = _appDbContext.Bulletins.Any(x => x.ContractorId == contractorId);

            if (exist)
            {
                context.AddFail(ValidationCodes.CONTRACTOR_HAS_BULLETIN, ValidationMessages.InvalidReference);
            }
        }
    }
}
