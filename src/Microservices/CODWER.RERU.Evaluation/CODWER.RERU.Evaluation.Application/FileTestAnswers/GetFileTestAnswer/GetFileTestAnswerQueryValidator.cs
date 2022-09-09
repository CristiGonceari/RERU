﻿using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.GetFileTestAnswer
{
    public class GetFileTestAnswerQueryValidator : AbstractValidator<GetFileTestAnswerQuery>
    {
        public GetFileTestAnswerQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_SOLICITED_VACANT_POSITION_ID,
                    ValidationMessages.NotFound));
        }
    }
}
