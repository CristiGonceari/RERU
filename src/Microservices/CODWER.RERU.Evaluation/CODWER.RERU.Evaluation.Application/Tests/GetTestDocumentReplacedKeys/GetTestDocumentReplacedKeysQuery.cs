using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestDocumentReplacedKeys
{
    public class GetTestDocumentReplacedKeysQuery : IRequest<string>
    {
        public int TestId { get; set; }
        public int DocumentTemplateId { get; set; }
    }
}
