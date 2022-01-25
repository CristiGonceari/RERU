using CODWER.RERU.Personal.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentGenerator.GetDocumentById
{
    public class GetDocumentByIdQuery: IRequest<AddEditDocumentTemplateDto>
    {
        public int Id { get; set; }
    }
}
