using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions
{
    public class CandidatePositionMappingProfile : Profile
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

            CreateMap<CandidatePosition, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.RequiredDocumentPositions))
                .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.Name));

        }
    }
}
