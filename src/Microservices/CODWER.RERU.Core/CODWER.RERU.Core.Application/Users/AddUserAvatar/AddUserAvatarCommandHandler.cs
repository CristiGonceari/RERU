using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.StorageService;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.AddUserAvatar
{
    public class AddUserAvatarCommandHandler : BaseHandler, IRequestHandler<AddUserAvatarCommand, string>
    {
        private readonly IStorageFileService _storageFileService;

        public AddUserAvatarCommandHandler(ICommonServiceProvider commonServiceProvider, 
            IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _storageFileService = storageFileService;
        }

        public async Task<string> Handle(AddUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var user = AppDbContext.UserProfiles.FirstOrDefault(x => x.Id == request.Data.UserId);

            if (request.Data.File == null && user != null)
            {
               user.MediaFileId = string.Empty;
            }
            else if (user != null)
            {
                user.MediaFileId = await _storageFileService.AddFile(request.Data.File);
            }

            await AppDbContext.SaveChangesAsync();

            return user?.MediaFileId;
        }
    }
}
