using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ActivateUser {
    class ActivateUserCommandHandler : BaseHandler, IRequestHandler<ActivateUserCommand, Unit> {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public ActivateUserCommandHandler (
            ICommonServiceProvider commonServicepProvider,
            UserManager<ERPIdentityUser> userManager,
            IEmailService emailService
        ) : base (commonServicepProvider) {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await CoreDbContext.UserProfiles
                .FirstOrDefaultAsync(up => up.Id == request.Id);

            if (userProfile != null)
            {
                userProfile.IsActive = true;
                await CoreDbContext.SaveChangesAsync();

                try
                {
                    string assemblyPath =
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
                    var template = await File.ReadAllTextAsync(assemblyPath + "/ActivateUser.html", cancellationToken);

                    template = template
                        .Replace("{FirstName}", userProfile.Name + ' ' + userProfile.LastName);

                    await _emailService.QuickSendAsync(subject: "Account Activation",
                        body: template,
                        from: "Do Not Reply",
                        to: userProfile.Email);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR {e.Message}");
                }
            }

            return Unit.Value;
        }
    }
}