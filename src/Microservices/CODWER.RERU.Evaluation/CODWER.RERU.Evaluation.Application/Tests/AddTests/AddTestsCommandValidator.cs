using System;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests
{
    public class AddTestsCommandValidator : AbstractValidator<AddTestsCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public AddTestsCommandValidator(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;

            RuleFor(x => x.ProcessId)
                .SetValidator(x => new ItemMustExistValidator<Process>(appDbContext, ValidationCodes.INVALID_PROGRESS,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.UserProfileIds)
                .Must(x => x.Any())
                .WithErrorCode(ValidationCodes.INVALID_EVLUATED_USER_LIST);

            When(x => x.EventId == null, () =>
            {
                RuleFor(x => x.ProgrammedTime)
                                .NotNull()
                                .WithErrorCode(ValidationCodes.INVALID_TIME)
                                .GreaterThan(new DateTime(2000, 1, 1))
                                .WithErrorCode(ValidationCodes.INVALID_TIME);
            });

            When(r => r.EventId.HasValue, () =>
            {
                RuleFor(x => x.EventId.Value)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x)
                    .Must(x => appDbContext.EventTestTemplates.Any(et => et.EventId == x.EventId && et.TestTemplateId == x.TestTemplateId))
                    .WithErrorCode(ValidationCodes.INEXISTENT_TEST_TEMPLATE_IN_EVENT);

                RuleFor(x => x)
                    .MustAsync((x, cancellation) => ExistentCandidateInEvent(x))
                    .WithErrorCode(ValidationCodes.INEXISTENT_CANDIDATE_IN_EVENT);
            });

            //When(r => !r.EvaluatorId.HasValue && !r.EventId.HasValue, () =>
            //{
            //    RuleFor(x => x)
            //        .MustAsync((x, cancellation) => IsOnlyOneAnswerTest(x))
            //        .WithErrorCode(ValidationCodes.MUST_ADD_EVENT_OR_EVALUATOR);
            //});

            //When(r => r.EventId.HasValue && r.EvaluatorId.HasValue, () =>
            //{
            //    RuleFor(x => x)
            //        .Must(x => !appDbContext.EventEvaluators.Any(e => e.EventId == x.EventId))
            //        .WithErrorCode(ValidationCodes.EXISTENT_EVALUATOR_IN_EVENT);
            //});

            When(r => r.LocationId.HasValue, () =>
            {
                RuleFor(x => x.LocationId.Value)
                    .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));
            });

            //When(r => r.EvaluatorIds.Any(), () =>
            //{
            //    //RuleFor(x => x.EvaluatorId.Value)
            //    //    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
            //    //        ValidationMessages.InvalidReference));

            //    RuleFor(x => x.EvaluatorIds)
            //        .Must(x => x.Any())
            //        .WithErrorCode(ValidationCodes.INVALID_EVLUATED_USER_LIST);

            //    RuleFor(x => x)
            //        .Must(x => x.UserProfileIds.Intersect(x.EvaluatorIds).Any())
            //        .WithErrorCode(ValidationCodes.EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME);
            //});
        }

        private async Task<bool> IsOnlyOneAnswerTest(AddTestsCommand data)
        {
            var dataList = await _mediator.Send(new GetTestTemplateByStatusQuery { TestTemplateStatus = TestTemplateStatusEnum.Active });

            var result = dataList.FirstOrDefault(x => x.TestTemplateId == data.TestTemplateId);

            return result?.IsOnlyOneAnswer ?? false;
        }

        private async Task<bool> ExistentCandidateInEvent(AddTestsCommand data)
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
    }
}
