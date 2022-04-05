using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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

            var locationValues = await _appDbContext.LocationResponsiblePersons.ToListAsync();

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
                    await _appDbContext.SaveChangesAsync();

                    var addedLocationUserId = _appDbContext.LocationResponsiblePersons.FirstOrDefault(lrp => lrp.UserProfileId == userId);

                    locationUsersIds.Add(addedLocationUserId.Id);
                }
                else 
                { 
                    locationUsersIds.Add(locationUser.Id);
                }

                locationValues = locationValues.Where(l => l.UserProfileId != userId).ToList();
                
            }

            if (locationValues.Count() > 0)
            {

                _appDbContext.LocationResponsiblePersons.RemoveRange(locationValues);
                await _appDbContext.SaveChangesAsync();
            }

            return locationUsersIds;
         }
            
    }
}
