﻿using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contacts.GetContact
{
    public class GetContactQueryValidator : AbstractValidator<GetContactQuery>
    {
        public GetContactQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Contact>(appDbContext, ValidationCodes.CONTACT_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
