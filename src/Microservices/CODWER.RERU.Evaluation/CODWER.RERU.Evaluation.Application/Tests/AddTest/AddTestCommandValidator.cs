using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTest
{
    public class AddTestCommandValidator : AbstractValidator<AddTestCommand>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;

        public AddTestCommandValidator(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext.NewInstance();
            _mediator = mediator;

            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.UserProfileId)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(_appDbContext, ValidationCodes.INVALID_USER,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.TestTemplateId)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(_appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                        ValidationMessages.InvalidReference));

                When(x => x.Data.EventId == null, () =>
                {
                    RuleFor(x => x.Data.ProgrammedTime)
                                    .GreaterThan(new DateTime(2000, 1, 1))
                                    .WithErrorCode(ValidationCodes.INVALID_TIME);
                });

                When(r => r.Data.EventId.HasValue, () =>
                {
                    RuleFor(x => x.Data.EventId.Value)
                        .SetValidator(x => new ItemMustExistValidator<Event>(_appDbContext, ValidationCodes.INVALID_EVENT,
                            ValidationMessages.InvalidReference));

                    RuleFor(x => x.Data)
                        .Must(x => _appDbContext.EventTestTemplates.Any(et => et.EventId == x.EventId && et.TestTemplateId == x.TestTemplateId))
                        .WithErrorCode(ValidationCodes.INEXISTENT_TEST_TEMPLATE_IN_EVENT);

                    RuleFor(x => x.Data)
                        .Must(x => _appDbContext.EventUsers.Any(et => et.EventId == x.EventId && et.UserProfileId == x.UserProfileId))
                        .WithErrorCode(ValidationCodes.INEXISTENT_CANDIDATE_IN_EVENT);
                });

                When(r => !r.Data.EvaluatorId.HasValue && !r.Data.EventId.HasValue, () =>
                {
                    RuleFor(x => x.Data)
                        .MustAsync((x, cancellation) => IsOnlyOneAnswerTest(x))
                        .WithErrorCode(ValidationCodes.MUST_ADD_EVENT_OR_EVALUATOR);
                });

                When(r => r.Data.LocationId.HasValue, () =>
                {
                    RuleFor(x => x.Data.LocationId.Value)
                        .SetValidator(x => new ItemMustExistValidator<Location>(_appDbContext, ValidationCodes.INVALID_LOCATION,
                            ValidationMessages.InvalidReference));
                });

                When(r => r.Data.EvaluatorId.HasValue, () =>
                {
                    RuleFor(x => x.Data.EvaluatorId.Value)
                        .SetValidator(x => new ItemMustExistValidator<UserProfile>(_appDbContext, ValidationCodes.INVALID_USER,
                            ValidationMessages.InvalidReference));

                    RuleFor(x => x.Data)
                        .Must(x => x.EvaluatorId != x.UserProfileId)
                        .WithErrorCode(ValidationCodes.EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME);
                });
            });
        }

        private async Task<bool> IsOnlyOneAnswerTest(AddEditTestDto data)
        {
            var dataList = await _mediator.Send(new GetTestTemplateByStatusQuery { TestTemplateStatus = TestTemplateStatusEnum.Active });

            var result = dataList.FirstOrDefault(x => x.TestTemplateId == data.TestTemplateId);

            return result.IsOnlyOneAnswer;
        }
     }
}
