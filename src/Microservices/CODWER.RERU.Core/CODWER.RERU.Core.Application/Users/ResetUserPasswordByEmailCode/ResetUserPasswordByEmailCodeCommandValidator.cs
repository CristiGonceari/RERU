using CODWER.RERU.Core.Application.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.Users.ResetUserPasswordByEmailCode
{
    public class ResetUserPasswordByEmailCodeCommandValidator : AbstractValidator<ResetUserPasswordByEmailCodeCommand>
    {
        private readonly AppDbContext _appDbContext;

        public ResetUserPasswordByEmailCodeCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).NotEmpty()
                .Must(x => CheckCode(x.Code, x.Email))
                .WithErrorCode(ValidationCodes.INVALID_CODE);
        }

        private bool CheckCode(string code, string email)
        {
            var emailVerification = _appDbContext.EmailVerifications.FirstOrDefault(x => x.Email == email);

            if (emailVerification != null)
            {
                if (emailVerification.Code == code)
                {
                    return true;
                }
            }

            return false;
        }
    }
    
}
