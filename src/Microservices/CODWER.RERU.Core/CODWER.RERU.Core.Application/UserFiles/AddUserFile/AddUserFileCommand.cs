using CODWER.RERU.Core.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.UserFiles.AddUserFile
{
    public class AddUserFileCommand : IRequest<string>
    {
        public AddEditUserFileDto Data { get; set; }
    }
}
