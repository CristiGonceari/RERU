using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentGenerator.GetFilteredByEnum
{
    public class GetFilteredByEnumQuery: PaginatedQueryParameter, IRequest <PaginatedModel<DocumentTemplateGeneratorDto>>
    {
        public FileTypeEnum? File { get; set; }
    }
}
