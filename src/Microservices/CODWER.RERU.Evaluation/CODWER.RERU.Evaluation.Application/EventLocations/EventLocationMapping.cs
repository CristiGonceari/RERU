using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.EventLocations
{
    public class EventTestTemplateMapping : Profile
    {
        public EventTestTemplateMapping()
        {
            CreateMap<AddEventLocationDto, EventLocation>();
        }
    }
}
