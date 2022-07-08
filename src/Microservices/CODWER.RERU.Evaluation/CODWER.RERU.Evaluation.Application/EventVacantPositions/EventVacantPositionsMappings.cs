using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using RERU.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions
{
    public class EventVacantPositionsMappings : Profile
    {
        public EventVacantPositionsMappings()
        {
            CreateMap<EventVacantPosition, AddEditEventVacantPositionDto>();
            CreateMap<AddEditEventVacantPositionDto, EventVacantPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<List<RequiredDocument>, List<RequiredDocumentDto>>();

            CreateMap<RequiredDocumentPosition, RequiredDocumentDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.RequiredDocument.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.RequiredDocument.Name));

            CreateMap<EventVacantPosition, EventsWithTestTemplateDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Event.Id))
                .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
                .ForMember(x => x.TestTemplateNames, opts => opts.MapFrom(src => src.Event.EventTestTemplates));

        }
    }
}
