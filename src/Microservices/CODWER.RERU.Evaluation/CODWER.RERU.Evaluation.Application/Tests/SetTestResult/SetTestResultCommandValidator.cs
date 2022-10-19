using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.SetTestResult
{
    public class SetTestResultCommandValidator : AbstractValidator<SetTestResultCommand>
    {
        public SetTestResultCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            When((x => x.ResultStatus == TestResultStatusEnum.Recommended), () =>
            {
                RuleFor(x => x)
                    .Must(x => !x.RecommendedFor.Intersect(x.NotRecommendedFor).Any())
                    .WithErrorCode(ValidationCodes.INVALID_SELECTED_COLUMNS);

                RuleFor(x => x)
                    .Must(x => x.RecommendedFor.Length != 0 || x.NotRecommendedFor.Length != 0)
                    .WithErrorCode(ValidationCodes.INVALID_SELECTED_COLUMNS);

                When((x => x.RecommendedFor.Length != 0), () =>
                {
                    RuleFor(x => x.RecommendedFor)
                        .Must(x => x.All(c => c is > 0 and < 5))
                        .WithErrorCode(ValidationCodes.INVALID_SELECTED_COLUMNS);
                });

                When((x => x.NotRecommendedFor.Length != 0), () =>
                {
                    RuleFor(x => x.NotRecommendedFor)
                        .Must(x => x.All(c => c is > 0 and < 5))
                        .WithErrorCode(ValidationCodes.INVALID_SELECTED_COLUMNS);
                });
            });
        }
    }
}
