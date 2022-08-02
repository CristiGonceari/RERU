using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.UpdateRecommendationForStudy
{
    public class UpdateRecommendationForStudyCommandValidator : AbstractValidator<UpdateRecommendationForStudyCommand>
    {
        public UpdateRecommendationForStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<RecommendationForStudy>(appDbContext, ValidationCodes.RECOMANDATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new RecommendationForStudyValidator(appDbContext));

        }
    }
}
