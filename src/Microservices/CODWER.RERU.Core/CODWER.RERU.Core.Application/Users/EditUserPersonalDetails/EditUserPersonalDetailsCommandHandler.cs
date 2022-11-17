using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails
{
    public class EditUserPersonalDetailsCommandHandler : BaseHandler, IRequestHandler<EditUserPersonalDetailsCommand, int>
    {
        private readonly ILoggerService<EditUserPersonalDetailsCommandHandler> _loggerService;
        private readonly IMapper _mapper;

        public EditUserPersonalDetailsCommandHandler(ICommonServiceProvider commonServiceProvider,
            ILoggerService<EditUserPersonalDetailsCommandHandler> loggerService, 
            IMapper mapper
            ) : base(commonServiceProvider)
        {
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(EditUserPersonalDetailsCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.Data.Id);

            _mapper.Map(request.Data, userProfile);
            await AppDbContext.SaveChangesAsync();

            await LogAction(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($"Utilizatorul {userProfile.FullName} a fost actualizat în sistem", userProfile));
        }
    }
}