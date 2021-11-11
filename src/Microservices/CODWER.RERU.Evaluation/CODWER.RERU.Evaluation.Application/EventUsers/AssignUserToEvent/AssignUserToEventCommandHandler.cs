using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    public class AssignUserToEventCommandHandler : IRequestHandler<AssignUserToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignUserToEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignUserToEventCommand request, CancellationToken cancellationToken)
        {
            var eventUser = _mapper.Map<EventUser>(request.Data);

            await _appDbContext.EventUsers.AddAsync(eventUser);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
