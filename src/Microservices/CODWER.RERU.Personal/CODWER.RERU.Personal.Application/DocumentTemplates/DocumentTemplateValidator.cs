using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Common.Validation;
using CVU.ERP.StorageService.Entities;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DocumentTemplates
{
    public class DocumentTemplateValidator : AbstractValidator<AddEditDocumentTemplateDto>
    {
        public DocumentTemplateValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => (int)x.FileType)
                .SetValidator(new ExistInEnumValidator<FileTypeEnum>())
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.Value).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
