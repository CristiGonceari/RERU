using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddEvaluations
{
    public class AddEvaluationsCommandValidator : AbstractValidator<AddEvaluationsCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddEvaluationsCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE, ValidationMessages.InvalidReference));

            When(r => r.EventId.HasValue, () =>
            {
                RuleFor(x => x.EventId.Value)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT, ValidationMessages.InvalidReference));

                RuleFor(x => x)
                    .Must(x => appDbContext.EventTestTemplates.Any(et =>
                        et.EventId == x.EventId && et.TestTemplateId == x.TestTemplateId && et.TestTemplate.Mode == TestTemplateModeEnum.Evaluation))
                    .WithErrorCode(ValidationCodes.INEXISTENT_TEST_TEMPLATE_IN_EVENT);

                RuleFor(x => x)
                    .MustAsync((x, cancellation) => ExistentCandidateInEvent(x))
                    .WithErrorCode(ValidationCodes.INEXISTENT_CANDIDATE_IN_EVENT);

                RuleFor(x => x)
                    .MustAsync((x, cancellation) => ExistentEvaluatorInEvent(x))
                    .WithErrorCode(ValidationCodes.INEXISTENT_EVALUATOR_IN_EVENT);
            });

            When(r => !r.EventId.HasValue, () =>
            {
                RuleFor(x => x.UserProfileIds)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_USER);

                RuleFor(x => x.EvaluatorIds)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_EVALUATOR);
            });
        }

        private async Task<bool> ExistentCandidateInEvent(AddEvaluationsCommand data)
        {
            var result = false;

            foreach (var userId in data.UserProfileIds)
            {
                result = _appDbContext.EventUsers.Any(et => et.EventId == data.EventId && et.UserProfileId == userId);

                if (!result)
                {
                    return false;
                }
            }

            return result;
        }

        private async Task<bool> ExistentEvaluatorInEvent(AddEvaluationsCommand data)
        {
            var result = false;

            foreach (var evaluatorId in data.EvaluatorIds)
            {
                result = _appDbContext.EventEvaluators.Any(et => et.EventId == data.EventId && et.EvaluatorId == evaluatorId);

                if (!result)
                {
                    return false;
                }
            }

            return result;
        }
    }
}
