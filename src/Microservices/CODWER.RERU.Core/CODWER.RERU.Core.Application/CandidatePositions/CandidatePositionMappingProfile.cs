using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.CandidatePositions
{
    public class CandidatePositionMappingProfile : Profile
    {
        public CandidatePositionMappingProfile()
        {
            CreateMap<CandidatePosition, CandidatePositionDto>();
        }
    }
}
