using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.Application.Validators.Files
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator(string errorMessage)
        {
            RuleFor(x => x.FileName).Custom((name, c) => ValidateExtension(name, errorMessage, c));
        }

        private void ValidateExtension(string name, string errorMessage, CustomContext context)
        {
            var splitArray = name.Split('.');

            if (splitArray.Length < 2)
            {
                context.AddFail(ValidationCodes.INVALID_FILE_NAME, errorMessage);
            }

            var extension = splitArray.Last().ToLower();
            if (!FileExtensions().Contains(extension))
            {
                context.AddFail(ValidationCodes.INVALID_FILE_EXTENSION, errorMessage);
            }

        }

        private List<string> FileExtensions() => new() {"pdf", "doc", "docx", "jpg", "png", "txt"};
    }
}
