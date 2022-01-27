using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.IdentityDocuments;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Bulletins.UpdateBulletin
{
    public class UpdateBulletinCommandValidator : AbstractValidator<UpdateBulletinCommand>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateBulletinCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Bulletin>(appDbContext, ValidationCodes.EMPTY_BULLETIN_EMITTER,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data)
                .SetValidator(new BulletinValidator());

            //RuleFor(x => x.Data)
            //    .Custom(CheckIfUniqueIdnpOnUpdate);
        }

        private void CheckIfUniqueIdnpOnUpdate(BulletinsDataDto dto, CustomContext context)
        {
            var exist = _appDbContext.Bulletins.Any(x => x.Idnp == dto.Idnp
                                                        && x.ContractorId != dto.ContractorId);

            if (exist)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}
