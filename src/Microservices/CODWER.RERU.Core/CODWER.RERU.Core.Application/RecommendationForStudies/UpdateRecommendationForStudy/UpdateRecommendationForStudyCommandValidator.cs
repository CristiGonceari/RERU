using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.UpdateRecommendationForStudy
{
    public class UpdateRecommendationForStudyCommandValidator : AbstractValidator<UpdateRecommendationForStudyCommand>
    {
        public UpdateRecommendationForStudyCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<RecommendationForStudy>(appDbContext, ValidationCodes.RECOMANDATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new RecommendationForStudyValidator(appDbContext, currentUserProvider));

        }
    }
}
