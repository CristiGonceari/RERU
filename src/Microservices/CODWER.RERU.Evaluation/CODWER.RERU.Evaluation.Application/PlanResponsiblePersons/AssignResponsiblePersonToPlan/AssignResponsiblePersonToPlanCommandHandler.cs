using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.AssignResponsiblePersonToPlan
{
    public class AssignResponsiblePersonToPlanCommandHandler : IRequestHandler<AssignResponsiblePersonToPlanCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignResponsiblePersonToPlanCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<int>> Handle(AssignResponsiblePersonToPlanCommand request, CancellationToken cancellationToken)
        {

            var planUsersIds = new List<int>();

            var planValues = await _appDbContext.PlanResponsiblePersons.ToListAsync();


            foreach (var userId in request.UserProfileId)
            {
                var planUser = planValues.FirstOrDefault(l => l.UserProfileId == userId);

                if (planUser == null)
                {

                    var newPlanUser = new AddPlanPersonDto()
                    {
                        UserProfileId = userId,
                        PlanId = request.PlanId,
                    };

                    var planResponsiblePerson = _mapper.Map<PlanResponsiblePerson>(newPlanUser);

                    await _appDbContext.PlanResponsiblePersons.AddAsync(planResponsiblePerson);
                    await _appDbContext.SaveChangesAsync();

                    var planName = await _appDbContext.PlanResponsiblePersons.FirstAsync(x => x.UserProfileId == userId);

                    planUsersIds.Add(planName.Id);
                }
                else
                {
                    planUsersIds.Add(planUser.Id);
                }

                planValues = planValues.Where(l => l.UserProfileId != userId).ToList();
            }

            if (planValues.Count() > 0)
            {
                _appDbContext.PlanResponsiblePersons.RemoveRange(planValues);
                await _appDbContext.SaveChangesAsync();
            }

            return planUsersIds;
        }
    }

}
