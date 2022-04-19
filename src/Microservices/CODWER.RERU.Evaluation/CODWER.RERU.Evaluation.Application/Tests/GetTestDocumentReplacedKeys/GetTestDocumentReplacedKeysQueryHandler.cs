using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestDocumentReplacedKeys
{
    public class GetTestDocumentReplacedKeysQueryHandler : IRequestHandler<GetTestDocumentReplacedKeysQuery, string>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGetTestDocumentReplacedKeys _getTestDocumentReplacedKeys;

        public GetTestDocumentReplacedKeysQueryHandler(AppDbContext appDbContext, IGetTestDocumentReplacedKeys getTestDocumentReplacedKeys)
        {
            _appDbContext = appDbContext;
            _getTestDocumentReplacedKeys = getTestDocumentReplacedKeys;
        }

        public async Task<string> Handle(GetTestDocumentReplacedKeysQuery request, CancellationToken cancellationToken)
        {
            var testValues = _appDbContext.Tests
                                        .Include(t => t.UserProfile)
                                        .Include(t => t.TestTemplate)
                                        .Include(t => t.Location)
                                        .Include(t => t.Event)
                                        .FirstOrDefault(t => t.Id == request.TestId);

            var replacedKeys = await _getTestDocumentReplacedKeys.GetTestDocumentReplacedKey(testValues, request.DocumentTemplateId);

            return replacedKeys;
        }
    }
}
