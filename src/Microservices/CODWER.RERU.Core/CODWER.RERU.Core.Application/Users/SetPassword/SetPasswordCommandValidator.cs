using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
using FluentValidation.Results;
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
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly ICommonServiceProvider _commonServiceProvider;

        public SetPasswordCommandValidator(UserManager<ERPIdentityUser> userManager, ICommonServiceProvider commonServiceProvider)
        {
            _userManager = userManager;
            _commonServiceProvider = commonServiceProvider;

            RuleFor(x => x).CustomAsync(CheckUserPassword);

            RuleFor(x => x.Data)
                .Must(x => x.Password == x.RepeatNewPassword)
                .WithErrorCode(ValidationCodes.PASSWORDS_NOT_MATCH)
                .WithMessage("the repeated password does not match the new password");
        }

        private async Task CheckUserPassword(SetPasswordCommand x, CustomContext context, CancellationToken cancellation)
        {
            var userProfile = await _commonServiceProvider.AppDbContext.UserProfiles
                .Include(z => z.Identities)
                .FirstOrDefaultAsync(up => up.Id == x.Data.Id);

            if (userProfile == null || !userProfile.Identities.Any(upi => upi.Type == "local"))
            {
                return;
            }

            var upIdentity = userProfile
            .Identities
            .FirstOrDefault(upi => upi.Type == "local");

            var identityServerUser = await _commonServiceProvider
                .UserManagementDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == upIdentity.Identificator);

            var newPasswordEqualOldPassword = await _userManager.CheckPasswordAsync(identityServerUser, x.Data.Password);

            if (newPasswordEqualOldPassword)
            {
                context.AddFail(ValidationCodes.OLD_PASSWORD_SAME_AS_NEW_PASSWORD, "old password same as new password");
            }
        }
    }
}
