using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.BulkAddEditRecommendationForStudy
{
    public  class BulkAddEditRecommendationForStudyCommandValidator : AbstractValidator<BulkAddEditRecommendationForStudyCommand>
    {
        public BulkAddEditRecommendationForStudyCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new RecommendationForStudyValidator(appDbContext, currentUserProvider));
        }
    }
}
