using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    public class AssignResponsiblePersonToEventCommandHandler : IRequestHandler<AssignResponsiblePersonToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignResponsiblePersonToEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignResponsiblePersonToEventCommand request, CancellationToken cancellationToken)
        {
            var eventResponsiblePerson = _mapper.Map<EventResponsiblePerson>(request.Data);

            await _appDbContext.EventResponsiblePersons.AddAsync(eventResponsiblePerson);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
