using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions
{
    public class SolicitedTestMapping : Profile
    {
        public SolicitedTestMapping()
        {
            CreateMap<SolicitedVacantPosition, SolicitedCandidatePositionDto>()
                 .ForMember(x => x.UserProfileName, opts => opts.MapFrom(src => src.UserProfile.FirstName + " " + src.UserProfile.LastName + " " + src.UserProfile.FatherName))
                 .ForMember(x => x.UserProfileIdnp, opts => opts.MapFrom(src => src.UserProfile.Idnp))
                 .ForMember(x => x.CandidatePositionName, opts => opts.MapFrom(src => src.CandidatePosition.Name))
                 .ForMember(x => x.SolicitedTime, opts => opts.MapFrom(src => src.CreateDate))
                 .ForMember(x => x.AttachedFilesCount, opts => opts.MapFrom(src => src.SolicitedVacantPositionUserFiles.Count))
                 .ForMember(x => x.RequiredAttachedFilesCount, opts => opts.MapFrom(src => src.CandidatePosition.RequiredDocumentPositions.Count))
                 .ForMember(x => x.PhoneNumber, opts => opts.MapFrom(src => src.UserProfile.PhoneNumber))
                 .ForMember(x => x.Email, opts => opts.MapFrom(src => src.UserProfile.Email))
                 .ForMember(x => x.SolicitedTestStatus, opts => opts.MapFrom(src => src.SolicitedPositionStatus));

            CreateMap<SolicitedCandidatePositionDto, SolicitedVacantPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<AddEditSolicitedPositionDto, SolicitedVacantPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Event, EventsWithTestTemplateDto>();
        }
    }
}
