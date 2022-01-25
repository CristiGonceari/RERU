using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Contacts.AddContact
{
    public class AddContactCommandValidator : AbstractValidator<AddContactCommand>
    {
        private readonly AppDbContext _appDbContext;
        public AddContactCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data).SetValidator(new ContactValidator(appDbContext));

            RuleFor(x => x.Data).Custom(ValidateRepetitiveContact);
        }

        private void ValidateRepetitiveContact(AddEditContactDto dto, CustomContext context)
        {
            var existent = _appDbContext.Contacts.Any(x => x.Value == dto.Value && x.Type == dto.Type);

            if (existent)
            {
                context.AddFail(ValidationCodes.EXISTENT_CONTACT, ValidationMessages.InvalidReference);
            }
        }
    }
}
