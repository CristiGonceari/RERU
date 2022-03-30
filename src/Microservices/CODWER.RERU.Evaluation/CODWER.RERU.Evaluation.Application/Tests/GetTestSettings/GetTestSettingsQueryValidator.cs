﻿using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestSettings
{
    public class GetTestSettingsQueryValidator : AbstractValidator<GetTestSettingsQuery>
    {
        public GetTestSettingsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            When(x => appDbContext.Tests.Include(x => x.TestTemplate)
                .First(t => t.Id == x.Id)
                .TestTemplate.Mode == TestTemplateModeEnum.Test, () =>
                {
                    When(x => !appDbContext.Tests
                         .Include(x => x.TestTemplate)
                             .ThenInclude(x => x.Settings)
                         .First(t => t.Id == x.Id)
                         .TestTemplate.Settings.StartWithoutConfirmation, () =>
                         {
                             RuleFor(x => x.Id)
                             .Must(x => appDbContext.Tests.First(t => t.Id == x).TestStatus == TestStatusEnum.AlowedToStart)
                             .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
                         });

                    When(x => !appDbContext.Tests
                        .Include(x => x.TestTemplate)
                            .ThenInclude(x => x.Settings)
                        .First(t => t.Id == x.Id)
                        .TestTemplate.Settings.StartBeforeProgrammation, () =>
                        {
                            RuleFor(x => x.Id)
                            .Must(x => appDbContext.Tests.First(t => t.Id == x).ProgrammedTime <= DateTime.Now)
                            .WithErrorCode(ValidationCodes.WAIT_THE_START_TIME);
                        });

                    When(x => !appDbContext.Tests
                        .Include(x => x.TestTemplate)
                            .ThenInclude(x => x.Settings)
                        .First(t => t.Id == x.Id)
                        .TestTemplate.Settings.StartBeforeProgrammation, () =>
                        {
                            RuleFor(x => x.Id)
                            .Must(x => appDbContext.Tests.First(t => t.Id == x).ProgrammedTime <= DateTime.Now)
                            .WithErrorCode(ValidationCodes.WAIT_THE_START_TIME);
                        });

                    When(x => appDbContext.Tests.First(t => t.Id == x.Id).TestPassStatus.HasValue, () =>
                    {
                        RuleFor(x => x.Id)
                        .Must(x => appDbContext.Tests.First(t => t.Id == x).TestPassStatus.Value == TestPassStatusEnum.Allowed)
                        .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
                    });

                    When(x => appDbContext.Tests.First(t => t.Id == x.Id).EventId.HasValue, () =>
                    {
                        RuleFor(x => x.Id)
                        .Must(x => appDbContext.Tests.Include(x => x.Event).First(t => t.Id == x).Event.TillDate > DateTime.Now)
                        .WithErrorCode(ValidationCodes.FINISHED_EVENT);
                    });
                });
        }
    }
}
