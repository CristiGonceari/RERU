using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateDocumentReplacedKeys
{
    public  class GetTestTemplateDocumentReplacedKeysQuery : IRequest<string>
    {
        public int TestTemplateId { get; set; }

        public int DocumentTemplateId { get; set; }
    }
}
