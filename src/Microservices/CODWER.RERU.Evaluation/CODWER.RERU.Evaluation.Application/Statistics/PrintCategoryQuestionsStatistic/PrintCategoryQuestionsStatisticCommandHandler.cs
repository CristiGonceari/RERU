using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Statistics.PrintCategoryQuestionsStatistic
{
    public class PrintCategoryQuestionsStatisticCommandHandler : IRequestHandler<PrintCategoryQuestionsStatisticCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<TestQuestionStatisticDto, TestQuestionStatisticDto> _printer;


        public PrintCategoryQuestionsStatisticCommandHandler(AppDbContext appDbContext, ITablePrinter<TestQuestionStatisticDto, TestQuestionStatisticDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintCategoryQuestionsStatisticCommand request, CancellationToken cancellationToken)
        {
            var questionInCategory = await _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Where(x => x.QuestionCategoryId == request.CategoryId)
                .ToListAsync();

            var questionsInTests = await _appDbContext.TestQuestions
                .Include(x => x.QuestionUnit)
                .Where(x => questionInCategory.Select(s => s.Id).Contains(x.QuestionUnitId))
                .ToListAsync();

            var answer = new List<TestQuestionStatisticDto>();

            foreach (var questionToAnalize in questionInCategory)
            {
                if (!questionsInTests.Any(x => x.QuestionUnitId == questionToAnalize.Id))
                {
                    answer.Add(new TestQuestionStatisticDto()
                    {
                        QuestionId = questionToAnalize.Id,
                        TotalUsed = 0,
                        CountByFilter = 0,
                        Percent = 0,
                        CountCorrect = 0,
                        PercentCorrect = 0,
                        CountNotCorrect = 0,
                        PercentNotCorrect = 0,
                        CountSkipped = 0,
                        PercentSkipped = 0,
                        Question = questionToAnalize.Question,
                        CategoryName = questionToAnalize.QuestionCategory.Name
                    });
                }
                else
                {
                    var questionsArray = questionsInTests.Where(x => x.QuestionUnitId == questionToAnalize.Id).ToList();
                    var total = questionsArray.Count();
                    var correct = questionsArray.Where(x => x.IsCorrect == true).Count();
                    var notCorrect = questionsArray.Where(x => x.IsCorrect == false && x.AnswerStatus == AnswerStatusEnum.Answered).Count();
                    var skipped = questionsArray.Where(x => x.AnswerStatus == AnswerStatusEnum.Skipped).Count();

                    answer.Add(new TestQuestionStatisticDto()
                    {
                        QuestionId = questionToAnalize.Id,
                        TotalUsed = total,
                        CountCorrect = correct,
                        PercentCorrect = 100 * correct / total,
                        CountNotCorrect = notCorrect,
                        PercentNotCorrect = 100 * notCorrect / total,
                        CountSkipped = skipped,
                        PercentSkipped = 100 * skipped / total,
                        Question = questionToAnalize.Question,
                        CategoryName = questionToAnalize.QuestionCategory.Name
                    });
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.Correct)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.CountCorrect;
                        ans.Percent = ans.PercentCorrect;
                    }
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.NotCorrect)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.CountNotCorrect;
                        ans.Percent = ans.PercentNotCorrect;
                    }
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.Skipped)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.CountSkipped;
                        ans.Percent = ans.PercentSkipped;
                    }
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.Select)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.TotalUsed;
                        ans.Percent = ans.Percent;
                    }
                }
            }

            var statisctica = answer.OrderByDescending(x => x.CountByFilter).Take(request.ItemsPerPage).AsQueryable();

            var result = _printer.PrintTable(new TableData<TestQuestionStatisticDto>
            {
                Name = request.TableName,
                Items = statisctica,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
