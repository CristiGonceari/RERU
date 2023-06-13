using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using CVU.ERP.ServiceProvider;
using CVU.ERP.Notifications.Services;

namespace CODWER.RERU.Core.Application.Users.ChangeMyPassword
{
    public class ChangeMyPasswordCommandHandler : BaseHandler, IRequestHandler<ChangeMyPasswordCommand, Unit>
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly ICurrentApplicationUserProvider _userProvider;
        private readonly INotificationService _notificationService;

        public ChangeMyPasswordCommandHandler(
            ICommonServiceProvider commonServiceProvider, ICurrentApplicationUserProvider userProvider,
            UserManager<ERPIdentityUser> userManager,
            INotificationService notificationService) : base(commonServiceProvider)
        {
            _userManager = userManager;
            _notificationService = notificationService;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(ChangeMyPasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProvider.Get();
            var user = await UserManagementDbContext.Users.FirstOrDefaultAsync(u => u.Email == currentUser.Email);
            var userProfile = await AppDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id.ToString() == currentUser.Id);

            if (await _userManager.CheckPasswordAsync(user, request.oldPassword))
            {
                if (request.newPassword != request.repeatPassword)
                {
                    throw new Exception("New password and Repeat password aren't the same");
                }

                List<string> passwordErrors = new List<string>();

                var validators = _userManager.PasswordValidators;

                foreach (var validator in validators)
                {
                    var result = await validator.ValidateAsync(_userManager, null, request.newPassword);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            passwordErrors.Add(error.Description);
                            throw new Exception("Validation Error");
                        }
                    }
                    else
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.newPassword);
                        await _userManager.UpdateAsync(user);

                        userProfile.Password = request.newPassword;
                        await AppDbContext.SaveChangesAsync();
                    }
                }

                await _notificationService.PutEmailInQueue(new QueuedEmailData
                {
                    Subject = "Parolă nouă a fost schimbată cu succes!",
                    To = user.Email,
                    HtmlTemplateAddress = "Templates/SetPassword.html",
                    ReplacedValues = new Dictionary<string, string>()
                    {
                        { "{FirstName}", user.UserName },
                        { "{Password}", request.newPassword }
                    }
                });
            }
            else
            {
                throw new Exception("Wrong Old Password");
            }

            return Unit.Value;
        }
    }
}