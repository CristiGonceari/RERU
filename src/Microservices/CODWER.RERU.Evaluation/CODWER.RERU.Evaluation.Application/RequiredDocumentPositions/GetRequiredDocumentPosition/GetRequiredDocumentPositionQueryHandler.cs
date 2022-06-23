using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.GetRequiredDocumentPosition
{
    public class GetRequiredDocumentPositionQueryHandler : IRequestHandler<GetRequiredDocumentPositionQuery, RequiredDocumentPositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetRequiredDocumentPositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RequiredDocumentPositionDto> Handle(GetRequiredDocumentPositionQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.RequiredDocumentPositions
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<RequiredDocumentPositionDto>(item);
        }
    }
}
