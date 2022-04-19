using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateDocumentReplacedKeys
{
    public  class GetTestTemplateDocumentReplacedKeysQuery : IRequest<string>
    {
        public int TestTemplateId { get; set; }

        public int DocumentTemplateId { get; set; }
    }
}
