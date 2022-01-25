﻿using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationRoles
{
    public class OrganizationRoleValidator : AbstractValidator<AddEditOrganizationRoleDto>
    {
        public OrganizationRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            //RuleFor(x=>x.Description).NotEmpty()
            //    .WithMessage(ValidationMessages.InvalidInput)
            //    .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
