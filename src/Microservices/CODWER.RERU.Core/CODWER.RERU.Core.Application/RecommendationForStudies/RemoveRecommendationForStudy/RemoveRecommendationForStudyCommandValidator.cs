using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.RemoveRecommendationForStudy
{
    public class RemoveRecommendationForStudyCommandValidator : AbstractValidator<RemoveRecommendationForStudyCommand>
    {
        public RemoveRecommendationForStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.RecommendationForStudyId)
                .SetValidator(new ItemMustExistValidator<RecommendationForStudy>(appDbContext, ValidationCodes.RECOMANDATION_NOT_FOUND, ValidationMessages.NotFound));

        }
    }
}
