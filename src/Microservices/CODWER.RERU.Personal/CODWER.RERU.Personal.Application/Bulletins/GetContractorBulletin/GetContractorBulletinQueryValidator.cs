using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Bulletins.GetContractorBulletin
{
    public class GetContractorBulletinQueryValidator: AbstractValidator<GetContractorBulletinQuery>
    {
        private readonly AppDbContext _appDbContext;
        public GetContractorBulletinQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.ContractorId).Custom(Validate);

        }

        private void Validate(int contractorId, CustomContext context)
        {
            var existent = _appDbContext.Bulletins.FirstOrDefault(x => x.ContractorId == contractorId);

            if (existent == null)
            {
                context.AddFail(ValidationCodes.BULLETIN_NOT_FOUND, ValidationMessages.NotFound);
            }
        }
    }
}
