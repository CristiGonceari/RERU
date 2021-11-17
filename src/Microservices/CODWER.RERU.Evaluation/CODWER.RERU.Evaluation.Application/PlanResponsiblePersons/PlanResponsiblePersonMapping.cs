using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;

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
