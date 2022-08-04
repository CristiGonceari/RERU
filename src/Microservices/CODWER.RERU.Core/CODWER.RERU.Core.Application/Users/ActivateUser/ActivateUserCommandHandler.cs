using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ActivateUser {
    public class ActivateUserCommandHandler : BaseHandler, IRequestHandler<ActivateUserCommand, Unit> 
    {
        private readonly INotificationService _notificationService;

        public ActivateUserCommandHandler (ICommonServiceProvider commonServiceProvider, INotificationService notificationService) 
            : base (commonServiceProvider) 
        {
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync(up => up.Id == request.Id);

            if (userProfile != null)
            {
                userProfile.IsActive = true;
                await AppDbContext.SaveChangesAsync();

                await _notificationService.PutEmailInQueue(new QueuedEmailData
                {
                    Subject = "Account Activation",
                    To = userProfile.Email,
                    HtmlTemplateAddress = "Templates/ActivateUser.html",
                    ReplacedValues = new Dictionary<string, string>()
                    {
                        { "{FirstName}", userProfile.FullName }
                    }
                });
            }

            return Unit.Value;
        }
    }
}