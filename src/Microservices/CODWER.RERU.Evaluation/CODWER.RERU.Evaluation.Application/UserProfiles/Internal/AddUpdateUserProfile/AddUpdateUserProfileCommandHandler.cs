using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.Internal.AddUpdateUserProfile
{
    public class AddUpdateUserProfileCommandHandler : IRequestHandler<AddUpdateUserProfileCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddUpdateUserProfileCommandHandler> _loggerService;

        public AddUpdateUserProfileCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<AddUpdateUserProfileCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(AddUpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfileInEvaluation = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.CoreUserId == request.Data.CoreUserId);

            if (userProfileInEvaluation != null)
            {
                _mapper.Map(request.Data, userProfileInEvaluation);

                await SaveAndLog(userProfileInEvaluation, "was updated");
            }
            else
            {
                var mappedItem = _mapper.Map<UserProfile>(request.Data);
                await _appDbContext.UserProfiles.AddAsync(mappedItem);

                await SaveAndLog(mappedItem, "was created");
            }

            return Unit.Value;
        }

        private async Task SaveAndLog(UserProfile up, string action)
        {
            await _appDbContext.SaveChangesAsync();
            await _loggerService.Log(LogData.AsEvaluation($"UserProfile {up.FirstName} {up.LastName} {action}", up));
        }
    }
}
