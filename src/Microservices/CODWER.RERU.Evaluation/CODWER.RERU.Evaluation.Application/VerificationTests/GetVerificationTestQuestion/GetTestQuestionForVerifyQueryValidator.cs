﻿using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetVerificationTestQuestion
{
    public class GetTestQuestionForVerifyQueryValidator : AbstractValidator<GetTestQuestionForVerifyQuery>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IMediator _mediator;
        public GetTestQuestionForVerifyQueryValidator(AppDbContext appDbContext, IUserProfileService userProfileService, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _mediator = mediator;

            RuleFor(x => x.Data)
                .Must(x => appDbContext.TestQuestions
                    .Any(t => t.Id == appDbContext.Tests.FirstOrDefault(ts => ts.Id == x.TestId).TestQuestions.FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
                .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

            RuleFor(x => x.Data)
                .MustAsync((x, cancellation) => IsEvaluator(x))
                .WithErrorCode(ValidationCodes.INVALID_EVALUATOR_FOR_THIS_TEST);
        }

        private async Task<bool> IsEvaluator(VerificationTestQuestionDto data)
        {
            var test = _appDbContext.Tests
                .Include(t => t.TestType)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.QuestionUnit)
                .FirstOrDefault(t => t.Id == data.TestId);

            var currentUser = await _userProfileService.GetCurrentUser();

            var isEvaluator = false;

            var isCandidate = false;

            if (!test.TestQuestions.All(t => t.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer))
            {
                if (test != null && test.EventId != null && test.EvaluatorId == null && test.TestType.Settings.CanViewResultWithoutVerification)
                {
                    isCandidate = _appDbContext.EventUsers.Any(e =>
                        e.EventId == test.EventId && e.UserProfileId == currentUser.Id);
                    
                    isEvaluator = _appDbContext.EventEvaluators.Any(e =>
                        e.EventId == test.EventId && e.EvaluatorId == currentUser.Id);
                }
                else if (test != null && test.TestType.Settings.CanViewResultWithoutVerification)
                {
                    isEvaluator = _appDbContext.Tests.Any(t => t.Id == test.Id &&
                                                               (t.EvaluatorId == currentUser.Id || t.UserProfileId == currentUser.Id));
                }
            }
            else return true;

            return isEvaluator || isCandidate;
        }
    }
}
