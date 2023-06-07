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
                .Include(x => x.Contractor)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            var identity = userProfile.Identities.FirstOrDefault();
            var service = _identityServices.FirstOrDefault(s => s.Type == identity.Type);
            service?.Remove(identity.Identificator);

            await RemoveUserData(userProfile);

            await LogAction(userProfile);

            return Unit.Value;
        }

        private async Task RemoveUserData(UserProfile userProfile)
        {
            var userFiles = AppDbContext.UserFiles.Where(x => x.UserProfileId == userProfile.Id);
            var userVacantFiles = AppDbContext.SolicitedVacantPositionUserFiles.Where(x => x.UserProfileId == userProfile.Id);
            var userTests = AppDbContext.Tests.Where(x => x.UserProfileId == userProfile.Id);
            var userEvaluatedTests = AppDbContext.Tests.Where(x => x.EvaluatorId == userProfile.Id);
            var userLocations = AppDbContext.LocationResponsiblePersons.Where(x => x.UserProfileId == userProfile.Id);
            var userEvents = AppDbContext.EventResponsiblePersons.Where(x => x.UserProfileId == userProfile.Id);
            var userPlans = AppDbContext.PlanResponsiblePersons.Where(x => x.UserProfileId == userProfile.Id);
            var userEventUsers = AppDbContext.EventUsers.Where(x => x.UserProfileId == userProfile.Id);
            var userNotifications = AppDbContext.Notifications.Where(x => x.UserProfileId == userProfile.Id);
            var userEmailTests = AppDbContext.EmailTestNotifications.Where(x => x.UserProfileId == userProfile.Id);
            var userSolicitedVacantPositions = AppDbContext.SolicitedVacantPositions.Where(x => x.UserProfileId == userProfile.Id);
            var userCandidatePositions = AppDbContext.CandidatePositionNotifications.Where(x => x.UserProfileId == userProfile.Id);

            foreach (var file in userFiles)
            {
                _storageFileService.RemoveFile(file.FileId);
            }

            foreach (var file in userVacantFiles)
            {
                _storageFileService.RemoveFile(file.FileId);
            }

            AppDbContext.Tests.RemoveRange(userTests);
            AppDbContext.Tests.RemoveRange(userEvaluatedTests);
            AppDbContext.LocationResponsiblePersons.RemoveRange(userLocations);
            AppDbContext.EventResponsiblePersons.RemoveRange(userEvents);
            AppDbContext.PlanResponsiblePersons.RemoveRange(userPlans);
            AppDbContext.EventUsers.RemoveRange(userEventUsers);
            AppDbContext.Notifications.RemoveRange(userNotifications);
            AppDbContext.EmailTestNotifications.RemoveRange(userEmailTests);
            AppDbContext.SolicitedVacantPositions.RemoveRange(userSolicitedVacantPositions);
            AppDbContext.CandidatePositionNotifications.RemoveRange(userCandidatePositions);

            AppDbContext.UserProfiles.Remove(userProfile);

            await AppDbContext.SaveChangesAsync();
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($@"Utilizatorul ""{userProfile.FullName}"" a fost șters din sistem", userProfile));
        }
    }
}
