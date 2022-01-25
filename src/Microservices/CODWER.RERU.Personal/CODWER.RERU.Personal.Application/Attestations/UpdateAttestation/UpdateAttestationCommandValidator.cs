using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Attestations.UpdateAttestation
{
    public class UpdateAttestationCommandValidator : AbstractValidator<UpdateAttestationCommand>
    {
        public UpdateAttestationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Attestation>(appDbContext, ValidationCodes.ATTESTATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new AttestationValidator(appDbContext));
        }
    }
}
