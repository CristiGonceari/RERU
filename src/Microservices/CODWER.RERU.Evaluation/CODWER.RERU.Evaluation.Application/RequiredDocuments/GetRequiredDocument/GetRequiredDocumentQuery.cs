using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocument
{
    public class GetRequiredDocumentQuery : IRequest<RequiredDocumentDto>
    {
        public int Id { get; set; }
    }
}
