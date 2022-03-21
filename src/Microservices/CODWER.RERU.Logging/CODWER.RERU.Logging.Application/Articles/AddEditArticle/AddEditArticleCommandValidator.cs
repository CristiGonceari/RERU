﻿using FluentValidation;
using CODWER.RERU.Logging.Application.Validation;

namespace CODWER.RERU.Logging.Application.Articles.AddEditArticle
{
    public class AddEditArticleCommandValidator : AbstractValidator<AddEditArticleCommand>
    {
        public AddEditArticleCommandValidator()
        {
            RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_NAME);

            RuleFor(r => r.Data.Content)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_CONTENT);
        }
    }
}
