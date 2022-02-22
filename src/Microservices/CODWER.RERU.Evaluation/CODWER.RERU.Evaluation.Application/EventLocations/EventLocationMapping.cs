using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

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
