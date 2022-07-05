using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.AddEditRequiredDocument
{
    public class AddEditRequiredDocumentsCommandValidator : AbstractValidator<AddEditRequiredDocumentsCommand>
    {
        public AddEditRequiredDocumentsCommandValidator()
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);
            });
        }
    }
}
