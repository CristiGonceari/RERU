using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;

namespace CODWER.RERU.Evaluation.Application.Plans
{
    public class PlanMapping : Profile
    {
        public PlanMapping()
        {
            CreateMap<Plan, PlanDto>();

            CreateMap<AddEditPlanDto, Plan>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }

}
