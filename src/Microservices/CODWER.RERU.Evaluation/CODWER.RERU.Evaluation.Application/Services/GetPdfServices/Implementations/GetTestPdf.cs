using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices.Implementations
{
    public class GetTestPdf : IGetTestPdf
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly IMediator _mediator;

        public GetTestPdf(IGeneratePdf generatePdf, AppDbContext appDbContext, IMediator mediator)
        {
            _generatePdf = generatePdf;
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task<FileDataDto> PrintTestPdf(int testId)
        {
            var item = _appDbContext.Tests
                .Include(t => t.TestType)
                    .ThenInclude(tt => tt.TestTypeQuestionCategories)
                        .ThenInclude(tc => tc.QuestionCategory)
                            .ThenInclude(c => c.QuestionUnits)
                .Include(t => t.UserProfile)
                .Include(t => t.Evaluator)
                .Include(t => t.Event)
                    .ThenInclude(e => e.EventEvaluators)
                .Include(t => t.Location)
                .FirstOrDefault(t => t.Id == testId);

            return await GetPdf(item);
        }

        private async Task<string> GetTableContentAsync(Test item)
        {
            var content = string.Empty;

            foreach (var testCategory in item.TestType.TestTypeQuestionCategories)
            {
                content += $@"<tr>
                                    <th colspan=""2"" style=""border: 1px solid black; border-collapse: collapse; text-align: left;
                                    padding-left: 5px; background-color: #1f3864; color: white; height: 30px;"">Denumirea categoriei</th>
                                </tr>
                                <tr>
                                <tr>
                                    <th colspan=""2"" style=""border: 1px solid black; border-collapse: collapse;
                                    padding-left: 5px; font-size: 15px; height: 30px;"">{testCategory.QuestionCategory.Name}</th>
                                </tr>
                                <tr>
                                    <th colspan=""2"" style=""border: 1px solid black; border-collapse: collapse; background-color: #1f3864; color: white; height: 30px;"">Numărul de întrebări din categorie</th>
                                </tr>
                                <tr>
                                    <th colspan=""2"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;""><b>{testCategory.QuestionCount}</b> din <b>{testCategory.QuestionCategory.QuestionUnits.Count}</b>, oridnea - {EnumMessages.EnumMessages.GetQuestionSequence(testCategory.SequenceType)}</th>
                                </tr>
                                <tr>
                                    <th style=""border: 1px solid black; border-collapse: collapse; text-align: left; background-color: #1f3864; color: white; height: 30px;"">Lista de întrebări</th>
                                    <th style=""border: 1px solid black; border-collapse: collapse; text-align: left; background-color: #1f3864; color: white; height: 30px;"">Tipul întrebării</th>
                                </tr>";

                var testCategoryQuestionData = await _mediator.Send(new TestCategoryQuestionsQuery { TestTypeQuestionCategoryId = testCategory.Id });
                
                foreach (var question in testCategoryQuestionData.Questions)
                {
                    content += $@"<tr>
                                <th style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px; max-width: 500px;"">{question.Question}</th>
                                <th style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;"">{EnumMessages.EnumMessages.GetQuestionType(question.QuestionType)}</th>
                            </tr> ";
                }

                if (item.TestType.TestTypeQuestionCategories.Count() >= 2)
                {
                    content += $@"<tr>
                                <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; background-color: rgba(223, 221, 221, 0.842); height: 35px;""></th>
                             </tr>";
                }
            }
            
            return content;
        }

        public async Task<FileDataDto> GetPdf(Test item)
        {
            byte[] res;
            var path = new FileInfo("PdfTemplates/Test.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            source = source.Replace("{test_name}", item.TestType.Name);
            source = source.Replace("{nr_test_question}", item.TestType.QuestionCount.ToString());
            source = source.Replace("{test_time}", item.ProgrammedTime.ToString("dd/MM/yyyy, HH:mm"));
            source = source.Replace("{min_percentage}", item.TestType.MinPercent.ToString());
            source = source.Replace("{event_name}", item.EventId != null ? item.Event.Name : "-");
            source = source.Replace("{location_name}", item.LocationId != null ? item.Location.Name : "-");
            source = source.Replace("{evaluat_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName);
            source = source.Replace("{evaluator_name}", getEvaluatorName(item));
            source = source.Replace("{tr_area_replace}", await GetTableContentAsync(item));

            try
            {
                res = _generatePdf.GetPDF(source);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Test.pdf"
            };
        }

        public string getEvaluatorName(Test item)
        {
            var evaluators = new List<EventEvaluator>();
            var list = new List<string>();

            if (item.Event != null)
            {
                evaluators = _appDbContext.EventEvaluators
                    .Include(e => e.Evaluator)
                    .Where(e => e.EventId == item.EventId)
                    .ToList();

                foreach (var evaluator in evaluators)
                {
                    list.Add(evaluator.Evaluator.FirstName + " " + evaluator.Evaluator.LastName);

                }
                string combineString = string.Join(", ", list);

                return combineString;
            }
            else if(item.EvaluatorId != null)
            {
                return item.Evaluator.FirstName + " " + item.Evaluator.LastName;
            }

            return "-";
        }
    }
}
