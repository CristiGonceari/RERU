using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests
{
    public class SolicitedTestMapping : Profile
    {
        public SolicitedTestMapping()
        {
            CreateMap<SolicitedVacantPosition, SolicitedTestDto>()
                 .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
                 .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(src => src.TestTemplate.Name))
                 .ForMember(x => x.UserProfileName, opts => opts.MapFrom(src => src.UserProfile.FirstName + " " + src.UserProfile.LastName + " " + src.UserProfile.FatherName))
                 .ForMember(x => x.UserProfileIdnp, opts => opts.MapFrom(src => src.UserProfile.Idnp))
                 .ForMember(x => x.CandidatePositionName, opts => opts.MapFrom(src => src.CandidatePosition.Name));

            CreateMap<SolicitedTestDto, SolicitedVacantPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<AddEditSolicitedTestDto, SolicitedVacantPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
