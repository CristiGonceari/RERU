using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.AddEditRequiredDocumentPosition
{
    public class AddEditRequiredDocumentPositionCommandValidator : AbstractValidator<AddEditRequiredDocumentPositionCommand>
    {
        public AddEditRequiredDocumentPositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.CandidatePositionId)
                    .SetValidator(new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION, ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.RequiredDocumentId)
                    .SetValidator(new ItemMustExistValidator<RequiredDocument>(appDbContext, ValidationCodes.INVALID_REQUIRED_DOCUMENT, ValidationMessages.InvalidReference));
            });
        }
    }
}
