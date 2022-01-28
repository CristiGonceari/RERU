using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails 
{
    public class EditUserPersonalDetailsCommandHandler : BaseHandler, IRequestHandler<EditUserPersonalDetailsCommand, Unit>
    {
        private readonly IEvaluationClient _evaluationClient;
        private readonly ILoggerService<EditUserPersonalDetailsCommandHandler> _loggerService;

        public EditUserPersonalDetailsCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEvaluationClient evaluationClient,
            ILoggerService<EditUserPersonalDetailsCommandHandler> loggerService) : base(
            commonServiceProvider)
        {
            _evaluationClient = evaluationClient;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle (EditUserPersonalDetailsCommand request, CancellationToken cancellationToken) 
        {
            var userProfile = await CoreDbContext.UserProfiles
                .FirstOrDefaultAsync (up => up.Id == request.Data.Id);

            Mapper.Map (request.Data, userProfile);
            await CoreDbContext.SaveChangesAsync ();

            await LogAction(userProfile);
            await SyncUserProfile(userProfile);

            return Unit.Value;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($"User {userProfile.Name} {userProfile.LastName} was edited", userProfile));
        }

        private async Task SyncUserProfile(UserProfile userProfile)
        {
            await _evaluationClient.SyncUserProfile(Mapper.Map<BaseUserProfile>(userProfile));
        }
    }
}