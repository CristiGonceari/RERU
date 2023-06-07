using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.RemoveUser
{
    public class RemoveUserCommandHandler : BaseHandler, IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<RemoveUserCommandHandler> _loggerService;
        private readonly IStorageFileService _storageFileService;

        public RemoveUserCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<RemoveUserCommandHandler> loggerService, 
            IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles
                .Include(x => x.Identities)

                .Include(x => x.Tests)
                .Include(x => x.TestsWithEvaluator)
                .Include(x => x.LocationResponsiblePersons)
                .Include(x => x.EventResponsiblePersons)
                .Include(x => x.PlanResponsiblePersons)
                .Include(x => x.EventUsers)
                .Include(x => x.Notifications)
                .Include(x => x.EmailTestNotifications)
                .Include(x => x.UserFiles)
                .Include(x => x.ModuleRoles)
                .Include(x => x.SolicitedVacantPositions)
                .Include(x => x.SolicitedVacantPositionUserFiles)
                .Include(x => x.CandidatePositionNotifications)

                .Include(x => x.Contractor)

                .FirstOrDefaultAsync(u => u.Id == request.Id);

            var identity = userProfile.Identities.FirstOrDefault();
            var service = _identityServices.FirstOrDefault(s => s.Type == identity.Type);
            service.Remove(identity.Identificator);

            //foreach (var identity in userProfile.Identities)
            //{
            //    var service = _identityServices.FirstOrDefault(s => s.Type == identity.Type);
            //    service.Remove(identity.Identificator);
            //}

            foreach (var file in userProfile.UserFiles)
            {
                _storageFileService.RemoveFile(file.FileId);
            }

            foreach (var file in userProfile.SolicitedVacantPositionUserFiles)
            {
                _storageFileService.RemoveFile(file.FileId);
            }

            AppDbContext.Tests.RemoveRange(userProfile.Tests);
            AppDbContext.Tests.RemoveRange(userProfile.TestsWithEvaluator);
            AppDbContext.LocationResponsiblePersons.RemoveRange(userProfile.LocationResponsiblePersons);
            AppDbContext.EventResponsiblePersons.RemoveRange(userProfile.EventResponsiblePersons);
            AppDbContext.PlanResponsiblePersons.RemoveRange(userProfile.PlanResponsiblePersons);
            AppDbContext.EventUsers.RemoveRange(userProfile.EventUsers);
            AppDbContext.Notifications.RemoveRange(userProfile.Notifications);
            AppDbContext.EmailTestNotifications.RemoveRange(userProfile.EmailTestNotifications);
            AppDbContext.SolicitedVacantPositions.RemoveRange(userProfile.SolicitedVacantPositions);
            AppDbContext.CandidatePositionNotifications.RemoveRange(userProfile.CandidatePositionNotifications);

            AppDbContext.UserProfiles.Remove(userProfile);

            await AppDbContext.SaveChangesAsync();

            await LogAction(userProfile);

            return Unit.Value;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($@"Utilizatorul ""{userProfile.FullName}"" a fost șters din sistem", userProfile));
        }
    }
}
