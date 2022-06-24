using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Users;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Identity.Models;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using System.Linq;

namespace CODWER.RERU.Core.Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ERPIdentityUser, UserDto>();
            CreateMap<ERPIdentityUser, CreateUserDto>()
                .ForMember(destinationMember => destinationMember.EmailNotification, options => options.Ignore());
            CreateMap<ERPIdentityUser, UserPersonalDataDto>();

            CreateMap<UserProfile, UserPersonalDataDto>();

            CreateMap<UserDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.LockoutEnabled, options => options.Ignore())
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<CreateUserDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.UserName, options => options.MapFrom(source => source.Email))
                .ForMember(destinationMember => destinationMember.LockoutEnabled, options => options.Ignore())
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<CreateUserDto, UserProfile>()
                .ForMember(x => x.IsActive, opts => opts.MapFrom(x => true));

            CreateMap<EditUserFromColaboratorDto, UserProfile>()
                .ForMember(x => x.IsActive, opts => opts.MapFrom(x => true));

            CreateMap<AddUserDto, UserProfile>()
                .ForMember(x => x.IsActive, opts => opts.MapFrom(x => true));

            CreateMap<UserPersonalDataDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<ERPIdentityUser, EditUserDto>();
            CreateMap<EditUserDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<UserProfile, EditUserPersonalDetailsDto>();

            CreateMap<EditUserPersonalDetailsDto, UserProfile>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<UserProfile, EditCandidateDto>();

            CreateMap<EditCandidateDto, UserProfile>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<Test, UserTestDto>()
               .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
               .ForMember(x => x.UserId, opts => opts.MapFrom(src => src.UserProfile.Id))
               .ForMember(x => x.MinPercent, opts => opts.MapFrom(src => src.TestTemplate.MinPercent))
               .ForMember(x => x.QuestionCount, opts => opts.MapFrom(src => src.TestTemplate.QuestionCount))
               .ForMember(x => x.AccumulatedPercentage, opts => opts.MapFrom(src => src.AccumulatedPercentage))
               .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(src => src.TestTemplate.Name))
               .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
               .ForMember(x => x.EventId, opts => opts.MapFrom(src => src.EventId))
               .ForMember(x => x.UserName, opts => opts.MapFrom(src => src.UserProfile.FirstName + " " + src.UserProfile.LastName + " " + src.UserProfile.FatherName))
               .ForMember(x => x.Result, opts => opts.MapFrom(src => src.ResultStatus))
               .ForMember(x => x.VerificationProgress, opts => opts.MapFrom(src => GetVerifiationStatus(src)));

            CreateMap<EmailVerificationCodeDto, EmailVerification>()
                .ForMember(x => x.Id, opts => opts.Ignore());
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