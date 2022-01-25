using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Attestations.AddAttestation
{
    public class AddAttestationCommandValidator : AbstractValidator<AddAttestationCommand>
    {
        public AddAttestationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new AttestationValidator(appDbContext));
        }
    }
}
