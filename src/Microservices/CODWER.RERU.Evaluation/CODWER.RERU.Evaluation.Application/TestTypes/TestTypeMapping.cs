using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;

namespace CODWER.RERU.Evaluation.Application.TestTypes
{
    public class TestTypeMapping : Profile
    {
        public TestTypeMapping()
        {
            CreateMap<TestType, TestTypeDto>()
                .ForMember(x => x.CategoriesCount, opts => opts.MapFrom(tt => tt.TestTypeQuestionCategories.Select(x => x.QuestionCategory).Distinct().Count()))
                .ForMember(x => x.Status, opts => opts.MapFrom(tt => tt.Status));

            CreateMap<AddEditTestTypeDto, TestType>()
                .ForMember(x => x.Status, opts => opts.MapFrom(tt => (int)TestTypeStatusEnum.Draft))
                .ForMember(x => x.CategoriesSequence, opts => opts.MapFrom(tt => (int)SequenceEnum.Random));

            CreateMap<TestTypeSettingsDto, TestTypeSettings>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<TestTypeSettings, TestTypeSettingsDto>();

            CreateMap<TestType, SelectItem>()
               .ForMember(x => x.Value, opts => opts.MapFrom(tt => tt.Id))
               .ForMember(x => x.Label, opts => opts.MapFrom(tt => tt.Name));

            CreateMap<TestType, SelectTestTypeValueDto>()
               .ForMember(x => x.TestTypeId, opts => opts.MapFrom(tt => tt.Id))
               .ForMember(x => x.TestTypeName, opts => opts.MapFrom(tt => tt.Name));

            CreateMap<TestType, RulesDto>()
                .ForMember(x => x.TestTypeId, opts => opts.MapFrom(tt => tt.Id));
        }
    }
}