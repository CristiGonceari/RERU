using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService.Entities;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorFiles
{
    public class GetContractorFilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<FileNameDto>>
    {
        public int ContractorId { get; set; }
        public FileTypeEnum? FileType { get; set; }
    }
}
