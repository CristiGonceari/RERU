﻿using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Documents;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.UpdateDocumentTemplate
{
    public class UpdateDocumentTemplateCommandValidator : AbstractValidator<UpdateDocumentTemplateCommand>
    {
        public UpdateDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<DocumentTemplate>(appDbContext, ValidationCodes.DOCUMENTS_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new DocumentTemplateValidator());
        }
    }
}
