﻿using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.AddUpdateContractorPermissions
{
    public class AddUpdateContractorPermissionsCommandValidator : AbstractValidator<AddUpdateContractorPermissionsCommand>
    {
        public AddUpdateContractorPermissionsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }
    }
}
