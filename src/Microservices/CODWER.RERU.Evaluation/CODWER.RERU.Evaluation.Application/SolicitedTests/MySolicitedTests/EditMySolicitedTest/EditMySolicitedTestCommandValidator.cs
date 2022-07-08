using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.EditMySolicitedTest
{
    public class EditMySolicitedTestCommandValidator : AbstractValidator<EditMySolicitedTestCommand>
    {
        public EditMySolicitedTestCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.CandidatePositionId)
               .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                   ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.Id.Value)
                .SetValidator(x => new ItemMustExistValidator<SolicitedVacantPosition>(appDbContext, ValidationCodes.INVALID_SOLICITED_POSITION,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.SolicitedTestStatus)
                .Must(x => x == SolicitedPositionStatusEnum.New)
                .WithErrorCode(ValidationCodes.ONLY_NEW_SOLICITED_TEST_CAN_BE_UPDATED);
        }
    }
}
