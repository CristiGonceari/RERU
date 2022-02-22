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
            var oldtestTemplate = await _appDbContext.TestTemplates
                .Include(x => x.Settings)
                .Include(x => x.TestTemplateQuestionCategories)
                .Include(x => x.EventTestTemplates)
                .FirstAsync(x => x.Id == request.TestTemplateId);

            var newtestTemplate = new TestTemplate()
            {
                Name = oldtestTemplate.Name,
                Rules = oldtestTemplate.Rules,
                QuestionCount = oldtestTemplate.QuestionCount,
                MinPercent = oldtestTemplate.MinPercent,
                Duration = oldtestTemplate.Duration,
                Status = TestTemplateStatusEnum.Draft,
                Mode = oldtestTemplate.Mode,
                CategoriesSequence = oldtestTemplate.CategoriesSequence
            };

            await _appDbContext.TestTemplates.AddAsync(newtestTemplate);
            await _appDbContext.SaveChangesAsync();

            var settingsToAdd = oldtestTemplate.Settings == null
                ? new TestTemplateSettings() { TestTemplateId = newtestTemplate.Id }
                : new TestTemplateSettings()
                {
                    TestTemplateId = newtestTemplate.Id,
                    StartWithoutConfirmation = oldtestTemplate.Settings.StartWithoutConfirmation,
                    StartBeforeProgrammation = oldtestTemplate.Settings.StartBeforeProgrammation,
                    StartAfterProgrammation = oldtestTemplate.Settings.StartAfterProgrammation,
                    PossibleGetToSkipped = oldtestTemplate.Settings.PossibleGetToSkipped,
                    PossibleChangeAnswer = oldtestTemplate.Settings.PossibleChangeAnswer,
                    CanViewResultWithoutVerification = oldtestTemplate.Settings.CanViewResultWithoutVerification,
                    CanViewPollProgress = oldtestTemplate.Settings.CanViewPollProgress,
                    HidePagination = oldtestTemplate.Settings.HidePagination,
                    ShowManyQuestionPerPage = oldtestTemplate.Settings.ShowManyQuestionPerPage,
                    QuestionsCountPerPage = oldtestTemplate.Settings.QuestionsCountPerPage,
                    MaxErrors = oldtestTemplate.Settings.MaxErrors
                };

            await _appDbContext.TestTemplateSettings.AddAsync(settingsToAdd);
            await _appDbContext.SaveChangesAsync();

            if (oldtestTemplate.EventTestTemplates?.Count > 0)
            {
                var eventsToAdd = new List<EventTestTemplate>();
                foreach (var Event in oldtestTemplate.EventTestTemplates)
                {
                    eventsToAdd.Add(new EventTestTemplate()
                    {
                        TestTemplateId = newtestTemplate.Id,
                        EventId = Event.Id
                    });
                }

                await _appDbContext.EventTestTemplates.AddRangeAsync(eventsToAdd);
                await _appDbContext.SaveChangesAsync();
            }

            if (oldtestTemplate.TestTemplateQuestionCategories?.Count > 0)
            {
                var questionCategoriesToAdd = new List<TestTemplateQuestionCategory>();
                foreach (var questionCategory in oldtestTemplate.TestTemplateQuestionCategories)
                {
                    questionCategoriesToAdd.Add(new TestTemplateQuestionCategory()
                    {
                        TestTemplateId = newtestTemplate.Id,
                        QuestionCategoryId = questionCategory.QuestionCategoryId,
                        CategoryIndex = questionCategory.CategoryIndex,
                        QuestionType = questionCategory.QuestionType,
                        QuestionCount = questionCategory.QuestionCount,
                        TimeLimit = questionCategory.TimeLimit,
                        SelectionType = questionCategory.SelectionType,
                        SequenceType = questionCategory.SequenceType
                    });
                }

                await _appDbContext.TestTemplateQuestionCategories.AddRangeAsync(questionCategoriesToAdd);
                await _appDbContext.SaveChangesAsync();
            }

            return newtestTemplate.Id;
        }
    }
}
