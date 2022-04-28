﻿using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.RemoveRole
{
    public class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
    {
        public RemoveRoleCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Role>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));
        }
    }
}
