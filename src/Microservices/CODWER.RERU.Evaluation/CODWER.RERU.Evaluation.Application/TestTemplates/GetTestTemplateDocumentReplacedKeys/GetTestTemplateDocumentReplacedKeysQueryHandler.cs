using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateDocumentReplacedKeys
{
    public class GetTestTemplateDocumentReplacedKeysQueryHandler : IRequestHandler<GetTestTemplateDocumentReplacedKeysQuery, string>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGetTestTemplateDocumentReplacedKeys _getTestTemplateDocumentReplacedKeys;

        public GetTestTemplateDocumentReplacedKeysQueryHandler(AppDbContext appDbContext, IGetTestTemplateDocumentReplacedKeys getTestTemplateDocumentReplacedKeys) 
        {
            _appDbContext = appDbContext;
            _getTestTemplateDocumentReplacedKeys = getTestTemplateDocumentReplacedKeys;
        }

        public async Task<string> Handle(GetTestTemplateDocumentReplacedKeysQuery request, CancellationToken cancellationToken)
        {
            var testTemplateValues = _appDbContext.TestTemplates.Include(tt => tt.Settings).FirstOrDefault(tt => tt.Id == request.TestTemplateId);

            var replacedKeys = await _getTestTemplateDocumentReplacedKeys.GetTestTemplateDocumentReplacedKey(testTemplateValues, request.DocumentTemplateId);

            return replacedKeys;
        }
    }
}
