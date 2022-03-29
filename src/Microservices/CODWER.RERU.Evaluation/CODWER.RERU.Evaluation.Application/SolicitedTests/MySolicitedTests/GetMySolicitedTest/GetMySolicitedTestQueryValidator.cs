﻿using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.GetMySolicitedTest
{
    public class GetMySolicitedTestQueryValidator : AbstractValidator<GetMySolicitedTestQuery>
    {
        public GetMySolicitedTestQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<SolicitedTest>(appDbContext, ValidationCodes.INVALID_SOLICITED_TEST,
                    ValidationMessages.InvalidReference));
        }
    }
}