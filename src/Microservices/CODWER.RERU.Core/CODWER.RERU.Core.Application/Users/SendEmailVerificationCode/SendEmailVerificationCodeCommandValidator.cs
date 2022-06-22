using System.Linq;
using CODWER.RERU.Core.Application.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeCommandValidator : AbstractValidator<SendEmailVerificationCodeCommand>
    {
        private readonly AppDbContext _appDbContext;
        public SendEmailVerificationCodeCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Email)
                .Must(x => !DublicateEmail(x))
                .WithErrorCode(ValidationCodes.DUPLICATE_EMAIL_IN_SYSTEM);
        }

        private bool DublicateEmail(string email)
        {
            var exist = _appDbContext.UserProfiles.Any(x => x.Email == email);

            return exist;
        }
    }
}
