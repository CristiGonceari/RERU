﻿using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Documents;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateDocumentReplacedKeys
{
    public class GetTestTemplateDocumentReplacedKeysQueryValidator : AbstractValidator<GetTestTemplateDocumentReplacedKeysQuery>
    {
        public GetTestTemplateDocumentReplacedKeysQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
               .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                   ValidationMessages.InvalidReference));

            RuleFor(x => x.DocumentTemplateId)
               .SetValidator(x => new ItemMustExistValidator<DocumentTemplate>(appDbContext, ValidationCodes.INVALID_DOCUMENT_TEMPLATE,
                   ValidationMessages.InvalidReference));
        }
    }
}