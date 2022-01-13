using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits
{
    public class QuestionUnitMapping : Profile
    {
        public QuestionUnitMapping()
        {
            CreateMap<QuestionUnit, QuestionUnitDto>()
                .ForMember(dto => dto.AnswersCount, options => options.MapFrom(entity => entity.Options.Count))
                .ForMember(dto => dto.UsedInTestsCount, options => options.MapFrom(entity => entity.TestQuestions.Count))
                .ForMember(dto => dto.CategoryName, options => options.MapFrom(entity => entity.QuestionCategory.Name))
                .ForMember(dto => dto.Tags, options => options.MapFrom(entity => entity.QuestionUnitTags.Select(x => x.Tag.Name)))
                .ForMember(dto => dto.CategoryId, options => options.MapFrom(entity => entity.QuestionCategory.Id))
                .ForMember(dto => dto.OptionsCount, options => options.MapFrom(entity => entity.Options.Count()))
                .ForMember(dto => dto.MediaFileId, options => options.MapFrom(entity => entity.MediaFileId))
                //.ForMember(dto => dto.IsReadyToActivate, options => options.MapFrom(entity => 
                //    entity.Options.Count > 1 && entity.Options.Any(op => op.IsCorrect) || entity.QuestionType == QuestionTypeEnum.FreeText || entity.QuestionType == QuestionTypeEnum.HashedAnswer))
                ;

            CreateMap<QuestionUnit, ActiveQuestionUnitValueDto>()
                .ForMember(dto => dto.CategoryId, options => options.MapFrom(entity => entity.QuestionCategory.Id));

            CreateMap<QuestionUnit, QuestionUnitPreviewDto>()
                .ForMember(dto => dto.CategoryName, options => options.MapFrom(entity => entity.QuestionCategory.Name));

            CreateMap<AddEditQuestionUnitDto, QuestionUnit>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<EditQuestionUnitCommand, QuestionUnit>();

            CreateMap<QuestionUnit, EditQuestionUnitDto>();

            CreateMap<QuestionUnit, EditQuestionStatusDto>()
                .ForMember(dto => dto.QuestionId, options => options.MapFrom(entity => entity.Id));

            CreateMap<Tag, TagDto>();
        }
    }
}
