using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventLocations.AssignLocationToEvent
{
    public class AssignLocationToEventCommandHandler : IRequestHandler<AssignLocationToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignLocationToEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignLocationToEventCommand request, CancellationToken cancellationToken)
        {
            var eventLocation = _mapper.Map<EventLocation>(request.Data);

            await _appDbContext.EventLocations.AddAsync(eventLocation);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
