using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories
{
    public class TestTemplateQuestionCategoryMapping : Profile
    {
        public TestTemplateQuestionCategoryMapping()
        {
            CreateMap<AssignQuestionCategoryToTestTemplateDto, TestTypeQuestionCategory>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.TestCategoryQuestions, opts => opts.Ignore());

            CreateMap<TestTypeQuestionCategory, TestTypeQuestionCategoryDto>()
                .ForMember(x => x.CategoryName, opts => opts.MapFrom(qc => qc.QuestionCategory.Name));

            CreateMap<TestCategoryQuestionDto, TestCategoryQuestion>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}