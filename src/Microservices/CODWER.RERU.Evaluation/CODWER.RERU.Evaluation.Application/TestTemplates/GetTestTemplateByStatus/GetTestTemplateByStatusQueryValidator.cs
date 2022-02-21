using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus
{
    public class GetTestTemplateByStatusQueryValidator : AbstractValidator<GetTestTemplateByStatusQuery>
    {
        public GetTestTemplateByStatusQueryValidator()
        {
            RuleFor(x => x.testTemplateStatus)
                .IsInEnum()
                .WithErrorCode(ValidationCodes.INVALID_STATUS);
        }
    }
}
