using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocument
{
    public class GetRequiredDocumentQueryValidator : AbstractValidator<GetRequiredDocumentQuery>
    {
        public GetRequiredDocumentQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(
                    new ItemMustExistValidator<RequiredDocument>(appDbContext, ValidationCodes.INVALID_ID, ValidationMessages.NotFound)
                );
        }
    }
}
