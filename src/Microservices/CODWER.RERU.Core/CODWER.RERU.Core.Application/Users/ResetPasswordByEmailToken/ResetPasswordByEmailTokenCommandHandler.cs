using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Interfaces;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ResetPasswordByEmailToken {
    public class ResetPasswordByEmailTokenCommandHandler : BaseHandler, IRequestHandler<ResetPasswordByEmailTokenCommand, Unit> {
        private readonly IEmailService _emailService;
        private readonly UserManager<ERPIdentityUser> _userManager;

        public ResetPasswordByEmailTokenCommandHandler (
            ICommonServiceProvider commonServicepProvider,
            IEmailService emailService,
            UserManager<ERPIdentityUser> userManager
        ) : base (commonServicepProvider) {
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle (ResetPasswordByEmailTokenCommand request, CancellationToken cancellationToken) {
            var userProf = await CoreDbContext.UserProfiles.FirstOrDefaultAsync (u => u.Email == request.Token.Email);
            var time = DateTime.UtcNow;

            if (userProf.Token == request.Token.Token & userProf.TokenLifetime > time) {
                var user = await UserManagementDbContext.Users.FirstOrDefaultAsync (u => u.Email == userProf.Email);

                var password = StringExtensions.GenerateRandomString (12) + "!";
                user.PasswordHash = _userManager.PasswordHasher.HashPassword (user, password);
                await _userManager.UpdateAsync (user);

                string assemblyPath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location) + "/Templates";
                var template = File.ReadAllText (assemblyPath + "/ResetByEmailPassword.html");

                template = template
                    .Replace ("{FirstName}", user.Name + " " + user.LastName)
                    .Replace ("{Password}", password);

                await _emailService.QuickSendAsync (subject: "Reset Password",
                    body : template,
                    from: "Do Not Reply",
                    to : userProf.Email);
            }
            return Unit.Value;
        }
    }
}