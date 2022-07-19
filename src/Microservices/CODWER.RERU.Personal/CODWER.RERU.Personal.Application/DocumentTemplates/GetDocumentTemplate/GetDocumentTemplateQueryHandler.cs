using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplate
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
