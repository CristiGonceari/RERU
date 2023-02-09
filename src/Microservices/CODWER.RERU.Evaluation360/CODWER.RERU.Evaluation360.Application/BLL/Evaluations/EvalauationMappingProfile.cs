using AutoMapper;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.Evaluation360;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations
{
    public class EvalauationMappingProfile: Profile
    {
        public EvalauationMappingProfile()
        {
            CreateMap<Evaluation, EvaluationRowDto>()
                .ForMember(dest=> dest.EvaluatorName, opts=> opts.MapFrom(src=> $"{src.EvaluatorUserProfile.FullName}" ))
                .ForMember(dest=> dest.EvaluatedName, opts=> opts.MapFrom(src=> $"{src.EvaluatedUserProfile.FullName}" ))
                .ForMember(dest=> dest.CounterSignerName, opts=> opts.MapFrom(src=> $"{src.CounterSignerUserProfile.FullName}" ))
                ;

            CreateMap<Evaluation, PrintTableEvaluationsDto>()
                .ForMember(dest=> dest.EvaluatorName, opts=> opts.MapFrom(src=> $"{src.EvaluatorUserProfile.FullName}" ))
                .ForMember(dest=> dest.EvaluatedName, opts=> opts.MapFrom(src=> $"{src.EvaluatedUserProfile.FullName}" ))
                .ForMember(dest=> dest.CounterSignerName, opts=> opts.MapFrom(src=> $"{src.CounterSignerUserProfile.FullName}" ))
                ;

            CreateMap<Evaluation, GetEvaluationDto>()
                .ForMember(dest=> dest.EvaluatorName, opts=> opts.MapFrom(src=> $"{src.EvaluatorUserProfile.FullName}" ))
                .ForMember(dest=> dest.EvaluatedName, opts=> opts.MapFrom(src=> $"{src.EvaluatedUserProfile.FullName}" ))
                .ForMember(dest=> dest.CounterSignerName, opts=> opts.MapFrom(src=> $"{src.CounterSignerUserProfile.FullName}" ))
                .ForMember(dest=> dest.FunctionSubdivision, opts=> opts.MapFrom(src=> $"{src.EvaluatedUserProfile.EmployeeFunction.Name}"))
                .ForMember(dest=> dest.FunctionEvaluator, opts=> opts.MapFrom(src=> $"{src.EvaluatorUserProfile.EmployeeFunction.Name}"))
                .ForMember(dest=> dest.FunctionCounterSigner, opts=> opts.MapFrom(src=> $"{src.CounterSignerUserProfile.EmployeeFunction.Name}"))
                ;

            CreateMap<CreateEvaluationsCommand, Evaluation>()
                .ForMember(dest=> dest.EvaluatorUserProfileId, opts=> opts.Ignore())
                .ForMember(dest=> dest.EvaluatedUserProfileId, opts=> opts.Ignore())
                .ForMember(dest=> dest.Status, opts=> opts.MapFrom(src=> EvaluationStatusEnum.Schiță))
                ;

            CreateMap<EditEvaluationDto, Evaluation>()
                .ForMember(dest=> dest.Id, opts=> opts.Ignore())
                ;

            CreateMap<AcceptRejectEvaluationDto, Evaluation>()
                .ForMember(dest=> dest.Id, opts=> opts.Ignore())
                .ForMember(dest=> dest.CommentsEvaluated, opts=> opts.MapFrom(src=> $"{src.CommentsEvaluated}"))
                ;

            CreateMap<CounterSignAcceptRejectEvaluationDto, Evaluation>()
                .ForMember(dest=> dest.Id, opts=> opts.Ignore())
                .ForMember(dest=> dest.CheckComment1, opts=> opts.MapFrom(src=> $"{src.CheckComment1}"))
                .ForMember(dest=> dest.CheckComment2, opts=> opts.MapFrom(src=> $"{src.CheckComment2}"))
                .ForMember(dest=> dest.CheckComment3, opts=> opts.MapFrom(src=> $"{src.CheckComment3}"))
                .ForMember(dest=> dest.CheckComment4, opts=> opts.MapFrom(src=> $"{src.CheckComment4}"))
                .ForMember(dest=> dest.OtherComments, opts=> opts.MapFrom(src=> $"{src.OtherComments}"))
                ;
        }
    }
}