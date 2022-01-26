using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFiles
{
    public class GetContractorFilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<FileNameDto>>
    {
        public FileTypeEnum? Type { get; set; }
    }
}
