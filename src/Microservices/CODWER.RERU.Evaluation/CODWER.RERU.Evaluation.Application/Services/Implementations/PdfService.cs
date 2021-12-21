using CODWER.RERU.Evaluation.Application.TestCategoryQuestions;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class PdfService : IPdfService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly IMediator _mediator;
        private readonly IQuestionUnitService _questionUnitService;

        public PdfService(AppDbContext appDbContext,
            IGeneratePdf generatePdf, 
            IMediator mediator, 
            IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _mediator = mediator;
            _questionUnitService = questionUnitService;
        }

        public async Task<FileDataDto> PrintTestTemplatePdf(int testTemplateId)
        {
            var testTemplate = await _appDbContext.TestTypes
                .Include(x => x.TestTypeQuestionCategories)
                    .ThenInclude(x => x.TestCategoryQuestions)
                .Include(x => x.TestTypeQuestionCategories)
                    .ThenInclude(x => x.QuestionCategory)
                        .ThenInclude(x => x.QuestionUnits)
                .Include(x => x.Settings)
                .FirstOrDefaultAsync(x => x.Id == testTemplateId);

            return await GetPdf(testTemplate);
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
        public async Task<FileDataDto> PrintQuestionUnitPdf(int questionId)
        {
            var questions = _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(op => op.Options)
                .FirstOrDefault(x => x.Id == questionId);

            return await GetPdf(questions);
        }
        public async Task<FileDataDto> PrintPerformingTestPdf(int testId)
        {
            var item = _appDbContext.Tests
                .Include(t => t.UserProfile)
                .Include(t => t.Evaluator)
                .Include(t => t.TestType)
                .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.QuestionUnit)
                        .ThenInclude(q => q.Options)
                .FirstOrDefault(t => t.Id == testId);

            return await GetPdf(item, "PdfTemplates/PerformingTest.html");
        }

        #region GetPdf

        private async Task<FileDataDto> GetPdf(TestType testTemplate)
        {
            var path = new FileInfo("PdfTemplates/TestTemplate.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(testTemplate);

            source = ReplaceKeys(source, myDictionary);

            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Sablon_De_Test.pdf"
            };
        }
        private async Task<FileDataDto> GetPdf(Test item)
        {
            var path = new FileInfo("PdfTemplates/Test.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(item);

            source = ReplaceKeys(source, myDictionary);

            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Test.pdf"
            };
        }
        private async Task<FileDataDto> GetPdf(QuestionUnit items)
        {
            var path = new FileInfo("PdfTemplates/one_multiple_question.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(items);

            source = ReplaceKeys(source, myDictionary);

            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Intrebarea.pdf"
            };
        }
        private async Task<FileDataDto> GetPdf(Test item, string name)
        {
            var path = new FileInfo(name).FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(item, name);

            source = ReplaceKeys(source, myDictionary);

            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "PerformingTest.pdf"
            };
        }
        #endregion

        #region GetDictionary

        private async Task<Dictionary<string, string>> GetDictionary(TestType testTemplate)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{test_name}", testTemplate.Name);
            myDictionary.Add("{nr_test_question}", testTemplate.QuestionCount.ToString());
            myDictionary.Add("{test_time}", testTemplate.Duration.ToString());
            myDictionary.Add("{min_percentage}", testTemplate.MinPercent.ToString());
            myDictionary.Add("{test_mode}", testTemplate.Mode.ToString());
            myDictionary.Add("{settings_replace}", GetParsedSettingsForTestTemplate(testTemplate));
            myDictionary.Add("{rules_name}", DecodeRules(testTemplate.Rules));
            myDictionary.Add("{category_replace}", await GetTableContent(testTemplate.TestTypeQuestionCategories.ToList()));

            return myDictionary;
        }
        private async Task<Dictionary<string, string>> GetDictionary(Test item)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{test_name}", item.TestType.Name);
            myDictionary.Add("{nr_test_question}", item.TestType.QuestionCount.ToString());
            myDictionary.Add("{test_time}", item.ProgrammedTime.ToString("dd/MM/yyyy, HH:mm"));
            myDictionary.Add("{min_percentage}", item.TestType.MinPercent.ToString());
            myDictionary.Add("{event_name}", item.EventId != null ? item.Event.Name : "-");
            myDictionary.Add("{location_name}", item.LocationId != null ? item.Location.Name : "-");
            myDictionary.Add("{evaluat_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName);
            myDictionary.Add("{evaluator_name}", GetEvaluatorName(item));
            myDictionary.Add("{tr_area_replace}", await GetTableContent(item));

            return myDictionary;
        }
        private async Task<Dictionary<string, string>> GetDictionary(QuestionUnit items)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{question_name}", await GetQuestionName(items.Id));
            myDictionary.Add("{category_name}", items.QuestionCategory.Name);
            myDictionary.Add("{question_type}", EnumMessages.EnumMessages.GetQuestionType(items.QuestionType));
            myDictionary.Add("{question_points}", items.QuestionPoints.ToString());
            myDictionary.Add("{question_status}", EnumMessages.EnumMessages.GetQuestionStatus(items.Status));
            myDictionary.Add("{answer_option}", GetTableContent(items));

            return myDictionary;
        }
        private async Task<Dictionary<string, string>> GetDictionary(Test item, string name)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{candidate_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName);
            myDictionary.Add("{min_percent}", item.TestType.MinPercent.ToString());
            myDictionary.Add("{test_name}", item.TestType.Name);
            myDictionary.Add("{test_type_duration}", item.TestType.Duration.ToString());
            myDictionary.Add("{tr_area_replace}", await GetTestQuestionContent(item));
            myDictionary.Add("{evaluator_name}", GetEvaluatorName(item));
            myDictionary.Add("{test_date}", item.ProgrammedTime.ToString("dd-MM-yyyy"));

            return myDictionary;
        }

        #endregion

        #region GetTableContent

        private async Task<string> GetTableContent(List<TestTypeQuestionCategory> testTypeQuestionCategories)
        {
            var content = string.Empty;

            foreach (var item in testTypeQuestionCategories)
            {
                content += $@"
            <tr>
            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left;
            padding-left: 5px; background-color: #1f3864; color: white; height: 30px;"">Denumirea categoriei</th>
            </tr>
             <tr>
           <tr>
            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse;
            padding-left: 5px; font-size: 15px; height: 30px;"">{item.QuestionCategory.Name}</th>
            </tr>       
             <tr>
            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; background-color: #1f3864; color: white; height: 30px;"">Număr de întrebări din categorie</th>
            </tr>
            <tr>
                    <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;"">
                          <b>{item.QuestionCount}</b> {ParseQuestion(item.QuestionCount)} din <b>{item.QuestionCategory.QuestionUnits.Count}</b>, ordinea intrebarilor - {EnumMessages.EnumMessages.GetQuestionSequence(item.SequenceType)}
                    </th>
            </tr>
            <tr>
            <th colspan=""3"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; background-color: #1f3864; color: white; height: 30px; max-width: 500px"">Lista de Intrebari</th>
            <th colspan=""1"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; background-color: #1f3864; color: white; height: 30px;"">Tipul</th>
            </tr>
            "
            ;

                var result = await GetQuestionsForCategoryContent(item.Id);

                content += BuildHtmlContentForQuestions(result);

                content += AddBrakeLineAfterCategory();
            }

            return content;
        }
        private async Task<string> GetTableContent(Test item)
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
        private string GetTableContent(QuestionUnit questionOption)
        {
            var content = string.Empty;
            if (questionOption.QuestionType == QuestionTypeEnum.MultipleAnswers || questionOption.QuestionType == QuestionTypeEnum.OneAnswer)
            {
                var options = questionOption.Options.ToList();

                if (options != null)
                {
                    content += $@"<tr>
                    <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; background-color: #1f3864; color: white; height: 30px;'>Variante de răspuns</th>
                    <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; background-color: #1f3864; color: white; height: 30px;'>Răspuns </th>
                    </tr>";
                    foreach (var option in options)
                    {
                        if (option.IsCorrect)
                        { content += $@"<tr>
                            <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>{option.Answer}</th>
                            <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>Corect</th>
                            </tr>"; }
                        else
                        {
                            content += $@"<tr>
                            <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>{option.Answer}</th>
                            <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>Incorect</th>
                            </tr>";
                        }
                    }
                }
            }
            return content;
        }
        private async Task<string> GetTestQuestionContent(Test item)
        {
            var content = string.Empty;

            foreach (var testQuestion in item.TestQuestions.Select((value, i) => new { i, value })) 
            {
                if (testQuestion.value.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
                {
                    content = await GetQuestionTemplateByType(testQuestion.value.QuestionUnit, content);
                }

                content += $@"<div style=""margin-bottom: 20px; width: 930px;""><b>{testQuestion.i+1}. {testQuestion.value.QuestionUnit.Question}</b> ({testQuestion.value.QuestionUnit.QuestionPoints}p)</div>";

                content = await GetQuestionTemplateByType(testQuestion.value.QuestionUnit, content);
            }

            return content;
        }

        #endregion

        private string ReplaceKeys(string source, Dictionary<string, string> myDictionary)
        {
            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }

            return source;
        }
        private byte[] Parse(string html)
        {
            try
            {
                return _generatePdf.GetPDF(html);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string BuildHtmlContentForQuestions(TestCategoryQuestionContentDto testTypeQuestionCategory)
        {
            return testTypeQuestionCategory.Questions.Aggregate(string.Empty, (current, questionUnit)
                =>
                current + $@"
            <tr>
                <th colspan=""3"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px; max-width: 500px"">{questionUnit.Question}</th>
                <th colspan=""1"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;"">{EnumMessages.EnumMessages.GetQuestionType(questionUnit.QuestionType)}</th>
            </tr>");
        }
        private string DecodeRules(string rules)
        {
            if (string.IsNullOrEmpty(rules)) return "-";

            var base64EncodedBytes = Convert.FromBase64String(rules);
            var testTemplateRules = Encoding.UTF8.GetString(base64EncodedBytes);

            return testTemplateRules;
        }
        private string GetParsedSettingsForTestTemplate(TestType testTemplate)
        {
            var content = string.Empty;

            if (testTemplate.Settings.HidePagination)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Ascundeți paginarea</th>
                    </tr>";
            }

            if (testTemplate.Settings.CanViewResultWithoutVerification)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Utilizatorul poate vizualiza rezultatul testului</th>
                    </tr>";
            }

            if (testTemplate.Settings.ShowManyQuestionPerPage)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Afișați mai multe întrebări pe pagină</th>
                    </tr>";
            }

            if (testTemplate.Settings.StartWithoutConfirmation)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Este posibil să porniți testul fără confirmare</th>
                    </tr>";
            }

            if (testTemplate.Settings.StartBeforeProgrammation)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Este posibil să porniți testul înainte de ora programată</th>
                    </tr>";
            }

            if (testTemplate.Settings.StartAfterProgrammation)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Este posibil să porniți testul după timpul programat</th>
                    </tr>";
            }

            if (testTemplate.Settings.PossibleChangeAnswer)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Posibil de schimbat răspunsul</th>
                    </tr>";
            }

            if (testTemplate.Settings.PossibleGetToSkipped)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Este posibil să reveniți la răspunsul omis</th>
                    </tr>";
            }

            if (testTemplate.Settings.MaxErrors.HasValue)
            {
                content += $@"
                    <tr>
			            <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; font-size: 15px; height: 30px;"">Numarul maxim de erori sunt: {testTemplate.Settings.MaxErrors}</th>
                    </tr>";
            }

            return content;
        }
        private string AddBrakeLineAfterCategory()
        {
            var content = string.Empty;

            content += $@"     
                <tr>
                    <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; background-color: rgba(223, 221, 221, 0.842); height: 30px;""></th>
               </tr> ";

            return content;
        }
        private string ParseQuestion(int? questionCount)
        {
            return questionCount > 1 ? "intrebari" : "intrebare";
        }
        private async Task<TestCategoryQuestionContentDto> GetQuestionsForCategoryContent(int testTypeQuestionCategoryId)
        {
            var command = new TestCategoryQuestionsQuery
            {
                TestTypeQuestionCategoryId = testTypeQuestionCategoryId
            };

            return await _mediator.Send(command);
        }
        private string GetEvaluatorName(Test item)
        {
            var evaluators = new List<EventEvaluator>();
            var list = new List<string>();

            if (item.Event != null)
            {
                evaluators = _appDbContext.EventEvaluators
                    .Include(e => e.Evaluator)
                    .Where(e => e.EventId == item.EventId)
                    .ToList();

                list.AddRange(evaluators.Select(evaluator => evaluator.Evaluator.FirstName + " " + evaluator.Evaluator.LastName));
                var combineString = string.Join(", ", list);

                return combineString;
            }

            if (item.EvaluatorId != null)
            {
                return item.Evaluator.FirstName + " " + item.Evaluator.LastName;
            }

            return "-";
        }
        private async Task<string> GetQuestionName(int id)
        {
            var questionUnit = _appDbContext.QuestionUnits.FirstOrDefault(x => x.Id == id);

            if (questionUnit is { QuestionType: QuestionTypeEnum.HashedAnswer })
            {
                questionUnit = await _questionUnitService.GetUnHashedQuestionUnit(questionUnit.Id);
                questionUnit.Options = null;
            }

            return questionUnit.Question;
        }
        private async Task<string> GetQuestionTemplateByType(QuestionUnit question, string content)
        {
            if (question.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                question = await _questionUnitService.GetUnHashedQuestionUnit(question.Id);

                if (question.Options != null)
                {
                    foreach (var option in question.Options)
                    {
                        question.Question = question.Question.Replace($"[answer]{option.Answer}[/answer]", "________________");
                    }
                }
            }

            if (question.QuestionType == QuestionTypeEnum.FreeText)
            {
                content += $@"<div style=""margin-bottom: 20px; width: 930px;"">
                                                   ______________________________________________________________________________________________________
                                                   ______________________________________________________________________________________________________
                                                   ______________________________________________________________________________________________________
                                                   ______________________________________________________________________________________________________
                                                   ______________________________________________________________________________________________________
                                                   ______________________________________________________________________________________________________
                             </div>";
            }

            if (question.QuestionType == QuestionTypeEnum.MultipleAnswers)
            {
                foreach (var option in question.Options.Select((value, i) => new { i, value}))
                {
                    content += $@"<div>
                                    <label>
                                        <input type=""checkbox"" style=""margin-left: 20px; margin-bottom: 15px;"">
                                        <span style=""height: 25px; width: 25px;"">{option.value.Answer}</span>
                                    </label>
                                 </div>";
                }

                content += $@"<div style=""margin-bottom: 20px;""></div>";
            }

            if (question.QuestionType == QuestionTypeEnum.OneAnswer)
            {
                foreach (var option in question.Options.Select((value, i) => new { i, value }))
                {
                    content += $@"<div>
                                                    <label>
                                                        <input type=""radio"" style=""margin-left: 20px; margin-bottom: 15px;"">
                                                        <span style=""height: 25px; width: 25px;"">{option.value.Answer}</span>
                                                    </label>
                                                 </div>";
                }

                content += $@"<div style=""margin-bottom: 20px;""></div>";
            }

            return content;
        }
    }
}
