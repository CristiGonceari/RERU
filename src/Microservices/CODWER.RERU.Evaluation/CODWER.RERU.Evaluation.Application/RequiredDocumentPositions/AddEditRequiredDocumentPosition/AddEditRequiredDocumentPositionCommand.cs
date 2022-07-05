using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.AddEditRequiredDocumentPosition
{
    public class AddEditRequiredDocumentPositionCommand : IRequest<int>
    {
        public AddEditRequiredDocumentPositionDto Data { get; set; }
    }
}
