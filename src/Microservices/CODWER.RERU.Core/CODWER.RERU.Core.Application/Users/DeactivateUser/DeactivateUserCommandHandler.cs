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

namespace CODWER.RERU.Core.Application.Users.DeactivateUser {
    class DeactivateUserCommandHandler : BaseHandler, IRequestHandler<DeactivateUserCommand, Unit> 
    {
        private readonly INotificationService _notificationService;

        public DeactivateUserCommandHandler (ICommonServiceProvider commonServiceProvider, INotificationService notificationService) 
            : base (commonServiceProvider)
        {
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle (DeactivateUserCommand request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.Id);
            if (userProfile != null)
            {
                userProfile.IsActive = false;
                await AppDbContext.SaveChangesAsync();

                try
                {
                    string assemblyPath =
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
                    var template =
                        await File.ReadAllTextAsync(assemblyPath + "/DeactivateUser.html", cancellationToken);

                    template = template
                        .Replace("{FirstName}", userProfile.FirstName + ' ' + userProfile.LastName);
                    
                    var emailData = new EmailData()
                    {
                        subject = "Account Deactivation",
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