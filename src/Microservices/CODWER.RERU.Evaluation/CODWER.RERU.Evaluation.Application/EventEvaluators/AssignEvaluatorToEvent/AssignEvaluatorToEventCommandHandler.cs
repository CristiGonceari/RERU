using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandHandler : IRequestHandler<AssignEvaluatorToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignEvaluatorToEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignEvaluatorToEventCommand request, CancellationToken cancellationToken)
        {
            var eventEvaluator = _mapper.Map<EventEvaluator>(request.Data);

            await _appDbContext.EventEvaluators.AddAsync(eventEvaluator);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
