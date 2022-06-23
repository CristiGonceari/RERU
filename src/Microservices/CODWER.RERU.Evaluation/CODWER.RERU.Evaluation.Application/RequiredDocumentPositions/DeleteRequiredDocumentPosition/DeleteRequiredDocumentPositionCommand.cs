using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.DeleteRequiredDocumentPosition
{
    public class DeleteRequiredDocumentPositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
