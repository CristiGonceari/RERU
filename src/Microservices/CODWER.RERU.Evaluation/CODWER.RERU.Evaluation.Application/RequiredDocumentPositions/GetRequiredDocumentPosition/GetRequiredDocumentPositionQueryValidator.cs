using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.GetRequiredDocumentPosition
{
    public class GetRequiredDocumentPositionQueryValidator : AbstractValidator<GetRequiredDocumentPositionQuery>
    {
        public GetRequiredDocumentPositionQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(
                    new ItemMustExistValidator<RequiredDocumentPosition>(appDbContext, ValidationCodes.INVALID_ID, ValidationMessages.NotFound)
                );
        }
    }
}
