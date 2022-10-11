using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositionNotifications;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications
{
    public class CandidatePositionNotificationMappingProfile : Profile
    {
        public CandidatePositionNotificationMappingProfile()
        {
            CreateMap<CandidatePositionNotificationDto, CandidatePositionNotification>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
