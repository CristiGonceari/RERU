using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.EditMySolicitedTest
{
    public class EditMySolicitedTestCommandValidator : AbstractValidator<EditMySolicitedTestCommand>
    {
        public EditMySolicitedTestCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.TestTemplateId)
               .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                   ValidationMessages.InvalidReference));

            When(r => r.Data.EventId.HasValue, () =>
            {
                RuleFor(x => x.Data.EventId.Value)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));
            });

            RuleFor(x => x.Data.SolicitedTime)
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithErrorCode(ValidationCodes.INVALID_TIME);

            //RuleFor(x => x.Data.SolicitedTestStatus)
            //    .IsInEnum()
            //    .Must(x => x == SolicitedTestStatusEnum.New)
            //    .WithErrorCode(ValidationCodes.ONLY_NEW_SOLICITED_TEST_CAN_BE_UPDATED);
        }
    }
}
