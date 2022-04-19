using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Documents;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestDocumentReplacedKeys
{
    public class GetTestDocumentReplacedKeysQueryValidator : AbstractValidator<GetTestDocumentReplacedKeysQuery>
    {
        public GetTestDocumentReplacedKeysQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
            .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                ValidationMessages.InvalidReference));

            RuleFor(x => x.DocumentTemplateId)
               .SetValidator(x => new ItemMustExistValidator<DocumentTemplate>(appDbContext, ValidationCodes.INVALID_DOCUMENT_TEMPLATE,
                   ValidationMessages.InvalidReference));
        }
    }
}
