using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using CVU.ERP.Common.Extensions;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.Application.Validators;

namespace CODWER.RERU.Personal.Application.UserProfiles.ResetContractorPassword
{
    public class ResetContractorPasswordCommandValidator : AbstractValidator<ResetContractorPasswordCommand>
    {
        private readonly AppDbContext _appDbContext;

        public ResetContractorPasswordCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));


            RuleFor(x => x.ContractorId)
                .Custom(ExistentUserProfile);
        }

        private void ExistentUserProfile(int contractorId, CustomContext context)
        {
            var exist = _appDbContext.UserProfiles.Any(x => x.Contractor.Id == contractorId);

            if (!exist)
            {
                context.AddFail(ValidationCodes.NONEXISTENT_USER_PROFILE, ValidationMessages.InvalidReference);
            }
        }
    }
}