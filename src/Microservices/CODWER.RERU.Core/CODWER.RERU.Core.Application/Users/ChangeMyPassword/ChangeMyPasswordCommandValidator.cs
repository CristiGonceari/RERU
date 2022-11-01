using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Identity.Models;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ChangeMyPassword
{
    public class ChangeMyPasswordCommandValidator : AbstractValidator<ChangeMyPasswordCommand>
    {
        public ChangeMyPasswordCommandValidator(UserManager<ERPIdentityUser> userManager, ICurrentApplicationUserProvider userProvider, UserManagementDbContext userManagementDbContext)
        {
            var currentUser = userProvider.Get().Result;
            var user = userManagementDbContext.Users.FirstOrDefaultAsync(u => u.Email == currentUser.Email).Result;

            RuleFor(x => x.oldPassword)
                .Must(x => userManager.CheckPasswordAsync(user, x).Result)
                .WithErrorCode(ValidationCodes.WRONG_OLD_PASSWORD);
        }
    }
}
