﻿using System.Collections.Generic;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.InternalTest;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.DocumentForSign;

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
                .ForMember(x => x.LocationNames, opts => opts.MapFrom(src => new List<string>{src.Location.Name, src.Location.Address}))
                .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
                .ForMember(x => x.EvaluatorId, opts => opts.MapFrom(src => src.EvaluatorId))
                .ForMember(x => x.EventId, opts => opts.MapFrom(src => src.EventId))
                .ForMember(x => x.UserName, opts => opts.MapFrom(src => src.UserProfile.FullName))
                .ForMember(x => x.Idnp, opts => opts.MapFrom(src => src.UserProfile.Idnp))
                .ForMember(x => x.Rules, opts => opts.MapFrom(src => src.TestTemplate.Rules))
                .ForMember(x => x.VerificationProgress, opts => opts.MapFrom(src => GetVerifiationStatus(src)))
                .ForMember(x => x.Result, opts => opts.MapFrom(src => src.ResultStatus))
                .ForMember(x => x.ResultValue, opts => opts.MapFrom(src => src.ResultStatusValue))
                .ForMember(x => x.ViewTestResult, opts => opts.MapFrom(src => src.TestTemplate.Settings.CanViewResultWithoutVerification))
                .ForMember(x => x.ModeStatus, opts => opts.MapFrom(src => src.TestTemplate.Mode))
                .ForMember(x => x.EvaluatorName, opts => opts.MapFrom(src => src.Evaluator.FullName))
                .ForMember(x => x.EvaluatorIdnp, opts => opts.MapFrom(src => src.Evaluator.Idnp))
                .ForMember(x => x.EndProgrammedTime, opts => opts.MapFrom(src => src.Event.TillDate))
                .ForMember(x => x.CanStartWithoutConfirmation, opts => opts.MapFrom(src => CheckCanStartWithoutConfirmation(src)))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(src => src.UserProfile.Department.Name))
                .ForMember(x => x.RoleName, opts => opts.MapFrom(src => src.UserProfile.Role.Name))
                .ForMember(x => x.FunctionName, opts => opts.MapFrom(src => src.UserProfile.EmployeeFunction.Name))
                .ForMember(x => x.FinalAccumulatedPercentage, opts => opts.MapFrom(src => src.FinalAccumulatedPercentage))
                .ForMember(x => x.FinalResult, opts => opts.MapFrom(src => src.FinalStatusResult))
                .ForMember(x => x.HashGroupKey, opts => opts.MapFrom(src => src.HashGroupKey))
                .ForMember(x => x.IsVerificatedAutomat, opts => opts.MapFrom(src => CheckIfTestIsCalculatedBySystem(src)))
                .ForMember(x => x.CreateById, opts => opts.MapFrom(src => src.CreateById));

            CreateMap<AddEditTestDto, Test>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Test, TestDataDto>()
                .ForMember(x => x.TestId, opts => opts.MapFrom(src => src.Id));

            CreateMap<Test, GetTestForFastStartDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.EventName, opts => opts.MapFrom(src => src.Event.Name))
                .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(src => src.TestTemplate.Name))
                .ForMember(x => x.ProgrammedTime, opts => opts.MapFrom(src => src.ProgrammedTime))
                .ForMember(x => x.EndProgrammedTime, opts => opts.MapFrom(src => src.EndProgrammedTime))
                .ForMember(x => x.CanStartWithoutConfirmation, opts => opts.MapFrom(src => CheckCanStartWithoutConfirmation(src)));

            CreateMap<DocumentsForSign, DocumentForSignDto>()
                .ForMember(x => x.DocumentForSignId, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.FileName, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.MediaFileId, opts => opts.MapFrom(src => src.MediaFileId));

            CreateMap<SignedDocument, SignedDocumentDto>()
                .ForMember(x => x.UserProfileId, opts => opts.MapFrom(src => src.UserProfileId))
                .ForMember(x => x.FullName, opts => opts.MapFrom(src => src.UserProfile.FullName))
                .ForMember(x => x.SignRequestId, opts => opts.MapFrom(src => src.SignRequestId))
                .ForMember(x => x.Status, opts => opts.MapFrom(src => src.Status));

        }

        private string GetVerifiationStatus(Test inputTest)
        {
            if (inputTest.TestStatus == (int)TestStatusEnum.Programmed || inputTest.TestStatus == TestStatusEnum.AlowedToStart || inputTest.TestStatus == TestStatusEnum.InProgress || inputTest.TestQuestions == null)
            {
                return "-";
            }

            var verified = inputTest.TestQuestions.Count(x => x.Verified is VerificationStatusEnum.Verified or VerificationStatusEnum.VerifiedBySystem);
            var all = inputTest.TestQuestions.Count;

            return $"{verified}/{all}";
        }

        private bool CheckCanStartWithoutConfirmation(Test test)
        {
            if (test.TestTemplate.Settings == null) return false;

            if (test.TestStatus is TestStatusEnum.AlowedToStart) return true;

            return test.TestTemplate.Settings.StartWithoutConfirmation && test.TestPassStatus is not TestPassStatusEnum.Forbidden or null;
        }

        private bool CheckIfTestIsCalculatedBySystem(Test test)
        {
           return test.TestQuestions.All(x => x.QuestionUnit?.QuestionType is QuestionTypeEnum.OneAnswer or QuestionTypeEnum.MultipleAnswers);
        }
    }
}