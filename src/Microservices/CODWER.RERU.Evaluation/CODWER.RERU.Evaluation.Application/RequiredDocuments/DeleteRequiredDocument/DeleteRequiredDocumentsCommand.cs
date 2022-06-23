using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.DeleteRequiredDocument
{
    public class DeleteRequiredDocumentsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
