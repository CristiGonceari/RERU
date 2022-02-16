using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService.Entities;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFiles
{
    public class GetContractorFilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<FileNameDto>>
    {
        public FileTypeEnum? FileType { get; set; }
    }
}
