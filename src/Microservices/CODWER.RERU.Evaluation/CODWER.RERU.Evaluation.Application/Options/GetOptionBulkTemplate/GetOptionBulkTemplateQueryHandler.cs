using CODWER.RERU.Evaluation.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Options.GetOptionBulkTemplate
{
    public class GetOptionBulkTemplateQueryHandler : IRequestHandler<GetOptionBulkTemplateQuery, byte[]>
    {
        private readonly IOptionService _optionService;

        public GetOptionBulkTemplateQueryHandler(IOptionService optionService)
        {
            _optionService = optionService;
        }

        public async Task<byte[]> Handle(GetOptionBulkTemplateQuery request, CancellationToken cancellationToken)
        {
            return await _optionService.GenerateExcelTemplate(request.QuestionType);
        }

    }
}
