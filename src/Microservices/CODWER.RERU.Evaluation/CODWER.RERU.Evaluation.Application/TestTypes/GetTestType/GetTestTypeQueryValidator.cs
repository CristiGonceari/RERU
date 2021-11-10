﻿using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestType
{
    public class GetTestTypeQueryValidator : AbstractValidator<GetTestTypeQuery>
    {
        public GetTestTypeQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));
        }
    }
}
