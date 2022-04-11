using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.UserFiles.GetUserFiles
{
    public class GetUserFilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<GetFilesDto>>
    {
        public int UserId { get; set; }
    }
}
