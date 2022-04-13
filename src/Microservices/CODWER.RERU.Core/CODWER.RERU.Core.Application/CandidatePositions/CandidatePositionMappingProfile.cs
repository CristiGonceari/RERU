using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

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

            CreateMap<CandidatePosition, SelectItem>()
              .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.Id))
              .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.Name));
        }
    }
}
