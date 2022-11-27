using AutoMapper;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update;
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
                .ForMember(dest=> dest.EvaluatorName, opts=> opts.MapFrom(src=> $"{src.EvaluatorUserProfile.FirstName} {src.EvaluatorUserProfile.LastName}" ))
                .ForMember(dest=> dest.EvalauatedName, opts=> opts.MapFrom(src=> $"{src.EvaluatedUserProfile.FirstName} {src.EvaluatedUserProfile.LastName}" ))
                .ForMember(dest=> dest.CounterSignerName, opts=> opts.MapFrom(src=> $"{src.CounterSignerUserProfile.FirstName} {src.CounterSignerUserProfile.LastName}" ))
                ;
            
            CreateMap<Evaluation, EditEvaluationDto>()
                .ForMember(dest=> dest.EvaluatedName, opts=> opts.MapFrom(src=> $"{src.EvaluatedUserProfile.FirstName} {src.EvaluatedUserProfile.LastName}" ))
                ;
            

            CreateMap<CreateEvaluationCommand, Evaluation>()
                .ForMember(dest=> dest.EvaluatorUserProfileId, opts=> opts.Ignore())
                .ForMember(dest=> dest.Status, opts=> opts.MapFrom(src=> EvaluationStatusEnum.Draft))
                ;

              CreateMap<EditEvaluationDto, Evaluation>()
                .ForMember(dest=> dest.Id, opts=> opts.Ignore())
                ;
               
        }
    }
}