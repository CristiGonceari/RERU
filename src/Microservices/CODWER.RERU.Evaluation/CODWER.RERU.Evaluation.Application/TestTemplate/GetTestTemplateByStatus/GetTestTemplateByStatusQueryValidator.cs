using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeByStatus
{
    public class GetTestTemplateByStatusQueryValidator : AbstractValidator<GetTestTemplateByStatusQuery>
    {
        public GetTestTemplateByStatusQueryValidator()
        {
            RuleFor(x => x.TestTypeStatus)
                .IsInEnum()
                .WithErrorCode(ValidationCodes.INVALID_STATUS);
        }
    }
}
