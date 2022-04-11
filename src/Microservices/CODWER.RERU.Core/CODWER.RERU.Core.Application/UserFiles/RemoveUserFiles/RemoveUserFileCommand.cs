using MediatR;

namespace CODWER.RERU.Core.Application.UserFiles.RemoveUserFiles
{
    public class RemoveUserFileCommand : IRequest<Unit>
    {
        public string FileId { get; set; }
    }
}
