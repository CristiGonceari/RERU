using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.AddRecommendationForStudy
{
    public class AddRecommendationForStudiesCommandValidation : AbstractValidator<AddRecommendationForStudiesCommand>
    {
        public AddRecommendationForStudiesCommandValidation(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data).SetValidator(new RecommendationForStudyValidator(appDbContext, currentUserProvider));
        }
    }
}
