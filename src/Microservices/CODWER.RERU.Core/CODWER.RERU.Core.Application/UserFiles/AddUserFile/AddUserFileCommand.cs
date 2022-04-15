using CODWER.RERU.Core.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.UserFiles.AddUserFile
{
    public class AddUserFileCommand : IRequest<string>
    {
        public AddUserFileDto Data { get; set; }
    }
}
