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
        }

        private void CheckStartTest(int testId, CustomContext context)
        {
            var test = _appDbContext.Tests
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.Settings)
                .FirstOrDefault(x => x.Id == testId);

            if (!test.TestTemplate.Settings.StartBeforeProgrammation && test.ProgrammedTime > DateTime.Now)
            {
                context.AddFail(ValidationCodes.INVALID_TEST_START_TIME, ValidationMessages.InvalidInput);

                return;
            }
            if (!test.TestTemplate.Settings.StartAfterProgrammation && test.ProgrammedTime < DateTime.Now)
            {
                context.AddFail(ValidationCodes.INVALID_TEST_START_TIME, ValidationMessages.InvalidInput);
            }
        }
    }
}
