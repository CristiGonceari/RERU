using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Tests
{
    public class TestMapping : Profile
    {
        public TestMapping()
        {
            CreateMap<Test, TestDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.UserId, opts => opts.MapFrom(src => src.UserProfile.Id))
                .ForMember(x => x.Duration, opts => opts.MapFrom(src => src.TestTemplate.Duration))
                .ForMember(x => x.MinPercent, opts => opts.MapFrom(src => src.TestTemplate.MinPercent))
                .ForMember(x => x.QuestionCount, opts => opts.MapFrom(src => src.TestTemplate.QuestionCount))
                .ForMember(x => x.AccumulatedPercentage, opts => opts.MapFrom(src => src.AccumulatedPercentage))
                .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(src => src.TestTemplate.Name))
                .ForMember(x => x.LocationName, opts => opts.MapFrom(src => src.Location.Name))
                .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
                .ForMember(x => x.EvaluatorId, opts => opts.MapFrom(src => src.EvaluatorId))
                .ForMember(x => x.EventId, opts => opts.MapFrom(src => src.EventId))
                .ForMember(x => x.UserName, opts => opts.MapFrom(src => src.UserProfile.FirstName + " " + src.UserProfile.LastName + " " + src.UserProfile.Patronymic))
                .ForMember(x => x.Idnp, opts => opts.MapFrom(src => src.UserProfile.Idnp))
                .ForMember(x => x.Rules, opts => opts.MapFrom(src => src.TestTemplate.Rules))
                .ForMember(x => x.VerificationProgress, opts => opts.MapFrom(src => GetVerifiationStatus(src)))
                .ForMember(x => x.Result, opts => opts.MapFrom(src => src.ResultStatus))
                .ForMember(x => x.ViewTestResult, opts => opts.MapFrom(src => src.TestTemplate.Settings.CanViewResultWithoutVerification))
                .ForMember(x => x.ModeStatus, opts => opts.MapFrom(src => src.TestTemplate.Mode));

            CreateMap<AddEditTestDto, Test>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<UserProfile, UserProfileDto>();

            CreateMap<Test, TestResultDto>()
                .ForMember(x => x.MinPercent, opts => opts.MapFrom(src => src.TestTemplate.MinPercent))
                .ForMember(x => x.AccumulatedPercentage, opts => opts.MapFrom(src => src.AccumulatedPercentage))
                .ForMember(x => x.Status, opts => opts.MapFrom(src => ((TestStatusEnum)src.TestStatus).ToString()))
                .ForMember(x => x.Result, opts => opts.MapFrom(src => ((TestResultStatusEnum)src.ResultStatus).ToString()));
        }

        private string GetVerifiationStatus(Test inputTest)
        {
            if (inputTest.TestStatus == (int)TestStatusEnum.Programmed || inputTest.TestStatus == TestStatusEnum.AlowedToStart || inputTest.TestStatus == TestStatusEnum.InProgress || inputTest.TestQuestions == null)
            {
                return "-";
            }

            var verified = inputTest.TestQuestions.Where(x => x.Verified == VerificationStatusEnum.Verified || x.Verified == VerificationStatusEnum.VerifiedBySystem).Count();
            var all = inputTest.TestQuestions.Count;

            return $"{verified}/{all}";
        }
    }
}