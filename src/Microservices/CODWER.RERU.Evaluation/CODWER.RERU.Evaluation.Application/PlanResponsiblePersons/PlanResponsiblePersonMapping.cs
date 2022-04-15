using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons
{
    public class PlanMapping : Profile
    {
        public PlanMapping()
        {
            CreateMap<AddPlanPersonDto, PlanResponsiblePerson>();
        }
    }
}
