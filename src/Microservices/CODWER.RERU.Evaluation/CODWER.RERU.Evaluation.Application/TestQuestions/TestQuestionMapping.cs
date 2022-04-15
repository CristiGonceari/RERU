using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestQuestions
{
    class TestQuestionMapping : Profile
    {
        public TestQuestionMapping()
        {
            CreateMap<TestQuestion, TestQuestionSummaryDto>()
                .ForMember(dto => dto.IsClosed, options => options.MapFrom(entity => IsTestQuestionClosed(entity)))
                .ForMember(dto => dto.QuestionType, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType));

            CreateMap<TestQuestion, TestQuestionDto>()
                .ForMember(dto => dto.Question, options => options.MapFrom(entity => entity.QuestionUnit.Question))
                .ForMember(dto => dto.QuestionUnitId, options => options.MapFrom(entity => entity.QuestionUnit.Id))
                .ForMember(dto => dto.QuestionCategoryId, options => options.MapFrom(entity => entity.QuestionUnit.QuestionCategory.Id))
                .ForMember(dto => dto.QuestionType, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType))
                .ForMember(dto => dto.CategoryName, options => options.MapFrom(entity => entity.QuestionUnit.QuestionCategory.Name))
                .ForMember(dto => dto.AnswersCount, options => options.MapFrom(entity => entity.QuestionUnit.QuestionCategory.Name))
                .ForMember(dto => dto.HashedOptions, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer ? entity.QuestionUnit.Options : null))
                .ForMember(dto => dto.Options, options => options.MapFrom(entity => entity.QuestionUnit.Options))
                .ForMember(dto => dto.AnswerStatus, options => options.MapFrom(entity => (AnswerStatusEnum)entity.AnswerStatus))
                .ForMember(dto => dto.MediaFileId, options => options.MapFrom(entity => entity.QuestionUnit.MediaFileId));

            CreateMap<TestQuestionDto, TestQuestion>();

            CreateMap<QuestionUnit, TestQuestionDto>()
                .ForMember(dto => dto.AnswersCount, options => options.MapFrom(entity => entity.Options.Count))
                .ForMember(dto => dto.CategoryName, options => options.MapFrom(entity => entity.QuestionCategory.Name))
                .ForMember(dto => dto.Options, options => options.MapFrom(entity => entity.QuestionType == QuestionTypeEnum.HashedAnswer ? null : entity.Options))
                .ForMember(dto => dto.HashedOptions, options => options.MapFrom(entity => entity.QuestionType == QuestionTypeEnum.HashedAnswer ? entity.Options : null));

            CreateMap<Option, TestOptionsDto>()
                .ForMember(dto => dto.Answer, options => options.MapFrom(entity => entity.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer ? null : entity.Answer));
        }

        private bool IsTestQuestionClosed(TestQuestion testQuestion)
        {
            var answer = false;

            var testSettings = testQuestion.Test.TestTemplate.Settings;

            if (!testSettings.PossibleChangeAnswer && testQuestion.AnswerStatus == AnswerStatusEnum.Answered)
            {
                answer = true;
            }

            if (!testSettings.PossibleGetToSkipped && testQuestion.AnswerStatus == AnswerStatusEnum.Skipped)
            {
                answer = true;
            }

            return answer;
        }
    }
}
