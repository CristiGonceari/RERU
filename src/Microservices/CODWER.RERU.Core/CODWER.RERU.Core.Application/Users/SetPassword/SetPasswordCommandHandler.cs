using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.SetPassword {
    public class SetPasswordCommandHandler : BaseHandler, IRequestHandler<SetPasswordCommand, Unit> {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public SetPasswordCommandHandler (
            ICommonServiceProvider commonServicepProvider,
            UserManager<ERPIdentityUser> userManager,
            IEmailService emailService
        ) : base (commonServicepProvider) {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle (SetPasswordCommand request, CancellationToken cancellationToken) {
            var userProfile = await CoreDbContext.UserProfiles
                .Include(x => x.Identities)
                .FirstOrDefaultAsync (up => up.Id == request.Data.Id);

            if (userProfile != null && userProfile.Identities.Any (upi => upi.Type == "local")) {

                var upIdentity = userProfile.Identities.FirstOrDefault (upi => upi.Type == "local");
                var identityServerUser = await UserManagementDbContext.Users.FirstOrDefaultAsync (u => u.Id == upIdentity.Identificator);

                if (identityServerUser != null) {
                    if (request.Data.Password == request.Data.RepeatNewPassword) {
                        List<string> passwordErrors = new List<string> ();

                        var validators = _userManager.PasswordValidators;

                        foreach (var validator in validators) {
                            var result = await validator.ValidateAsync (_userManager, null, request.Data.Password);

                            if (!result.Succeeded) {
                                foreach (var error in result.Errors) {
                                    passwordErrors.Add (error.Description);
                                    throw new Exception ("Validation Error");
                                }
                            } else {
                                identityServerUser.PasswordHash = _userManager.PasswordHasher.HashPassword (identityServerUser, request.Data.Password);
                                await _userManager.UpdateAsync (identityServerUser);
                            }
                        }
                    } else {
                        throw new Exception ("New password and Repeat password aren't the same");
                    }

                    if (request.Data.EmailNotification) {
                        try {
                            string assemblyPath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location) + "/Templates";
                            var template = File.ReadAllText (assemblyPath + "/SetPassword.html");

                            template = template
                                .Replace ("{FirstName}", identityServerUser.UserName)
                                .Replace ("{Password}", request.Data.Password);

                            _emailService.QuickSendAsync (subject: "New password",
                                body : template,
                                from: "Do Not Reply",
                                to : identityServerUser.Email);
                        } catch (Exception e) {
                            Console.WriteLine ($"ERROR {e.Message}");
                        }

                    }
                }
            }

            return Unit.Value;
        }
    }
}