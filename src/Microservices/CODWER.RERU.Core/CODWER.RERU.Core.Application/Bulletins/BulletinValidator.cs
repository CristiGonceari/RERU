using CODWER.RERU.Core.Application.Addresses;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Bulletins
{
    public class BulletinValidator : AbstractValidator<BulletinDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public BulletinValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

            RuleFor(x => x.Series)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_BULLETIN_SERIES)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.EmittedBy)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_BULLETIN_EMITTER)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.BirthPlace)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.ParentsResidenceAddress)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.ResidenceAddress)
                .SetValidator(new AddressValidator());

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.ContractorId).Result)
               .WithErrorCode(ValidationCodes.USER_NOT_FOUND);
        }

        private async Task<bool> CheckIfCurrentUser(int id)
        {
            var currentUser = await _currentUserProvider.Get();
            var contractor = _appDbContext.Contractors.Include(up => up.UserProfile).FirstOrDefault(up => up.UserProfileId.ToString() == currentUser.Id);

            if (contractor.Id == id)
            {
                return true;
            }

            return false;
        }
    }
}
