using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentTemplate
{
    public class GetDocumentTemplateQueryHandler : IRequestHandler<GetDocumentTemplateQuery, AddEditDocumentTemplateDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDocumentTemplateQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<AddEditDocumentTemplateDto> Handle(GetDocumentTemplateQuery request, CancellationToken cancellationToken)
        {
            var data = await _appDbContext.DocumentTemplates.FirstAsync(c => c.Id == request.Id);
            var mappedData = _mapper.Map<AddEditDocumentTemplateDto>(data);

            return mappedData;
        }
    }
}
