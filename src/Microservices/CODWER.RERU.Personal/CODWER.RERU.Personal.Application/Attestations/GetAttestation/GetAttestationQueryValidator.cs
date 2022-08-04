using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Attestations.GetAttestation
{
    public class GetAttestationQueryValidator :AbstractValidator<GetAttestationQuery>
    {
        public GetAttestationQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Attestation>(appDbContext,ValidationCodes.ATTESTATION_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
