using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.AssignTestTemplateToEvent
{
    public class AssignTestTemplateToEventCommandHandler : IRequestHandler<AssignTestTemplateToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignTestTemplateToEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignTestTemplateToEventCommand request, CancellationToken cancellationToken)
        {
            var eventTestTemplate = _mapper.Map<EventTestTemplate>(request.Data);

            await _appDbContext.EventTestTemplates.AddAsync(eventTestTemplate);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
