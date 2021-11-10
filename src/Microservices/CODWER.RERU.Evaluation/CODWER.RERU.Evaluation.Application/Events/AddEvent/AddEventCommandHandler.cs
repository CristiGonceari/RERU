using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.AddEvent
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var eventToCreate = _mapper.Map<Event>(request.Data);

            await _appDbContext.Events.AddAsync(eventToCreate);

            await _appDbContext.SaveChangesAsync();

            return eventToCreate.Id;
        }
    }
}
