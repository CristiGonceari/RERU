using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Documents;

namespace CODWER.RERU.Personal.Application.DocumentGenerator.GetDocumentById
{
    public class GetDocumentByIdQueryHandler: IRequestHandler<GetDocumentByIdQuery, AddEditDocumentTemplateDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDocumentByIdQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<AddEditDocumentTemplateDto> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _appDbContext.DocumentTemplates.FirstAsync(c => c.Id == request.Id);
            var mappedData = _mapper.Map<AddEditDocumentTemplateDto>(data);

            return mappedData;
        }
    }
}
