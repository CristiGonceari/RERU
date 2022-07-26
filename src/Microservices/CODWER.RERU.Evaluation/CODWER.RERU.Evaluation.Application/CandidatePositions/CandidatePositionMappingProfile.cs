using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
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

            CreateMap<EventVacantPosition, EventDiagramDto>()
                .ForMember(x => x.EventId, opts => opts.MapFrom(e => e.EventId))
                .ForMember(x => x.EventName, opts => opts.MapFrom(e => e.Event.Name));

            CreateMap<EventTestTemplate, TestTemplateDiagramDto>()
                .ForMember(x => x.Name, opts => opts.MapFrom(t => t.TestTemplate.Name))
                .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(t => t.TestTemplate.Id));

            CreateMap<Test, TestResultDiagramDto>()
                .ForMember(x => x.TestId, opts => opts.MapFrom(t => t.Id))
                .ForMember(x => x.Result, opts => opts.MapFrom(t => t.ResultStatus))
                .ForMember(x => x.PassDate, opts => opts.MapFrom(t => t.CreateDate))
                .ForMember(x => x.Status, opts => opts.MapFrom(t => t.TestStatus));

            CreateMap<EventTestTemplate, TestsByTestTemplateDiagramDto>()
                .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(e => e.TestTemplateId))
                .ForMember(x => x.EventId, opts => opts.MapFrom(e => e.EventId));

            CreateMap<EventUser, UserDiagramDto>()
                .ForMember(x => x.UserProfileId, opts => opts.MapFrom(t => t.UserProfileId))
                .ForMember(x => x.FullName, opts => opts.MapFrom(t => t.UserProfile.FirstName + " " + t.UserProfile.LastName + " " + t.UserProfile.FatherName));


            CreateMap<CandidatePosition, SelectItem>()
              .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.Id))
              .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.Name));

            CreateMap<SolicitedVacantPosition, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.CandidatePosition.Id.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.CandidatePosition.Name));
        }
    }
}
