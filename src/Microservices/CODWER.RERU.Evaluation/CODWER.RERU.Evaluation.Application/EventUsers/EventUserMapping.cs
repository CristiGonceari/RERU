using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.EventUsers
{
    public class EventResponsiblePersonMapping : Profile
    {
        public EventResponsiblePersonMapping()
        {
            CreateMap<AddEventPersonDto, EventUser>();
        }
    }
}
