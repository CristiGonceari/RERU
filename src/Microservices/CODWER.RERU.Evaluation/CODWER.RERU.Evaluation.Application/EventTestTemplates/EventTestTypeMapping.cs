using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

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
