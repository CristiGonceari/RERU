using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation
{
    public class AssignResponsiblePersonToLocationCommandHandler : IRequestHandler<AssignResponsiblePersonToLocationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignResponsiblePersonToLocationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignResponsiblePersonToLocationCommand request, CancellationToken cancellationToken)
        {
            var locationResponsiblePerson = _mapper.Map<LocationResponsiblePerson>(request.Data);

            await _appDbContext.LocationResponsiblePersons.AddAsync(locationResponsiblePerson);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
