using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes
{
    public class EventUserMapping : Profile
    {
        public EventUserMapping()
        {
            CreateMap<AddEventTestTypeDto, EventTestType>();
        }
    }
}
