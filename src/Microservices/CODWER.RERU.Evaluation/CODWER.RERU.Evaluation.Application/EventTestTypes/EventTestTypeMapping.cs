using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes
{
    public class EventTestTypeMapping : Profile
    {
        public EventTestTypeMapping()
        {
            CreateMap<AddEventTestTypeDto, EventTestType>();
        }
    }
}
