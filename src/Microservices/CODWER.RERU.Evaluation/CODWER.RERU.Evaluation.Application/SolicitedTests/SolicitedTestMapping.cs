using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests
{
    public class SolicitedTestMapping : Profile
    {
        public SolicitedTestMapping()
        {
            CreateMap<SolicitedTest, SolicitedTestDto>()
                 .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
                 .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(src => src.TestTemplate.Name))
                 .ForMember(x => x.UserProfileName, opts => opts.MapFrom(src => src.UserProfile.FirstName + " " + src.UserProfile.LastName + " " + src.UserProfile.FatherName))
                 .ForMember(x => x.UserProfileIdnp, opts => opts.MapFrom(src => src.UserProfile.Idnp));

            CreateMap<SolicitedTestDto, SolicitedTest>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<AddEditSolicitedTestDto, SolicitedTest>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
