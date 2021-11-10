using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.AssignTestTypeToEvent
{
    public class AssignTestTypeToEventCommandHandler : IRequestHandler<AssignTestTypeToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignTestTypeToEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignTestTypeToEventCommand request, CancellationToken cancellationToken)
        {
            var eventTestType = _mapper.Map<EventTestType>(request.Data);

            await _appDbContext.EventTestTypes.AddAsync(eventTestType);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
