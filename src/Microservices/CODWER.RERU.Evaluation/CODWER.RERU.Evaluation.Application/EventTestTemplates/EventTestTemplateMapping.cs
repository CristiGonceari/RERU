using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates
{
    public class EventTestTemplateMapping : Profile
    {
        public EventTestTemplateMapping()
        {
            CreateMap<AddEventTestTemplateDto, EventTestTemplate>();

            CreateMap<Event, TestTemplatesByEventDto>()
                .ForMember(x => x.EventId, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.EventName, opts => opts.MapFrom(x => x.Name));

            CreateMap<EventTestTemplate, SelectTestTemplateValueDto>()
                .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(x => x.TestTemplateId))
                .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(x => x.TestTemplate.Name));
        }
    }
}
