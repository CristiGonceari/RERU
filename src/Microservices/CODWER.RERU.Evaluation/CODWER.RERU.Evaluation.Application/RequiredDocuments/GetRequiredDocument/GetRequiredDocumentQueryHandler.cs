using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocument
{
    public class GetRequiredDocumentQueryHandler : IRequestHandler<GetRequiredDocumentQuery, RequiredDocumentDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetRequiredDocumentQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RequiredDocumentDto> Handle(GetRequiredDocumentQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.RequiredDocuments
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<RequiredDocumentDto>(item);
        }
    }
}
