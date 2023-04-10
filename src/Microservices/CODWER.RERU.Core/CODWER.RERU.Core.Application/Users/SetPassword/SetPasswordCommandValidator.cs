using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.ChangeMyPassword;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.Password;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Identity.Models;
using CVU.ERP.ServiceProvider;
using CVU.ERP.ServiceProvider.Exceptions;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.SetPassword
{
    public class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
    {
        public SetPasswordCommandValidator(UserManager<ERPIdentityUser> userManager, ICurrentApplicationUserProvider userProvider, ICommonServiceProvider commonServiceProvider)
        {
            WhenAsync(
                async (x, cancellation) =>
                {
                    var userProfile = await commonServiceProvider.AppDbContext.UserProfiles
                        .Include(z => z.Identities)
                        .FirstOrDefaultAsync(up => up.Id == x.Data.Id);

                    return (userProfile != null && userProfile.Identities.Any(upi => upi.Type == "local")) ;
                }, 
                () =>
                {
                    RuleFor(x => x)
                        .MustAsync(async (x, cancellation) =>
                        {
                            var userProfile = await commonServiceProvider.AppDbContext.UserProfiles
                                .Include(z => z.Identities)
                                .FirstOrDefaultAsync(up => up.Id == x.Data.Id);

                            var upIdentity = userProfile
                                .Identities
                                .FirstOrDefault(upi => upi.Type == "local");

                            var identityServerUser = await commonServiceProvider
                                .UserManagementDbContext
                                .Users
                                .FirstOrDefaultAsync(u => u.Id == upIdentity.Identificator);

                            var isPasswordsNotSame = !await userManager.CheckPasswordAsync(identityServerUser, x.Data.Password);

                            return isPasswordsNotSame;
                        })
                        .WithErrorCode(ValidationCodes.OLD_PASSWORD_SAME_AS_NEW_PASSWORD)
                        .WithMessage("old password same as new password");
                }
            );

            RuleFor(x => x.Data)
                .Must(x => x.Password == x.RepeatNewPassword)
                .WithErrorCode(ValidationCodes.PASSWORDS_NOT_MATCH)
                .WithMessage("the repeated password does not match the new password");
        }

    }
}
