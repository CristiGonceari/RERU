using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions
{
    public class EventVacantPositionsMappings : Profile
    {
        public EventVacantPositionsMappings()
        {
            CreateMap<EventVacantPosition, AddEditEventVacantPositionDto>();
            CreateMap<AddEditEventVacantPositionDto, EventVacantPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<EventVacantPosition, EventVacantPositionDto>();
        }
    }
}
