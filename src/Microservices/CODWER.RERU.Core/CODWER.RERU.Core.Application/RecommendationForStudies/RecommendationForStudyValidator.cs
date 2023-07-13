using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudy;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.RecommendationForStudies
{
    public class RecommendationForStudyValidator : AbstractValidator<RecommendationForStudyDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public RecommendationForStudyValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

            RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_RECOMANDATION_NAME)
                    .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.LastName)
                   .NotEmpty()
                   .WithErrorCode(ValidationCodes.EMPTY_RECOMANDATION_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Function)
                   .NotEmpty()
                   .WithErrorCode(ValidationCodes.EMPTY_RECOMANDATION_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Subdivision)
                   .NotEmpty()
                   .WithErrorCode(ValidationCodes.EMPTY_RECOMANDATION_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

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
