using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Services.Identity.Exceptions;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Identity.Models;
using CVU.ERP.Notifications.Email;
using Microsoft.AspNetCore.Identity;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Common.Services.Identity.IdentityServer
{
    public class IdentityServerIdentityService : IIdentityService
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IConfiguration _configuration;

        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly IPasswordHasher<ERPIdentityUser> _passwordHasher;
        private readonly IEnumerable<IUserValidator<ERPIdentityUser>> _userValidators;
        private readonly IEnumerable<IPasswordValidator<ERPIdentityUser>> _passwordValidators;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IdentityErrorDescriber _errors;
        private readonly IServiceProvider _services;
        private readonly ILogger<UserManager<ERPIdentityUser>> _logger;

        public string Type => "local";

        public IdentityServerIdentityService(IServiceProvider serviceProvider, INotificationService notificationService, IPasswordGenerator passwordGenerator, IConfiguration configuration, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ERPIdentityUser> passwordHasher, IEnumerable<IUserValidator<ERPIdentityUser>> userValidators, IEnumerable<IPasswordValidator<ERPIdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ERPIdentityUser>> logger)
        {
            _notificationService = notificationService;
            _passwordGenerator = passwordGenerator;
            _configuration = configuration;
            
            _optionsAccessor = optionsAccessor;
            _passwordHasher = passwordHasher;
            _userValidators = userValidators;
            _passwordValidators = passwordValidators;
            _keyNormalizer = keyNormalizer;
            _errors = errors;
            _services = services;
            _logger = logger;

            _userManager = UserManagerInstance;
        }

        private UserManager<ERPIdentityUser> UserManagerInstance => new UserManager<ERPIdentityUser>(
            new UserStore<ERPIdentityUser>(UserManagementDbContext.NewInstance(_configuration)),
            _optionsAccessor,
            _passwordHasher,
            _userValidators,
            _passwordValidators,
            _keyNormalizer,
            _errors,
            _services,
            _logger);

        public async Task<string> Create(UserProfile userProfile, bool notify)
        {
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