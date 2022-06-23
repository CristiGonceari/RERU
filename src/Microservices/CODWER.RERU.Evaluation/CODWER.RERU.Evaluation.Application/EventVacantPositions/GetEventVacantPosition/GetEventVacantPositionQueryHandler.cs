using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.GetEventVacantPosition
{
    public class GetEventVacantPositionQueryHandler : IRequestHandler<GetEventVacantPositionQuery, EventVacantPositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetEventVacantPositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<EventVacantPositionDto> Handle(GetEventVacantPositionQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.EventVacantPositions
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<EventVacantPositionDto>(item);
        }
    }
}
