using System;
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

            CreateMap<CandidatePosition, CandidatePositionDto>()
                .ForMember(x => x.IsActive, opts => opts.MapFrom(c => c.IsActive && c.From <= DateTime.Now && c.To >= DateTime.Now));

            CreateMap<EventVacantPosition, EventDiagramDto>()
                .ForMember(x => x.EventId, opts => opts.MapFrom(e => e.EventId))
                .ForMember(x => x.EventName, opts => opts.MapFrom(e => e.Event.Name))
                .ForMember(x => x.IsActive, opts => opts.MapFrom(e => e.Event.TillDate > DateTime.Now));

            CreateMap<EventTestTemplate, TestTemplateDiagramDto>()
                .ForMember(x => x.Name, opts => opts.MapFrom(t => t.TestTemplate.Name))
                .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(t => t.TestTemplate.Id))
                .ForMember(x => x.EventId, opts => opts.MapFrom(t => t.EventId))
                .ForMember(x => x.Mode, opts => opts.MapFrom(t => t.TestTemplate.Mode));

            CreateMap<Test, TestResultDiagramDto>()
                .ForMember(x => x.TestId, opts => opts.MapFrom(t => t.Id))
                .ForMember(x => x.Result, opts => opts.MapFrom(t => t.ResultStatus))
                .ForMember(x => x.ResultValue, opts => opts.MapFrom(t => t.ResultStatusValue))
                .ForMember(x => x.PassDate, opts => opts.MapFrom(t => t.CreateDate))
                .ForMember(x => x.Status, opts => opts.MapFrom(t => t.TestStatus));

            CreateMap<EventTestTemplate, TestsByTestTemplateDiagramDto>()
                .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(e => e.TestTemplateId))
                .ForMember(x => x.EventId, opts => opts.MapFrom(e => e.EventId));

            CreateMap<EventUserCandidatePosition, UserDiagramDto>()
                .ForMember(x => x.UserProfileId, opts => opts.MapFrom(t => t.EventUser.UserProfileId))
                .ForMember(x => x.FullName, opts => opts.MapFrom(t => t.EventUser.UserProfile.FullName));

            CreateMap<UserProfile, UserDiagramDto>()
                .ForMember(x => x.UserProfileId, opts => opts.MapFrom(t => t.Id))
                .ForMember(x => x.FullName, opts => opts.MapFrom(t => t.FullName));

            CreateMap<CandidatePosition, SelectItem>()
              .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.Id))
              .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.Name));

            CreateMap<SolicitedVacantPosition, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.CandidatePosition.Id.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.CandidatePosition.Name));
        }
    }
}
