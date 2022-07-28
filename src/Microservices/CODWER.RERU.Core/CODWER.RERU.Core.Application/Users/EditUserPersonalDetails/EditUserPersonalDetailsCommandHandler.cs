using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.ServiceProvider.Clients;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails 
{
    public class EditUserPersonalDetailsCommandHandler : BaseHandler, IRequestHandler<EditUserPersonalDetailsCommand, int>
    {
        private readonly IEvaluationClient _evaluationClient;
        private readonly ILoggerService<EditUserPersonalDetailsCommandHandler> _loggerService;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;

        public EditUserPersonalDetailsCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEvaluationClient evaluationClient,
            ILoggerService<EditUserPersonalDetailsCommandHandler> loggerService,
            IStorageFileService storageFileService, IMapper mapper) : base(commonServiceProvider)
        {
            _evaluationClient = evaluationClient;
            _storageFileService = storageFileService;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle (EditUserPersonalDetailsCommand request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync (up => up.Id == request.Data.Id);

            //if (request.Data.FileDto != null)
            //{
            //    var addFile = await _storageFileService.AddFile(request.Data.FileDto);

            //    userProfile.MediaFileId = addFile;
            //}
            //else
            //{
            //    userProfile.MediaFileId = request.Data.MediaFileId;
            //}

            _mapper.Map(request.Data, userProfile);
            await AppDbContext.SaveChangesAsync();
             
            await LogAction(userProfile);
            //await SyncUserProfile(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($"User {userProfile.FirstName} {userProfile.LastName} was edited", userProfile));
        }

        //private async Task SyncUserProfile(UserProfile userProfile)
        //{
        //    await _evaluationClient.SyncUserProfile(Mapper.Map<BaseUserProfile>(userProfile));
        //}
    }
}