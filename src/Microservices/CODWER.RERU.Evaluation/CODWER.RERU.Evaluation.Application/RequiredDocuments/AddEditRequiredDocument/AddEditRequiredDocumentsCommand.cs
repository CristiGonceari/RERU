using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.AddEditRequiredDocument
{
    public class AddEditRequiredDocumentsCommand : IRequest<int>
    {
        public AddEditRequiredDocumentsDto Data { get; set; }
    }
}
