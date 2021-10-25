using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.Interfaces;
using CODWER.RERU.Core.Application.Common.Services.Identity.Exceptions;
using CODWER.RERU.Core.Application.Common.Services.Password;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CODWER.RERU.Core.Application.Common.Services.Identity.IdentityServer
{
    public class IdentityServerIdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        public string Type => "local";

        public IdentityServerIdentityService(UserManager<ERPIdentityUser> userManager, IMapper mapper, IEmailService emailService, IPasswordGenerator passwordGenerator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
        }

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
                        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
                        var template = await File.ReadAllTextAsync(assemblyPath + "/UserRegister.html");

                        template = template
                            .Replace("{FirstName}", $"{userProfile.Name} {userProfile.LastName}")
                            .Replace("{Password}", password);

                        await _emailService.QuickSendAsync(subject: "New account",
                            body: template,
                            from: "Do Not Reply",
                            to: identityUser.Email);
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
                // throw new CreateIdentityFailedException($"User was not created because of some possible errors: {response.Errors.Select(e => $"{e.Code} {e.Description}")}");
                throw new CreateIdentityFailedException(response.Errors.Select(re => $"{re.Code}: {re.Description}").ToArray());
            }

            throw new CreateIdentityFailedException("User was not created for unknown reason");
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
                    .Replace("{Password}", password);

                await _emailService.QuickSendAsync(subject: "Reset Password",
                    body: template,
                    from: "Do Not Reply",
                    to: user.Email);
            }
        }
    }
}