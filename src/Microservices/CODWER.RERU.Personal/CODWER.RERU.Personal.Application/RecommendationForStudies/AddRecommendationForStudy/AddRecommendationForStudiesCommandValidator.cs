using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.AddRecommendationForStudy
{
    public class AddRecommendationForStudiesCommandValidation : AbstractValidator<AddRecommendationForStudiesCommand>
    {
        public AddRecommendationForStudiesCommandValidation(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new RecommendationForStudyValidator(appDbContext));
        }
    }
}
