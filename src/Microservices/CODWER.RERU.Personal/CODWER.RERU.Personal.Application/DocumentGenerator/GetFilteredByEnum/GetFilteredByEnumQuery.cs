using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService.Entities;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentGenerator.GetFilteredByEnum
{
    public class GetFilteredByEnumQuery: PaginatedQueryParameter, IRequest <PaginatedModel<DocumentTemplateGeneratorDto>>
    {
        public FileTypeEnum? File { get; set; }
    }
}
