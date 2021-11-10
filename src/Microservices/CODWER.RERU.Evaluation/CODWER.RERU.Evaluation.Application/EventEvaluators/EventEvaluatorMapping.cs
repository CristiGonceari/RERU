using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators
{
    public class EventEvaluatorMapping : Profile
    {
        public EventEvaluatorMapping()
        {
            CreateMap<AddEventEvaluatorDto, EventEvaluator>();
        }
    }
}
