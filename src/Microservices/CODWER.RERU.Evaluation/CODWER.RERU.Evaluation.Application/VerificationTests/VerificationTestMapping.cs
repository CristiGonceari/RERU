using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;

namespace CODWER.RERU.Evaluation.Application.VerificationTests
{
    public class VerificationTestMapping : Profile
    {
        public VerificationTestMapping()
        {
            CreateMap<TestQuestion, VerificationTestQuestionSummaryDto>()
                .ForMember(dto => dto.VerificationStatus, options => options.MapFrom(entity => entity.Verified))
                .ForMember(dto => dto.IsCorrect, options => options.MapFrom(entity => entity.IsCorrect));

            CreateMap<Option, VerificationTestOptionsDto>()
                .ForMember(dto => dto.OptionMediaFileId, options => options.MapFrom(entity => entity.MediaFileId));

            CreateMap<VerificationTestQuestionDto, TestQuestion>()
                .ForMember(x => x.IsCorrect, opts => opts.MapFrom(t => t.IsCorrect))
                .ForMember(x => x.Points, opts => opts.MapFrom(t => t.EvaluatorPoints))
                .ForMember(x => x.QuestionUnitId, opts => opts.MapFrom(t => t.QuestionUnitId));

            CreateMap<TestQuestion, VerificationTestQuestionUnitDto>()
                .ForMember(dto => dto.AnswersCount, options => options.MapFrom(entity => entity.QuestionUnit.Options.Count))
                .ForMember(dto => dto.CategoryName, options => options.MapFrom(entity => entity.QuestionUnit.QuestionCategory.Name))
                .ForMember(dto => dto.Question, options => options.MapFrom(entity => entity.QuestionUnit.Question))
                .ForMember(dto => dto.Options, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer ? null : entity.QuestionUnit.Options ))
                .ForMember(dto => dto.HashedOptions, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer ? entity.QuestionUnit.Options : null))
                .ForMember(dto => dto.QuestionType, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType))
                .ForMember(dto => dto.QuestionCategoryId, options => options.MapFrom(entity => entity.QuestionUnit.QuestionCategoryId))
                .ForMember(dto => dto.QuestionUnitId, options => options.MapFrom(entity => entity.QuestionUnit.Id))
                .ForMember(dto => dto.QuestionMaxPoints, options => options.MapFrom(entity => entity.QuestionUnit.QuestionPoints))
                .ForMember(dto => dto.EvaluatorPoints, options => options.MapFrom(entity => entity.Points))
                .ForMember(dto => dto.QuestionUnitMediaFileId, options => options.MapFrom(entity => entity.QuestionUnit.MediaFileId));
        }
    }
}
