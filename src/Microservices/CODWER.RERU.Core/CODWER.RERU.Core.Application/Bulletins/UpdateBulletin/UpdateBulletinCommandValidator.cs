using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.Bulletins.UpdateBulletin
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
        }

        private void CheckIfUniqueIdnpOnUpdate(BulletinDto dto, CustomContext context)
        {
            var existBulletin = _appDbContext.Bulletins.Include(b => b.UserProfile).Any(x => x.UserProfile.Idnp == dto.Idnp
                                                        && x.UserProfileId != dto.UserProfileId);
             
            
            if (existBulletin)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}
