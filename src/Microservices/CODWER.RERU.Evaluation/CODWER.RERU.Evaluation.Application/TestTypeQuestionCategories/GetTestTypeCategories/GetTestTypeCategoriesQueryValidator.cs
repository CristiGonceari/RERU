using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.GetTestTypeCategories
{
    public class GetTestTypeCategoriesQueryValidator : AbstractValidator<GetTestTypeCategoriesQuery>
    {
        public GetTestTypeCategoriesQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);
        }
    }
}
