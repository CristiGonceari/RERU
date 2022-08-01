using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies
{
    public class RecommendationForStudyValidator : AbstractValidator<RecommendationForStudyDto>
    {
        public RecommendationForStudyValidator(AppDbContext appDbContext)
        {
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

        }
    }
}
