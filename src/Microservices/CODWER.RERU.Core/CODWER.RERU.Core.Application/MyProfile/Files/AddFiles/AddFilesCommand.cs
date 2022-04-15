using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.MyProfile.Files.AddFiles
{
    public class AddFilesCommand : IRequest<string>
    {
        public AddFileDto File { get; set; }
    }
}
