using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

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

            var locationValues = await _appDbContext.LocationResponsiblePersons.Where(lrp => lrp.LocationId == request.LocationId).ToListAsync();

            foreach (var userId in request.UserProfileId)
            {
                var locationUser = locationValues.FirstOrDefault(l => l.UserProfileId == userId);

                if (locationUser == null)
                {

                    var newLocationUser = new AddLocationPersonDto()
                    {
                        UserProfileId = userId,
                        LocationId = request.LocationId,
                    };

                    var locationResponsiblePerson = _mapper.Map<LocationResponsiblePerson>(newLocationUser);

                    await _appDbContext.LocationResponsiblePersons.AddAsync(locationResponsiblePerson);

                    locationUsersIds.Add(userId);
                }
                else 
                { 
                    locationUsersIds.Add(locationUser.UserProfileId);
                }

                locationValues = locationValues.Where(l => l.UserProfileId != userId).ToList();
                
            }

            if (locationValues.Count() > 0)
            {

                _appDbContext.LocationResponsiblePersons.RemoveRange(locationValues);
            }

            await _appDbContext.SaveChangesAsync();

            return locationUsersIds;
         }
            
    }
}
