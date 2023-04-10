using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Identity.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using CVU.ERP.ServiceProvider.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.SetPassword {
    public class SetPasswordCommandHandler : BaseHandler, IRequestHandler<SetPasswordCommand, Unit> 
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly INotificationService _notificationService;

        public SetPasswordCommandHandler(ICommonServiceProvider commonServicepProvider, UserManager<ERPIdentityUser> userManager, INotificationService notificationService) 
            : base (commonServicepProvider)
        {
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle (SetPasswordCommand request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles
                .Include(x => x.Identities)
                .FirstOrDefaultAsync (up => up.Id == request.Data.Id);

            if (userProfile != null && userProfile.Identities.Any(upi => upi.Type == "local"))
            {
                var upIdentity = userProfile.Identities.FirstOrDefault(upi => upi.Type == "local");
                var identityServerUser = await UserManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == upIdentity.Identificator);

                if (await _userManager.CheckPasswordAsync(identityServerUser, request.Data.Password))
                {
                    throw new ApplicationRequestValidationException(
                        new List<ValidationMessage>()
                        {
                            new ()
                            {
                                MessageText = "old password same as new password", 
                                Code = ValidationCodes.OLD_PASSWORD_SAME_AS_NEW_PASSWORD
                            }
                        }
                    );
                }

                if (identityServerUser != null)
                {
                    if (request.Data.Password == request.Data.RepeatNewPassword)
                    {
                        List<string> passwordErrors = new List<string>();

                        var validators = _userManager.PasswordValidators;

                        foreach (var validator in validators)
                        {
                            var result = await validator.ValidateAsync(_userManager, null, request.Data.Password);

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
                                identityServerUser.PasswordHash =
                                    _userManager.PasswordHasher.HashPassword(identityServerUser, request.Data.Password);
                                await _userManager.UpdateAsync(identityServerUser);

                                userProfile.Password = request.Data.Password;
                                await AppDbContext.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("New password and Repeat password aren't the same");
                    }

                    if (request.Data.EmailNotification)
                    {
                        await _notificationService.PutEmailInQueue(new QueuedEmailData
                        {
                            Subject = "Parolă nouă",
                            To = identityServerUser.Email,
                            HtmlTemplateAddress = "Templates/SetPassword.html",
                            ReplacedValues = new Dictionary<string, string>()
                            {
                                { "{FirstName}", identityServerUser.UserName },
                                { "{Password}", request.Data.Password }
                            }
                        });
                    }
                }
            }

            return Unit.Value;
        }
    }
}