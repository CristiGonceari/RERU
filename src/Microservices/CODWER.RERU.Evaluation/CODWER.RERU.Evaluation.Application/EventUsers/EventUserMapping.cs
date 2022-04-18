using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Entities;

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
