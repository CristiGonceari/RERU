using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.UserFiles.GetUserFile
{
    public class GetUserFileQuery : IRequest<FileDataDto>
    {
        public string FileId { get; set; }
    }
}
