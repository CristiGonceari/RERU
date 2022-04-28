using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
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
                    When(x => appDbContext.Tests.First(t => t.Id == x.TestId).TestPassStatus.HasValue, () =>
                    {
                        RuleFor(x => x.TestId)
                        .Must(x => appDbContext.Tests.First(t => t.Id == x).TestPassStatus.Value == TestPassStatusEnum.Allowed)
                        .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
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


            var castNow = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            var castProgrammedTime = new DateTime(programmedTime.Year, programmedTime.Month, programmedTime.Day, programmedTime.Hour, programmedTime.Minute, 0);

            if (!test.TestTemplate.Settings.StartBeforeProgrammation && !test.TestTemplate.Settings.StartAfterProgrammation && castNow == castProgrammedTime)
            {
                return;
            }

            if (!test.TestTemplate.Settings.StartBeforeProgrammation && castProgrammedTime > castNow)
            {
                context.AddFail(ValidationCodes.INVALID_TEST_START_TIME, ValidationMessages.InvalidInput);

                return;
            }
            if (!test.TestTemplate.Settings.StartAfterProgrammation && castProgrammedTime <= castNow)
            {
                context.AddFail(ValidationCodes.INVALID_TEST_START_TIME, ValidationMessages.InvalidInput);
            }
        }
    }
}
