using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Services.Identity.Exceptions;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CVU.ERP.Identity.Models;
using CVU.ERP.Notifications.Email;
using Microsoft.AspNetCore.Identity;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Enums;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Common.Services.Identity.IdentityServer
{
    public class IdentityServerIdentityService : IIdentityService
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IPasswordGenerator _passwordGenerator;
        public string Type => "local";

        public IdentityServerIdentityService(IServiceProvider serviceProvider, INotificationService notificationService, IPasswordGenerator passwordGenerator)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ERPIdentityUser>>();
            _notificationService = notificationService;
            _passwordGenerator = passwordGenerator;
        }

        public async Task<string> Create(UserProfile userProfile, bool notify)
        {
           // var um = new UserManager<ERPIdentityUser>()

            var identityUser = new ERPIdentityUser()
            {
                Email = userProfile.Email,
                UserName = userProfile.Email
            };

            var password = _passwordGenerator.Generate();
            var response = await _userManager.CreateAsync(identityUser, password);

            if (response.Succeeded)
            {
                // TODO: asta trebuie de mutat in notification service
                if (notify)
                {
                    try
                    {
                        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
                        var template = await File.ReadAllTextAsync(assemblyPath + "/UserRegister.html");

                        template = template
                            .Replace("{FirstName}", $"{userProfile.FirstName} {userProfile.LastName}")
                            .Replace("{Login}", userProfile.Email)
                            .Replace("{Password}", password);

                        var emailData = new EmailData()
                        {
                            subject = "New account",
                            body = template,
                            from = "Do Not Reply",
                            to = identityUser.Email
                        };

                        await _notificationService.Notify(emailData, NotificationType.Both);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR {e.Message}");
                    }
                }
                // end

                return identityUser.Id;
            }

            if (response.Errors.Any())
            {
                throw new CreateIdentityFailedException(response.Errors.Select(re => $"{re.Code}: {re.Description}").ToArray());
            }

            throw new CreateIdentityFailedException("User was not created for unknown reason");
        }

        public async Task<string> Update(string userName, string newEmail, string lastEmail, bool notify)
        {
            var identityUser = await _userManager.FindByEmailAsync(lastEmail);

            var usernameResult = await _userManager.SetUserNameAsync(identityUser, newEmail);

            var emailResult = await _userManager.SetEmailAsync(identityUser, newEmail);

            var password = _passwordGenerator.Generate(); 
            await _userManager.UpdateNormalizedEmailAsync(identityUser);

            if (usernameResult.Succeeded && emailResult.Succeeded)
            {
                // TODO: asta trebuie de mutat in notification service
                if (notify)
                {
                    try
                    {
                        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
                        var template = await File.ReadAllTextAsync(assemblyPath + "/UserRegister.html");

                        template = template
                            .Replace("{FirstName}", $"{userName}")
                            .Replace("{Login}", newEmail)
                            .Replace("{Password}", password);

                        var emailData = new EmailData()
                        {
                            subject = "Update account",
                            body = template,
                            from = "Do Not Reply",
                            to = identityUser.Email
                        };

                        await _notificationService.Notify(emailData, NotificationType.Both);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR {e.Message}");
                    }
                }
                // end

                return identityUser.Id;
            }

            if (usernameResult.Errors.Any())
            {
                throw new CreateIdentityFailedException(usernameResult.Errors.Select(re => $"{re.Code}: {re.Description}").ToArray());
            }

            if (emailResult.Errors.Any())
            {
                throw new CreateIdentityFailedException(emailResult.Errors.Select(re => $"{re.Code}: {re.Description}").ToArray());
            }

            throw new CreateIdentityFailedException("User was not updated for unknown reason");
        }


        public async Task Remove(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var response = await _userManager.DeleteAsync(user);

                if (response.Errors.Any())
                {
                    throw new Exception("User was not deleted");
                }
            }
        }

        public async Task ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var password = _passwordGenerator.Generate();

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);

                string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
                var template = await File.ReadAllTextAsync(assemblyPath + "/ResetPassword.html");

                template = template
                    .Replace("{FirstName}", user.UserName)
                    .Replace("{Login}", user.Email)
                    .Replace("{Password}", password);

                var emailData = new EmailData()
                {
                    subject = "Reset Password",
                    body = template,
                    from = "Do Not Reply",
                    to = user.Email
                };

                await _notificationService.Notify(emailData, NotificationType.Both);
            }
        }
    }
}