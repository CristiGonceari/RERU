using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.GetEvent
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetEventQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var _event = await _appDbContext.Events
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<EventDto>(_event);
        }
    }
}
