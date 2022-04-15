using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons
{
    public class EventEvaluatorMapping : Profile
    {
        public EventEvaluatorMapping()
        {
            CreateMap<AddEventPersonDto, EventResponsiblePerson>();
        }
    }
}
