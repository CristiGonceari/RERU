using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.CloneTestTemplate
{
    public class CloneTestTemplateCommandHandler : IRequestHandler<CloneTestTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;

        public CloneTestTemplateCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CloneTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var oldTestType = await _appDbContext.TestTemplates
                .Include(x => x.Settings)
                .Include(x => x.TestTypeQuestionCategories)
                .Include(x => x.EventTestTypes)
                .FirstAsync(x => x.Id == request.TestTypeId);

            var newTestType = new TestTemplate()
            {
                Name = oldTestType.Name,
                Rules = oldTestType.Rules,
                QuestionCount = oldTestType.QuestionCount,
                MinPercent = oldTestType.MinPercent,
                Duration = oldTestType.Duration,
                Status = TestTypeStatusEnum.Draft,
                Mode = oldTestType.Mode,
                CategoriesSequence = oldTestType.CategoriesSequence
            };

            await _appDbContext.TestTemplates.AddAsync(newTestType);
            await _appDbContext.SaveChangesAsync();

            var settingsToAdd = oldTestType.Settings == null
                ? new TestTypeSettings() { TestTemplateId = newTestType.Id }
                : new TestTypeSettings()
                {
                    TestTemplateId = newTestType.Id,
                    StartWithoutConfirmation = oldTestType.Settings.StartWithoutConfirmation,
                    StartBeforeProgrammation = oldTestType.Settings.StartBeforeProgrammation,
                    StartAfterProgrammation = oldTestType.Settings.StartAfterProgrammation,
                    PossibleGetToSkipped = oldTestType.Settings.PossibleGetToSkipped,
                    PossibleChangeAnswer = oldTestType.Settings.PossibleChangeAnswer,
                    CanViewResultWithoutVerification = oldTestType.Settings.CanViewResultWithoutVerification,
                    CanViewPollProgress = oldTestType.Settings.CanViewPollProgress,
                    HidePagination = oldTestType.Settings.HidePagination,
                    ShowManyQuestionPerPage = oldTestType.Settings.ShowManyQuestionPerPage,
                    QuestionsCountPerPage = oldTestType.Settings.QuestionsCountPerPage,
                    MaxErrors = oldTestType.Settings.MaxErrors
                };

            await _appDbContext.TestTypeSettings.AddAsync(settingsToAdd);
            await _appDbContext.SaveChangesAsync();

            if (oldTestType.EventTestTypes?.Count > 0)
            {
                var eventsToAdd = new List<EventTestType>();
                foreach (var Event in oldTestType.EventTestTypes)
                {
                    eventsToAdd.Add(new EventTestType()
                    {
                        TestTemplateId = newTestType.Id,
                        EventId = Event.Id
                    });
                }

                await _appDbContext.EventTestTypes.AddRangeAsync(eventsToAdd);
                await _appDbContext.SaveChangesAsync();
            }

            if (oldTestType.TestTypeQuestionCategories?.Count > 0)
            {
                var questionCategoriesToAdd = new List<TestTypeQuestionCategory>();
                foreach (var questionCategory in oldTestType.TestTypeQuestionCategories)
                {
                    questionCategoriesToAdd.Add(new TestTypeQuestionCategory()
                    {
                        TestTemplateId = newTestType.Id,
                        QuestionCategoryId = questionCategory.QuestionCategoryId,
                        CategoryIndex = questionCategory.CategoryIndex,
                        QuestionType = questionCategory.QuestionType,
                        QuestionCount = questionCategory.QuestionCount,
                        TimeLimit = questionCategory.TimeLimit,
                        SelectionType = questionCategory.SelectionType,
                        SequenceType = questionCategory.SequenceType
                    });
                }

                await _appDbContext.TestTypeQuestionCategories.AddRangeAsync(questionCategoriesToAdd);
                await _appDbContext.SaveChangesAsync();
            }

            return newTestType.Id;
        }
    }
}
