using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Personal.Application.Bulletins.CreateBulletin
{
    public class CreateBulletinCommandValidator : AbstractValidator<CreateBulletinCommand>
    {
        private readonly AppDbContext _appDbContext;
        public CreateBulletinCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data)
                .SetValidator(new BulletinValidator());

            RuleFor(x => x.Data.ContractorId)
                .Custom(CheckIfContractorHasBulletin);

            //RuleFor(x => x.Data.Idnp)
            //    .Custom(CheckIfUniqueIdnpOnCreate);
        }

        private void CheckIfContractorHasBulletin(int contractorId, CustomContext context)
        {
            var exist = _appDbContext.Bulletins.Any(x => x.ContractorId == contractorId);

            if (exist)
            {
                context.AddFail(ValidationCodes.CONTRACTOR_HAS_BULLETIN, ValidationMessages.InvalidReference);
            }
        }

        private void CheckIfUniqueIdnpOnCreate(string idnp, CustomContext context)
        {
            var exist = _appDbContext.Bulletins.Any(x => x.Idnp == idnp);

            if (exist)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}