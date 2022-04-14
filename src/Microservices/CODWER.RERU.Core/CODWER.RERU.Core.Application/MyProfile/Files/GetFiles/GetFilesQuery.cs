using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.MyProfile.Files.GetFiles
{
    public class GetFilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<GetFilesDto>>
    {
    }
}
