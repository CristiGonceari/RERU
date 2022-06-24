using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.GetRequiredDocumentPosition
{
    public class GetRequiredDocumentPositionQuery : IRequest<RequiredDocumentPositionDto>
    {
        public int Id { get; set; }
    }
}
