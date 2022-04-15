﻿using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories
{
    public class TestTemplateQuestionCategoryMapping : Profile
    {
        public TestTemplateQuestionCategoryMapping()
        {
            CreateMap<AssignQuestionCategoryToTestTemplateDto, TestTemplateQuestionCategory>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.TestCategoryQuestions, opts => opts.Ignore());

            CreateMap<TestTemplateQuestionCategory, TestTemplateQuestionCategoryDto>()
                .ForMember(x => x.CategoryName, opts => opts.MapFrom(qc => qc.QuestionCategory.Name));

            CreateMap<TestCategoryQuestionDto, TestCategoryQuestion>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}