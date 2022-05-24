using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.StartTest
{
    public class StartTestCommandValidator : AbstractValidator<StartTestCommand>
    {
        private readonly AppDbContext _appDbContext;

        public StartTestCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Custom((id, c) => CheckStartTest(id, c));

            When(x => appDbContext.Tests.Include(x => x.TestTemplate)
                .First(t => t.Id == x.TestId)
                .TestTemplate.Mode == TestTemplateModeEnum.Test, () =>
                {
                    When(x => !appDbContext.Tests
                         .Include(x => x.TestTemplate)
                             .ThenInclude(x => x.Settings)
                         .First(t => t.Id == x.TestId)
                         .TestTemplate.Settings.StartWithoutConfirmation, () =>
                         {
                             RuleFor(x => x.TestId)
                             .Must(x => appDbContext.Tests.First(t => t.Id == x).TestStatus == TestStatusEnum.AlowedToStart)
                             .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
                         });

                    When(x => appDbContext.Tests.First(t => t.Id == x.TestId).TestPassStatus.HasValue, () =>
                    {
                        RuleFor(x => x.TestId)
                        .Must(x => appDbContext.Tests.First(t => t.Id == x).TestPassStatus.Value == TestPassStatusEnum.Allowed)
                        .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
                    });

                    When(x => appDbContext.Tests.First(t => t.Id == x.TestId).EventId.HasValue, () =>
                    {
                        RuleFor(x => x.TestId)
                        .Must(x => appDbContext.Tests.Include(x => x.Event).First(t => t.Id == x).Event.TillDate > DateTime.Now)
                        .WithErrorCode(ValidationCodes.FINISHED_EVENT);
                    });
                });
        }

        private void CheckStartTest(int testId, CustomContext context)
        {
            var test = _appDbContext.Tests
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.Settings)
                .FirstOrDefault(x => x.Id == testId);

            var now = DateTime.Now;
            var programmedTime = test.ProgrammedTime;

            var startBefore = test.TestTemplate.Settings.StartBeforeProgrammation;
            var startAfter = test.TestTemplate.Settings.StartAfterProgrammation;

            if (IntervalMaxOneMinute(now, programmedTime))
            {
                return;
            }

            if (!startBefore && now < programmedTime && !IntervalMaxOneMinute(now, programmedTime))
            {
                context.AddFail(ValidationCodes.INVALID_TEST_START_TIME, ValidationMessages.InvalidInput);

                return;
            }

            if (test.EventId == null)
            {
                if (!startAfter && programmedTime < now && !IntervalMaxOneMinute(now, programmedTime))
                {
                    context.AddFail(ValidationCodes.INVALID_TEST_START_TIME, ValidationMessages.InvalidInput);
                }
            }
        }

        private bool IntervalMaxOneMinute(DateTime now, DateTime programmed)
        {
            var interval = now - programmed;

            if (Math.Abs(interval.TotalSeconds) <= 90)
            {
                return true;
            }

            return false;
        }
    }
}
