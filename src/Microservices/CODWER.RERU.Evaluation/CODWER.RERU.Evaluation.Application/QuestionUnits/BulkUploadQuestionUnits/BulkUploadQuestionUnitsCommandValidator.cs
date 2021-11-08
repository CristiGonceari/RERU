using System;
using System.IO;
using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.BulkUploadQuestionUnits
{
    public class BulkUploadQuestionUnitsCommandValidator : AbstractValidator<BulkUploadQuestionUnitsCommand>
    {
        public BulkUploadQuestionUnitsCommandValidator()
        {
            RuleFor(r => r.Input)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_FILE);

            RuleFor(r => r.Input)
                .Must(r => Path.GetExtension(r.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                .WithErrorCode(ValidationCodes.ONLY_XLSX_FORMAT);
        }
    }
}
