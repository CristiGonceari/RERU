using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates
{
    public class EventTestTemplateMapping : Profile
    {
        public EventTestTemplateMapping()
        {
            CreateMap<AddEventTestTemplateDto, EventTestTemplate>();
        }
    }
}
