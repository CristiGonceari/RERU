using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudyDto;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RecommendationForStudies
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
