using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorFiles
{
    public class GetContractorFilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<FileNameDto>>
    {
        public int ContractorId { get; set; }
        public FileTypeEnum? Type { get; set; }
    }
}
