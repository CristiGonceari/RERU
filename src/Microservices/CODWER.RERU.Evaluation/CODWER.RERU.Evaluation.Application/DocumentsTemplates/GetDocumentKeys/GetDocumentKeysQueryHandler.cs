using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentCategoryKeys
{
    class GetDocumentKeysQueryHandler : IRequestHandler<GetDocumentKeysQuery, List<DocumentTemplateKeyDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDocumentKeysQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

        }

        public async Task<List<DocumentTemplateKeyDto>> Handle(GetDocumentKeysQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.DocumentTemplateKeys
                .Where(dtc => dtc.FileType == request.fileType)
                .AsQueryable();

            var mappedItems = _mapper.Map<List<DocumentTemplateKeyDto>>(items);

            return mappedItems;
        }
    }
}
