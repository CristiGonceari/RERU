using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;

namespace CODWER.RERU.Core.Application.CandidatePositions
{
    public class CandidatePositionMappingProfile: Profile
    {
        public CandidatePositionMappingProfile()
        {
            CreateMap<AddEditCandidatePositionDto, CandidatePosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<CandidatePosition, AddEditCandidatePositionDto>();

            CreateMap<CandidatePosition, CandidatePositionDto>();
        }
    }
}
