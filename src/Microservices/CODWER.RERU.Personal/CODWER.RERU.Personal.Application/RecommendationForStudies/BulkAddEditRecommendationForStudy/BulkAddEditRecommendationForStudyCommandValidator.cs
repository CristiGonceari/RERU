using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.BulkAddEditRecommendationForStudy
{
    public class BulkAddEditRecommendationForStudyCommandValidator : AbstractValidator<BulkAddEditRecommendationForStudyCommand>
    {
        public BulkAddEditRecommendationForStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new RecommendationForStudyValidator(appDbContext));
        }
    }
}
