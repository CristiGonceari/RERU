using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation
{
    public class AssignResponsiblePersonToLocationCommandHandler : IRequestHandler<AssignResponsiblePersonToLocationCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignResponsiblePersonToLocationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<int>> Handle(AssignResponsiblePersonToLocationCommand request, CancellationToken cancellationToken)
        {
            var locationUsersIds = new List<int>();

            foreach (var userId in request.UserProfileId)
            {
                var locationUser = new AddLocationPersonDto()
                {
                    UserProfileId = userId,
                    LocationId = request.LocationId,
                };

                var locationResponsiblePerson = _mapper.Map<LocationResponsiblePerson>(locationUser);

                await _appDbContext.LocationResponsiblePersons.AddAsync(locationResponsiblePerson);
                await _appDbContext.SaveChangesAsync();

                var locationName = _appDbContext.LocationResponsiblePersons.FirstOrDefault(lrp => lrp.UserProfileId == userId);

                locationUsersIds.Add(locationName.Id);
            }

            return locationUsersIds;
        }
    }
}
