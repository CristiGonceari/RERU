using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using System;
using System.IO;

namespace CODWER.RERU.Evaluation.Application.Options.BulkUploadOptions
{
    public class BulkUploadOptionsCommandValidator : AbstractValidator<BulkUploadOptionsCommand>
    {
        public BulkUploadOptionsCommandValidator()
        {
            RuleFor(r => r.Input)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_FILE);

            RuleFor(r => r.Input)
                .Must(r => Path.GetExtension(r.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                .WithErrorCode(ValidationCodes.ONLY_XLSX_FORMAT);

            RuleFor(r => r.QuestionUnitId)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);
        }
    }
}
