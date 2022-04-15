using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestTemplates
{
    public class TestTemplateMapping : Profile
    {
        public TestTemplateMapping()
        {
            CreateMap<TestTemplate, TestTemplateDto>()
                .ForMember(x => x.CategoriesCount, opts => opts.MapFrom(tt => tt.TestTemplateQuestionCategories.Select(x => x.QuestionCategory).Distinct().Count()))
                .ForMember(x => x.Status, opts => opts.MapFrom(tt => tt.Status));

            CreateMap<AddEditTestTemplateDto, TestTemplate>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Status, opts => opts.MapFrom(tt => (int)TestTemplateStatusEnum.Draft))
                .ForMember(x => x.CategoriesSequence, opts => opts.MapFrom(tt => (int)SequenceEnum.Random));

            CreateMap<TestTemplateSettingsDto, TestTemplateSettings>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<TestTemplateSettings, TestTemplateSettingsDto>();

            CreateMap<TestTemplate, SelectItem>()
               .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.Id))
               .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.Name));

            CreateMap<TestTemplate, SelectTestTemplateValueDto>()
               .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(tt => tt.Id))
               .ForMember(x => x.TestTemplateName, opts => opts.MapFrom(tt => tt.Name));

            CreateMap<TestTemplate, RulesDto>()
                .ForMember(x => x.TestTemplateId, opts => opts.MapFrom(tt => tt.Id));
        }
    }
}