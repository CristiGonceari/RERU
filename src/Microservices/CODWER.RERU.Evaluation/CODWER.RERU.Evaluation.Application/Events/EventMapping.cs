using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Events
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<Event, EventDto>();

            CreateMap<AddEditEventDto, Event>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Event, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(e => e.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(e => e.Name));

            CreateMap<Event, SelectEventValueDto>()
                .ForMember(x => x.EventId, opts => opts.MapFrom(e => e.Id))
                .ForMember(x => x.EventName, opts => opts.MapFrom(e => e.Name))
                .ForMember(x => x.IsEventEvaluator, opts => opts.MapFrom(src => src.EventEvaluators.Any()));
           
        }
    }
}
