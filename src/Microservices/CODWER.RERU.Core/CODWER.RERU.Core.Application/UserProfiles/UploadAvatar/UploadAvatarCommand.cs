using MediatR;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Core.Application.UserProfiles.UploadAvatar
{
    public class UploadAvatarCommand : IRequest<Unit>
    {
        public UploadAvatarCommand(IFormFile file)
        {
            File = file;
        }
        public IFormFile File { get; set; }
    }
}