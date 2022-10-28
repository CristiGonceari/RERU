using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Validators.TestValidators;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestionsSummary
{
    public class GetTestQuestionsSummaryQueryValidator : AbstractValidator<GetTestQuestionsSummaryQuery>
    {
        public GetTestQuestionsSummaryQueryValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .SetValidator(x => new TestCurrentUserValidator(userProfileService, appDbContext, ValidationCodes.INVALID_USER));
        }
    }
}
