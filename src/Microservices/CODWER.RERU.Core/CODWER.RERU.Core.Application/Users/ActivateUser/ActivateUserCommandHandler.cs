using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ActivateUser {
    class ActivateUserCommandHandler : BaseHandler, IRequestHandler<ActivateUserCommand, Unit> 
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly INotificationService _notificationService;

        public ActivateUserCommandHandler (ICommonServiceProvider commonServicepProvider, UserManager<ERPIdentityUser> userManager, INotificationService notificationService) 
            : base (commonServicepProvider) 
        {
            _userManager = userManager;
            _notificationService = notificationService;
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

                    var emailData = new EmailData()
                    {
                        subject = "Account Activation",
                        body = template,
                        from = "Do Not Reply",
                        to = userProfile.Email
                    };

                    await _notificationService.Notify(emailData, NotificationType.Both);
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