using CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly StorageDbContext _storageDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly IMediator _mediator;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IStorageFileService _storageFileService;

        public PdfService(AppDbContext appDbContext,
            IGeneratePdf generatePdf, 
            IMediator mediator, 
            IQuestionUnitService questionUnitService, 
            IStorageFileService storageFileService, 
            StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _mediator = mediator;
            _questionUnitService = questionUnitService;
            _storageFileService = storageFileService;
            _storageDbContext = storageDbContext;
        }

        public async Task<FileDataDto> PrintTestTemplatePdf(int testTemplateId)
        {
            var testTemplate = await _appDbContext.TestTemplates
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
                .Include(t => t.TestTemplates)
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

            var files = _storageDbContext.Files.FirstOrDefault(f => f.Id.ToString() == questions.MediaFileId && f.Type.Contains("image"));

            FileDataDto getQuestionFile = null;

            if (files != null)
            {
                getQuestionFile = await _storageFileService.GetFile(files.Id.ToString());
            }

            return await GetPdf(questions, getQuestionFile);
        }

        public async Task<FileDataDto> PrintPerformingTestPdf(List<int> testsIds)
        {
            return await GetPdf(testsIds);
        }

        #region GetPdf

        private async Task<FileDataDto> GetPdf(TestTemplate testTemplate)
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
        private async Task<FileDataDto> GetPdf(QuestionUnit items, FileDataDto getQuestionFile)
        {
            string questionImage = "";
            int imageConfig;

            if (getQuestionFile != null)
            {
                questionImage = Convert.ToBase64String(getQuestionFile.Content);
                imageConfig = 1;
            }
            else 
            {
                questionImage = "null";
                imageConfig = 0;
            }
            
            var path = new FileInfo("PdfTemplates/one_multiple_question.html").FullName;

            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(items, questionImage, imageConfig);

            source = ReplaceKeys(source, myDictionary);

            var res = Parse(source);

             return  new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Intrebarea.pdf"
            };

        }
        private async Task<FileDataDto> GetPdf(List<int> testsIds)
        {
            var path = new FileInfo("PdfTemplates/PerformingTest.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(testsIds);

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

        private async Task<Dictionary<string, string>> GetDictionary(TestTemplate testTemplate)
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

            myDictionary.Add("{test_name}", item.TestTemplates.Name);
            myDictionary.Add("{nr_test_question}", item.TestTemplates.QuestionCount.ToString());
            myDictionary.Add("{test_time}", item.ProgrammedTime.ToString("dd/MM/yyyy, HH:mm"));
            myDictionary.Add("{min_percentage}", item.TestTemplates.MinPercent.ToString());
            myDictionary.Add("{event_name}", item.EventId != null ? item.Event.Name : "-");
            myDictionary.Add("{location_name}", item.LocationId != null ? item.Location.Name : "-");
            myDictionary.Add("{evaluat_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName);
            myDictionary.Add("{evaluator_name}", GetEvaluatorName(item));
            myDictionary.Add("{tr_area_replace}", await GetTableContent(item));

            return myDictionary;
        }
        private async Task<Dictionary<string, string>> GetDictionary(QuestionUnit items, string questionImage, int imageConfig)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{question_name}", await GetQuestionName(items.Id));
            myDictionary.Add("{question_image}", questionImage);
            myDictionary.Add("'image_config'", imageConfig.ToString());
            myDictionary.Add("{category_name}", items.QuestionCategory.Name);
            myDictionary.Add("{question_type}", EnumMessages.EnumMessages.GetQuestionType(items.QuestionType));
            myDictionary.Add("{question_points}", items.QuestionPoints.ToString());
            myDictionary.Add("{question_status}", EnumMessages.EnumMessages.GetQuestionStatus(items.Status));
            myDictionary.Add("{answer_option}", await GetTableContent(items));

            var dictionary = new Dictionary<string, Image>();

            return myDictionary;
        }
        
        private async Task<Dictionary<string, string>> GetDictionary(List<int> testsIds)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{content}", await GetTableContent(testsIds));

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

            foreach (var testCategory in item.TestTemplates.TestTypeQuestionCategories)
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

                if (item.TestTemplates.TestTypeQuestionCategories.Count() >= 2)
                {
                    content += $@"<tr>
                                <th colspan=""4"" style=""border: 1px solid black; border-collapse: collapse; background-color: rgba(223, 221, 221, 0.842); height: 35px;""></th>
                             </tr>";
                }
            }

            return content;
        }
        private async Task<string> GetTableContent(QuestionUnit questionOption)
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
                        var searchOptionFile = await  GetOptionFileToString(option);

                        if (option.IsCorrect)
                            if (searchOptionFile != null)
                            {
                                { content += $@"<tr>
                                <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>
                                    <div>
                                        <div>
                                            {option.Answer}
                                        </div>
                                        <img style='max-width: 100px' src='data:image/png;base64,{searchOptionFile}'>
                                    </div>
                                </th>
                                <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>Corect</th>
                                </tr>"; }
                            }
                            else
                            {
                                { content += $@"<tr>
                                    <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>
                                        {option.Answer}
                                    </th>
                                        <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>Corect</th>
                                </tr>"; }
                            }
                        else
                        {
                            if (searchOptionFile != null)
                            {
                                content +=
                                   $@"<tr>
                                         <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>
                                            <div>
                                                <div>
                                                     {option.Answer}
                                                </div>
                                                <img style='max-width: 100px' src='data:image/png;base64,{searchOptionFile}'>
                                            </div>
                                         </th>
                                         <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>Incorect</th>
                                    </tr>";
                            }
                            else 
                            {
                               content += 
                                    $@"<tr>
                                         <th colspan='2' style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>{option.Answer}</th>
                                         <th style='border: 1px solid black; border-collapse: collapse; text-align: left; padding-left: 5px; height: 30px;'>Incorect</th>
                                    </tr>"; 
                            }
                            
                        }
                    }
                }
            }
            return content;
        }
        private async Task<string> GetOptionFileToString(Option option)
        {

            var optionFile = _storageDbContext.Files.FirstOrDefault(f => f.Id.ToString() == option.MediaFileId && f.Type.Contains("image"));

            string setOptionFile = null;

            if (optionFile != null)
            {
                var getoptionFile = await _storageFileService.GetFile(optionFile.Id.ToString());

                 setOptionFile = Convert.ToBase64String(getoptionFile.Content);
            }

            return setOptionFile;
        }
        private async Task<string> GetTableContent(List<int> testsIds)
        {
            var content = string.Empty;
            var image = "";
            var header = "";
            var body = "";
            var footer = "";

            foreach (var testId in testsIds)
            {
                var item = _appDbContext.Tests
                    .Include(t => t.UserProfile)
                    .Include(t => t.Evaluator)
                    .Include(t => t.TestTemplates)
                    .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.QuestionUnit)
                    .ThenInclude(q => q.Options)
                    .FirstOrDefault(t => t.Id == testId);

                image = await GetImageContent();
                header = await GetHeaderContent(item);
                body = await GetTestQuestionContent(item);
                footer = await GetFooterContent(item);

                content += image + header + body + footer;

                if (testsIds.Count() >= 2)
                {
                    content += $@"<div style=""page-break-after: always;""></div>";
                }
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
        private string GetParsedSettingsForTestTemplate(TestTemplate testTemplate)
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

            if (item.EventId != null)
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
            if (question.MediaFileId != null)
            {
                content += await GetTestMedia(question.MediaFileId);
            }

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

                    if (option.value.MediaFileId != null)
                    {
                        content += await GetTestMedia(option.value.MediaFileId);
                    }
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

                    if (option.value.MediaFileId != null)
                    {
                        content += await GetTestMedia(option.value.MediaFileId);
                    }
                }

                content += $@"<div style=""margin-bottom: 20px;""></div>";
            }

            return content;
        }
        private async Task<string> GetTestMedia(string mediaId)
        {
            var content = string.Empty;
            var image = await _storageFileService.GetFile(mediaId);

            if (image != null)
            {
                var result = Convert.ToBase64String(image.Content);

                content = $@"<div style=""margin-bottom: 10px;"">
                                  <img style=""margin-left: 20px; max-width: 600px; max-height: 500px;""; src=""data:image/png;base64,{result}"" alt=""Red dot"" />
                              </div>";
            }
            return content;
        }
        private async Task<string> GetImageContent()
        {
            var content = string.Empty;

            content += $@"<div class=""header"">
                            <img style=""padding-left: 200px; width: 250px; height: 125px; padding-top: 10px;""; src=""data:image/png;base64,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j3ofuz55f3q4kLyw8Bv3hPP74uYdwgAAAAlwSFlzAAAOxAAADsQBlSsOGwAAYVFpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+Cjx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTM4IDc5LjE1OTgyNCwgMjAxNi8wOS8xNC0wMTowOTowMSAgICAgICAgIj4KICAgPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICAgICAgPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIKICAgICAgICAgICAgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIgogICAgICAgICAgICB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iCiAgICAgICAgICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgICAgICAgICB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIKICAgICAgICAgICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgICAgICAgICAgeG1sbnM6Y3JzPSJodHRwOi8vbnMuYWRvYmUuY29tL2NhbWVyYS1yYXctc2V0dGluZ3MvMS4wLyIKICAgICAgICAgICAgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIgogICAgICAgICAgICB4bWxuczp0aWZmPSJodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyIKICAgICAgICAgICAgeG1sbnM6ZXhpZj0iaHR0cDovL25zLmFkb2JlLmNvbS9leGlmLzEuMC8iPgogICAgICAgICA8eG1wOkNyZWF0b3JUb29sPkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE3IChXaW5kb3dzKTwveG1wOkNyZWF0b3JUb29sPgogICAgICAgICA8eG1wOkNyZWF0ZURhdGU+MjAxOC0wMy0yMVQxNjoxODowNDwveG1wOkNyZWF0ZURhdGU+CiAgICAgICAgIDx4bXA6TW9kaWZ5RGF0ZT4yMDE4LTA1LTMxVDExOjE3OjMwKzAzOjAwPC94bXA6TW9kaWZ5RGF0ZT4KICAgICAgICAgPHhtcDpNZXRhZGF0YURhdGU+MjAxOC0wNS0zMVQxMToxNzozMCswMzowMDwveG1wOk1ldGFkYXRhRGF0ZT4KICAgICAgICAgPHhtcDpSYXRpbmc+MDwveG1wOlJhdGluZz4KICAgICAgICAgPGRjOmZvcm1hdD5pbWFnZS9wbmc8L2RjOmZvcm1hdD4KICAgICAgICAgPHhtcE1NOkluc3RhbmNlSUQ+eG1wLmlpZDo3NDc2OWY4My0xNjIxLWZiNGQtYTI0MS05ZTg4Njg3NTkyZGI8L3htcE1NOkluc3RhbmNlSUQ+CiAgICAgICAgIDx4bXBNTTpEb2N1bWVudElEPnhtcC5kaWQ6NTExODU0MzgtZTM4Mi1jZDQ5LThlYjAtYjRlOGYyMjBkZDU1PC94bXBNTTpEb2N1bWVudElEPgogICAgICAgICA8eG1wTU06RGVyaXZlZEZyb20gcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICA8c3RSZWY6aW5zdGFuY2VJRD54bXAuaWlkOjFiYWU5OGQ3LWEzNjItYmI0NC05ODE4LTc1ZTYzZDExYmVhMTwvc3RSZWY6aW5zdGFuY2VJRD4KICAgICAgICAgICAgPHN0UmVmOmRvY3VtZW50SUQ+eG1wLmRpZDo1MTE4NTQzOC1lMzgyLWNkNDktOGViMC1iNGU4ZjIyMGRkNTU8L3N0UmVmOmRvY3VtZW50SUQ+CiAgICAgICAgICAgIDxzdFJlZjpvcmlnaW5hbERvY3VtZW50SUQ+eG1wLmRpZDoxREQyRkE4MDJEMUIxMUU4OUNDMjk0QUIyNTkwRkVDMTwvc3RSZWY6b3JpZ2luYWxEb2N1bWVudElEPgogICAgICAgICA8L3htcE1NOkRlcml2ZWRGcm9tPgogICAgICAgICA8eG1wTU06SGlzdG9yeT4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6YWN0aW9uPmRlcml2ZWQ8L3N0RXZ0OmFjdGlvbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OnBhcmFtZXRlcnM+Y29udmVydGVkIGZyb20gaW1hZ2UvanBlZyB0byBpbWFnZS90aWZmPC9zdEV2dDpwYXJhbWV0ZXJzPgogICAgICAgICAgICAgICA8L3JkZjpsaT4KICAgICAgICAgICAgICAgPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDoxNjE1Mzg3Yi0yOGI1LTI4NDMtOWFjZS1kNzFlZTllNzk2ZTE8L3N0RXZ0Omluc3RhbmNlSUQ+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDp3aGVuPjIwMTgtMDUtMzBUMTY6Mjc6MjArMDM6MDA8L3N0RXZ0OndoZW4+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIFBob3Rvc2hvcCBDYW1lcmEgUmF3IDguMyAoV2luZG93cyk8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+CiAgICAgICAgICAgICAgIDwvcmRmOmxpPgogICAgICAgICAgICAgICA8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjg5ZDhkYzUyLWNmZTItNDQ0MC1iYzMxLWJiNTQ4ZjYxNmNiYzwvc3RFdnQ6aW5zdGFuY2VJRD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OndoZW4+MjAxOC0wNS0zMFQxNjoyODo0MiswMzowMDwvc3RFdnQ6d2hlbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4KICAgICAgICAgICAgICAgPC9yZGY6bGk+CiAgICAgICAgICAgICAgIDxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6YWN0aW9uPmNvbnZlcnRlZDwvc3RFdnQ6YWN0aW9uPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6cGFyYW1ldGVycz5mcm9tIGltYWdlL3RpZmYgdG8gYXBwbGljYXRpb24vdm5kLmFkb2JlLnBob3Rvc2hvcDwvc3RFdnQ6cGFyYW1ldGVycz4KICAgICAgICAgICAgICAgPC9yZGY6bGk+CiAgICAgICAgICAgICAgIDxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6YWN0aW9uPmRlcml2ZWQ8L3N0RXZ0OmFjdGlvbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OnBhcmFtZXRlcnM+Y29udmVydGVkIGZyb20gaW1hZ2UvdGlmZiB0byBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wPC9zdEV2dDpwYXJhbWV0ZXJzPgogICAgICAgICAgICAgICA8L3JkZjpsaT4KICAgICAgICAgICAgICAgPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo1MTE4NTQzOC1lMzgyLWNkNDktOGViMC1iNGU4ZjIyMGRkNTU8L3N0RXZ0Omluc3RhbmNlSUQ+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDp3aGVuPjIwMTgtMDUtMzBUMTY6Mjg6NDIrMDM6MDA8L3N0RXZ0OndoZW4+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cyk8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+CiAgICAgICAgICAgICAgIDwvcmRmOmxpPgogICAgICAgICAgICAgICA8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjFiYWU5OGQ3LWEzNjItYmI0NC05ODE4LTc1ZTYzZDExYmVhMTwvc3RFdnQ6aW5zdGFuY2VJRD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OndoZW4+MjAxOC0wNS0zMFQxNjoyODo1NyswMzowMDwvc3RFdnQ6d2hlbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4KICAgICAgICAgICAgICAgPC9yZGY6bGk+CiAgICAgICAgICAgICAgIDxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6YWN0aW9uPmNvbnZlcnRlZDwvc3RFdnQ6YWN0aW9uPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6cGFyYW1ldGVycz5mcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nPC9zdEV2dDpwYXJhbWV0ZXJzPgogICAgICAgICAgICAgICA8L3JkZjpsaT4KICAgICAgICAgICAgICAgPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDphY3Rpb24+ZGVyaXZlZDwvc3RFdnQ6YWN0aW9uPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6cGFyYW1ldGVycz5jb
                                252ZXJ0ZWQgZnJvbSBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIHRvIGltYWdlL3BuZzwvc3RFdnQ6cGFyYW1ldGVycz4KICAgICAgICAgICAgICAgPC9yZGY6bGk+CiAgICAgICAgICAgICAgIDxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6MWE5YWFlNTgtYjcyMS1lODRjLWJmY2MtZTA5ZmQyZGE3ODhiPC9zdEV2dDppbnN0YW5jZUlEPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6d2hlbj4yMDE4LTA1LTMwVDE2OjI4OjU3KzAzOjAwPC9zdEV2dDp3aGVuPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpPC9zdEV2dDpzb2Z0d2FyZUFnZW50PgogICAgICAgICAgICAgICAgICA8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPgogICAgICAgICAgICAgICA8L3JkZjpsaT4KICAgICAgICAgICAgICAgPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo3NDc2OWY4My0xNjIxLWZiNGQtYTI0MS05ZTg4Njg3NTkyZGI8L3N0RXZ0Omluc3RhbmNlSUQ+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDp3aGVuPjIwMTgtMDUtMzFUMTE6MTc6MzArMDM6MDA8L3N0RXZ0OndoZW4+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE3IChXaW5kb3dzKTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4KICAgICAgICAgICAgICAgPC9yZGY6bGk+CiAgICAgICAgICAgIDwvcmRmOlNlcT4KICAgICAgICAgPC94bXBNTTpIaXN0b3J5PgogICAgICAgICA8eG1wTU06T3JpZ2luYWxEb2N1bWVudElEPnhtcC5kaWQ6MUREMkZBODAyRDFCMTFFODlDQzI5NEFCMjU5MEZFQzE8L3htcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD4KICAgICAgICAgPGNyczpSYXdGaWxlTmFtZT5jMy5qcGc8L2NyczpSYXdGaWxlTmFtZT4KICAgICAgICAgPGNyczpWZXJzaW9uPjguMzwvY3JzOlZlcnNpb24+CiAgICAgICAgIDxjcnM6UHJvY2Vzc1ZlcnNpb24+Ni43PC9jcnM6UHJvY2Vzc1ZlcnNpb24+CiAgICAgICAgIDxjcnM6V2hpdGVCYWxhbmNlPkFzIFNob3Q8L2NyczpXaGl0ZUJhbGFuY2U+CiAgICAgICAgIDxjcnM6QXV0b1doaXRlVmVyc2lvbj4xMzQzNDg4MDA8L2NyczpBdXRvV2hpdGVWZXJzaW9uPgogICAgICAgICA8Y3JzOkluY3JlbWVudGFsVGVtcGVyYXR1cmU+MDwvY3JzOkluY3JlbWVudGFsVGVtcGVyYXR1cmU+CiAgICAgICAgIDxjcnM6SW5jcmVtZW50YWxUaW50PjA8L2NyczpJbmNyZW1lbnRhbFRpbnQ+CiAgICAgICAgIDxjcnM6U2F0dXJhdGlvbj4wPC9jcnM6U2F0dXJhdGlvbj4KICAgICAgICAgPGNyczpTaGFycG5lc3M+MDwvY3JzOlNoYXJwbmVzcz4KICAgICAgICAgPGNyczpMdW1pbmFuY2VTbW9vdGhpbmc+MDwvY3JzOkx1bWluYW5jZVNtb290aGluZz4KICAgICAgICAgPGNyczpDb2xvck5vaXNlUmVkdWN0aW9uPjA8L2NyczpDb2xvck5vaXNlUmVkdWN0aW9uPgogICAgICAgICA8Y3JzOlZpZ25ldHRlQW1vdW50PjA8L2NyczpWaWduZXR0ZUFtb3VudD4KICAgICAgICAgPGNyczpTaGFkb3dUaW50PjA8L2NyczpTaGFkb3dUaW50PgogICAgICAgICA8Y3JzOlJlZEh1ZT4wPC9jcnM6UmVkSHVlPgogICAgICAgICA8Y3JzOlJlZFNhdHVyYXRpb24+MDwvY3JzOlJlZFNhdHVyYXRpb24+CiAgICAgICAgIDxjcnM6R3JlZW5IdWU+MDwvY3JzOkdyZWVuSHVlPgogICAgICAgICA8Y3JzOkdyZWVuU2F0dXJhdGlvbj4wPC9jcnM6R3JlZW5TYXR1cmF0aW9uPgogICAgICAgICA8Y3JzOkJsdWVIdWU+MDwvY3JzOkJsdWVIdWU+CiAgICAgICAgIDxjcnM6Qmx1ZVNhdHVyYXRpb24+MDwvY3JzOkJsdWVTYXR1cmF0aW9uPgogICAgICAgICA8Y3JzOlZpYnJhbmNlPjA8L2NyczpWaWJyYW5jZT4KICAgICAgICAgPGNyczpIdWVBZGp1c3RtZW50UmVkPjA8L2NyczpIdWVBZGp1c3RtZW50UmVkPgogICAgICAgICA8Y3JzOkh1ZUFkanVzdG1lbnRPcmFuZ2U+MDwvY3JzOkh1ZUFkanVzdG1lbnRPcmFuZ2U+CiAgICAgICAgIDxjcnM6SHVlQWRqdXN0bWVudFllbGxvdz4wPC9jcnM6SHVlQWRqdXN0bWVudFllbGxvdz4KICAgICAgICAgPGNyczpIdWVBZGp1c3RtZW50R3JlZW4+MDwvY3JzOkh1ZUFkanVzdG1lbnRHcmVlbj4KICAgICAgICAgPGNyczpIdWVBZGp1c3RtZW50QXF1YT4wPC9jcnM6SHVlQWRqdXN0bWVudEFxdWE+CiAgICAgICAgIDxjcnM6SHVlQWRqdXN0bWVudEJsdWU+MDwvY3JzOkh1ZUFkanVzdG1lbnRCbHVlPgogICAgICAgICA8Y3JzOkh1ZUFkanVzdG1lbnRQdXJwbGU+MDwvY3JzOkh1ZUFkanVzdG1lbnRQdXJwbGU+CiAgICAgICAgIDxjcnM6SHVlQWRqdXN0bWVudE1hZ2VudGE+MDwvY3JzOkh1ZUFkanVzdG1lbnRNYWdlbnRhPgogICAgICAgICA8Y3JzOlNhdHVyYXRpb25BZGp1c3RtZW50UmVkPjA8L2NyczpTYXR1cmF0aW9uQWRqdXN0bWVudFJlZD4KICAgICAgICAgPGNyczpTYXR1cmF0aW9uQWRqdXN0bWVudE9yYW5nZT4wPC9jcnM6U2F0dXJhdGlvbkFkanVzdG1lbnRPcmFuZ2U+CiAgICAgICAgIDxjcnM6U2F0dXJhdGlvbkFkanVzdG1lbnRZZWxsb3c+MDwvY3JzOlNhdHVyYXRpb25BZGp1c3RtZW50WWVsbG93PgogICAgICAgICA8Y3JzOlNhdHVyYXRpb25BZGp1c3RtZW50R3JlZW4+MDwvY3JzOlNhdHVyYXRpb25BZGp1c3RtZW50R3JlZW4+CiAgICAgICAgIDxjcnM6U2F0dXJhdGlvbkFkanVzdG1lbnRBcXVhPjA8L2NyczpTYXR1cmF0aW9uQWRqdXN0bWVudEFxdWE+CiAgICAgICAgIDxjcnM6U2F0dXJhdGlvbkFkanVzdG1lbnRCbHVlPjA8L2NyczpTYXR1cmF0aW9uQWRqdXN0bWVudEJsdWU+CiAgICAgICAgIDxjcnM6U2F0dXJhdGlvbkFkanVzdG1lbnRQdXJwbGU+MDwvY3JzOlNhdHVyYXRpb25BZGp1c3RtZW50UHVycGxlPgogICAgICAgICA8Y3JzOlNhdHVyYXRpb25BZGp1c3RtZW50TWFnZW50YT4wPC9jcnM6U2F0dXJhdGlvbkFkanVzdG1lbnRNYWdlbnRhPgogICAgICAgICA8Y3JzOkx1bWluYW5jZUFkanVzdG1lbnRSZWQ+MDwvY3JzOkx1bWluYW5jZUFkanVzdG1lbnRSZWQ+CiAgICAgICAgIDxjcnM6THVtaW5hbmNlQWRqdXN0bWVudE9yYW5nZT4wPC9jcnM6THVtaW5hbmNlQWRqdXN0bWVudE9yYW5nZT4KICAgICAgICAgPGNyczpMdW1pbmFuY2VBZGp1c3RtZW50WWVsbG93PjA8L2NyczpMdW1pbmFuY2VBZGp1c3RtZW50WWVsbG93PgogICAgICAgICA8Y3JzOkx1bWluYW5jZUFkanVzdG1lbnRHcmVlbj4wPC9jcnM6THVtaW5hbmNlQWRqdXN0bWVudEdyZWVuPgogICAgICAgICA8Y3JzOkx1bWluYW5jZUFkanVzdG1lbnRBcXVhPjA8L2NyczpMdW1pbmFuY2VBZGp1c3RtZW50QXF1YT4KICAgICAgICAgPGNyczpMdW1pbmFuY2VBZGp1c3RtZW50Qmx1ZT4wPC9jcnM6THVtaW5hbmNlQWRqdXN0bWVudEJsdWU+CiAgICAgICAgIDxjcnM6THVtaW5hbmNlQWRqdXN0bWVudFB1cnBsZT4wPC9jcnM6THVtaW5hbmNlQWRqdXN0bWVudFB1cnBsZT4KICAgICAgICAgPGNyczpMdW1pbmFuY2VBZGp1c3RtZW50TWFnZW50YT4wPC9jcnM6THVtaW5hbmNlQWRqdXN0bWVudE1hZ2VudGE+CiAgICAgICAgIDxjcnM6U3BsaXRUb25pbmdTaGFkb3dIdWU+MDwvY3JzOlNwbGl0VG9uaW5nU2hhZG93SHVlPgogICAgICAgICA8Y3JzOlNwbGl0VG9uaW5nU2hhZG93U2F0dXJhdGlvbj4wPC9jcnM6U3BsaXRUb25pbmdTaGFkb3dTYXR1cmF0aW9uPgogICAgICAgICA8Y3JzOlNwbGl0VG9uaW5nSGlnaGxpZ2h0SHVlPjA8L2NyczpTcGxpdFRvbmluZ0hpZ2hsaWdodEh1ZT4KICAgICAgICAgPGNyczpTcGxpdFRvbmluZ0hpZ2hsaWdodFNhdHVyYXRpb24+MDwvY3JzOlNwbGl0VG9uaW5nSGlnaGxpZ2h0U2F0dXJhdGlvbj4KICAgICAgICAgPGNyczpTcGxpdFRvbmluZ0JhbGFuY2U+MDwvY3JzOlNwbGl0VG9uaW5nQmFsYW5jZT4KICAgICAgICAgPGNyczpQYXJhbWV0cmljU2hhZG93cz4wPC9jcnM6UGFyYW1ldHJpY1NoYWRvd3M+CiAgICAgICAgIDxjcnM6UGFyYW1ldHJpY0RhcmtzPjA8L2NyczpQYXJhbWV0cmljRGFya3M+CiAgICAgICAgIDxjcnM6UGFyYW1ldHJpY0xpZ2h0cz4wPC9jcnM6UGFyYW1ldHJpY0xpZ2h0cz4KICAgICAgICAgPGNyczpQYXJhbWV0cmljSGlnaGxpZ2h0cz4wPC9jcnM6UGFyYW1ldHJpY0hpZ2hsaWdodHM+CiAgICAgICAgIDxjcnM6UGFyYW1ldHJpY1NoYWRvd1NwbGl0PjI1PC9jcnM6UGFyYW1ldHJpY1NoYWRvd1NwbGl0PgogICAgICAgICA8Y3JzOlBhcmFtZXRyaWNNaWR0b25lU3BsaXQ+NTA8L2NyczpQYXJhbWV0cmljTWlkdG9uZVNwbGl0PgogICAgICAgICA8Y3JzOlBhcmFtZXRyaWNIaWdobGlnaHRTcGxpdD43NTwvY3JzOlBhcmFtZXRyaWNIaWdobGlnaHRTcGxpdD4KICAgICAgICAgPGNyczpTaGFycGVuUmFkaXVzPisxLjA8L2NyczpTaGFycGVuUmFkaXVzPgogICAgICAgICA8Y3JzOlNoYXJwZW5EZXRhaWw+MjU8L2NyczpTaGFycGVuRGV0YWlsPgogICAgICAgICA8Y3JzOlNoYXJwZW5FZGdlTWFza2luZz4wPC9jcnM6U2hhcnBlbkVkZ2VNYXNraW5nPgogICAgICAgICA8Y3JzOlBvc3RDcm9wVmlnbmV0dGVBbW91bnQ+MDwvY3JzOlBvc3RDcm9wVmlnbmV0dGVBbW91bnQ+CiAgICAgICAgIDxjcnM6R3JhaW5BbW91bnQ+MDwvY3JzOkdyYWluQW1vdW50PgogICAgICAgICA8Y3JzOkxlbnNQcm9maWxlRW5hYmxlPjA8L2NyczpMZW5zUHJvZmlsZUVuYWJsZT4KICAgICAgICAgPGNyczpMZW5zTWFudWFsRGlzdG9ydGlvbkFtb3VudD4wPC9jcnM6TGVuc01hbnVhbERpc3RvcnRpb25BbW91bnQ+CiAgICAgICAgIDxjcnM6UGVyc3BlY3RpdmVWZXJ0aWNhbD4wPC9jcnM6UGVyc3BlY3RpdmVWZXJ0aWNhbD4KICAgICAgICAgPGNyczpQZXJzcGVjdGl2ZUhvcml6b250YWw+MDwvY3JzOlBlcnNwZWN0aXZlSG9yaXpvbnRhbD4KICAgICAgICAgPGNyczpQZXJzcGVjdGl2ZVJvdGF0ZT4wLjA8L2NyczpQZXJzcGVjdGl2ZVJvdGF0ZT4KICAgICAgICAgPGNyczpQZXJzcGVjdGl2ZVNjYWxlPjEwMDwvY3JzOlBlcnNwZWN0aXZlU2NhbGU+CiAgICAgICAgIDxjcnM6UGVyc3BlY3RpdmVBc3BlY3Q+MDwvY3JzOlBlcnNwZWN0aXZlQXNwZWN0PgogICAgICAgICA8Y3JzOlBlcnNwZWN0aXZlVXByaWdodD4wPC9jcnM6UGVyc3BlY3RpdmVVcHJpZ2h0PgogICAgICAgICA8Y3JzOkF1dG9MYXRlcmFsQ0E+MDwvY3JzOkF1dG9MYXRlcmFsQ0E+CiAgICAgICAgIDxjcnM6RXhwb3N1cmUyMDEyPjAuMDA8L2NyczpFeHBvc3VyZTIwMTI+CiAgICAgICAgIDxjcnM6Q29udHJhc3QyMDEyPjA8L2NyczpDb250cmFzdDIwMTI+CiAgICAgICAgIDxjcnM6SGlnaGxpZ2h0czIwMTI+MDwvY3JzOkhpZ2hsaWdodHMyMDEyPgogICAgICAgICA8Y3JzOlNoYWRvd3MyMDEyPjA8L2NyczpTaGFkb3dzMjAxMj4KICAgICAgICAgPGNyczpXaGl0ZXMyMDEyPjA8L2NyczpXaGl0ZXMyMDEyPgogICAgICAgICA8Y3JzOkJsYWNrczIwMTI+MDwvY3JzOkJsYWNrczIwMTI+CiAgICAgICAgIDxjcnM6Q2xhcml0eTIwMTI+MDwvY3JzOkNsYXJpdHkyMDEyPgogICAgICAgICA8Y3JzOkRlZnJpbmdlUHVycGxlQW1vdW50PjA8L2NyczpEZWZyaW5nZVB1cnBsZUFtb3VudD4KICAgICAgICAgPGNyczpEZWZyaW5nZVB1cnBsZUh1ZUxvPjMwPC9jcnM6RGVmcmluZ2VQdXJwbGVIdWVMbz4KICAgICAgICAgPGNyczpEZWZyaW5nZVB1cnBsZUh1ZUhpPjcwPC9jcnM6RGVmcmluZ2VQdXJwbGVIdWVIaT4KICAgICAgICAgPGNyczpEZWZyaW5nZUdyZWVuQW1vdW50PjA8L2NyczpEZWZyaW5nZUdyZWVuQW1vdW50PgogICAgICAgICA8Y3JzOkRlZnJpbmdlR3JlZW5IdWVMbz40MDwvY3JzOkRlZnJpbmdlR3JlZW5IdWVMbz4KICAgICAgICAgPGNyczpEZWZyaW5nZUdyZWVuSHVlSGk+NjA8L2NyczpEZWZyaW5nZUdyZWVuSHVlSGk+CiAgICAgICAgIDxjcnM6Q29udmVydFRvR3JheXNjYWxlPkZhbHNlPC9jcnM6Q29udmVydFRvR3JheXNjYWxlPgogICAgICAgICA8Y3JzOlRvbmVDdXJ2ZU5hbWUyMDEyPkxpbmVhcjwvY3JzOlRvbmVDdXJ2ZU5hbWUyMDEyPgogICAgICAgICA8Y3JzOlRvbmVDdXJ2ZVBWMjAxMj4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGk+MCwgMDwvcmRmOmxpPgogICAgICAgICAgICAgICA8cmRmOmxpPjI1NSwgMjU1PC9yZGY6bGk+CiAgICAgICAgICAgIDwvcmRmOlNlcT4KICAgICAgICAgPC9jcnM6VG9uZUN1cnZlUFYyMDEyPgogICAgICAgICA8Y3JzOlRvbmVDdXJ2ZVBWMjAxMlJlZD4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGk+MCwgMDwvcmRmOmxpPgogICAgICAgICAgICAgICA8cmRmOmxpPjI1NSwgMjU1PC9yZGY6bGk+CiAgICAgICAgICAgIDwvcmRmOlNlcT4KICAgICAgICAgPC9jcnM6VG9uZUN1cnZlUFYyMDEyUmVkPgogICAgICAgICA8Y3JzOlRvbmVDdXJ2ZVBWMjAxMkdyZWVuPgogICAgICAgICAgICA8cmRmOlNlcT4KICAgICAgICAgICAgICAgPHJkZjpsaT4wLCAwPC9yZGY6bGk+CiAgICAgICAgICAgICAgIDxyZGY6bGk+MjU1LCAyNTU8L3JkZjpsaT4KICAgICAgICAgICAgPC9yZGY6U2VxPgogICAgICAgICA8L2NyczpUb25lQ3VydmVQVjIwMTJHcmVlbj4KICAgICAgICAgPGNyczpUb25lQ3VydmVQVjIwMTJCbHVlPgogICAgICAgICAgICA8cmRmOlNlcT4KICAgICAgICAgICAgICAgPHJkZjpsaT4wLCAwPC9yZGY6bGk+CiAgICAgICAgICAgICAgIDxyZGY6bGk+MjU1LCAyNTU8L3JkZjpsaT4KICAgICAgICAgICAgPC9yZGY6U2VxPgogICAgICAgICA8L2NyczpUb25lQ3VydmVQVjIwMTJCbHVlPgogICAgICAgICA8Y3JzOkNhbWVyYVByb2ZpbGU+RW1iZWRkZWQ8L2NyczpDYW1lcmFQcm9maWxlPgogICAgICAgICA8Y3JzOkNhbWVyYVByb2ZpbGVEaWdlc3Q+NTQ2NTBBMzQxQjVCNUNDQUU4NDQyRDBCNDNBOTJCQ0U8L2NyczpDYW1lcmFQcm9maWxlRGlnZXN0PgogICAgICAgICA8Y3JzOkxlbnNQcm9maWxlU2V0dXA+TGVuc0RlZmF1bHRzPC9jcnM6TGVuc1Byb2ZpbGVTZXR1cD4KICAgICAgICAgPGNyczpIYXNTZXR0aW5ncz5UcnVlPC9jcnM6SGFzU2V0dGluZ3M+CiAgICAgICAgIDxjcnM6SGFzQ3JvcD5GYWxzZTwvY3JzOkhhc0Nyb3A+CiAgICAgICAgIDxjcnM6QWxyZWFkeUFwcGxpZWQ+VHJ1ZTwvY3JzOkFscmVhZHlBcHBsaWVkPgogICAgICAgICA8cGhvdG9zaG9wOkNvbG9yTW9kZT4zPC9waG90b3Nob3A6Q29sb3JNb2RlPgogICAgICAgICA8cGhvdG9zaG9wOklDQ1Byb2ZpbGU+c1JHQiBJRUM2MTk2Ni0yLjE8L3Bob3Rvc2hvcDpJQ0NQcm9
                                maWxlPgogICAgICAgICA8cGhvdG9zaG9wOkRvY3VtZW50QW5jZXN0b3JzPgogICAgICAgICAgICA8cmRmOkJhZz4KICAgICAgICAgICAgICAgPHJkZjpsaT54bXAuZGlkOjE2MTUzODdiLTI4YjUtMjg0My05YWNlLWQ3MWVlOWU3OTZlMTwvcmRmOmxpPgogICAgICAgICAgICA8L3JkZjpCYWc+CiAgICAgICAgIDwvcGhvdG9zaG9wOkRvY3VtZW50QW5jZXN0b3JzPgogICAgICAgICA8dGlmZjpPcmllbnRhdGlvbj4xPC90aWZmOk9yaWVudGF0aW9uPgogICAgICAgICA8dGlmZjpYUmVzb2x1dGlvbj4zMDAwMDAwLzEwMDAwPC90aWZmOlhSZXNvbHV0aW9uPgogICAgICAgICA8dGlmZjpZUmVzb2x1dGlvbj4zMDAwMDAwLzEwMDAwPC90aWZmOllSZXNvbHV0aW9uPgogICAgICAgICA8dGlmZjpSZXNvbHV0aW9uVW5pdD4yPC90aWZmOlJlc29sdXRpb25Vbml0PgogICAgICAgICA8ZXhpZjpFeGlmVmVyc2lvbj4wMjIwPC9leGlmOkV4aWZWZXJzaW9uPgogICAgICAgICA8ZXhpZjpDb2xvclNwYWNlPjE8L2V4aWY6Q29sb3JTcGFjZT4KICAgICAgICAgPGV4aWY6UGl4ZWxYRGltZW5zaW9uPjExNzwvZXhpZjpQaXhlbFhEaW1lbnNpb24+CiAgICAgICAgIDxleGlmOlBpeGVsWURpbWVuc2lvbj4xNDQ8L2V4aWY6UGl4ZWxZRGltZW5zaW9uPgogICAgICA8L3JkZjpEZXNjcmlwdGlvbj4KICAgPC9yZGY6UkRGPgo8L3g6eG1wbWV0YT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgIC
                                AgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA
                                gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAKPD94cGFja2V0IGVuZD0idyI/Ptf/tuYAAJPkSURBVHhe7L0FgFVl9z28btd0NzPU0N2lgKQCJnZ3YGG32KgY2IqCiWILgiAd0j3AMEzPMN1zO8639rkDLyIo+qqv/+/n1od759wTT+y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
                                "" alt=""logo""> &nbsp;
                                <div style=""position: relative;"">
                                    <span style=""position: absolute; right: 100px; bottom: 57px; font-weight: bold;"">MINISTERUL AFACERILOR INTERNE</span>
                                </div>
                           </div>";

            return content;
        }
        private async Task<string> GetHeaderContent(Test item)
        {
            var content = string.Empty;

            content += $@"
                <div style=""padding-top: 150px;"">
                    <h2 style=""text-align: right; font-size: 18px; font-weight: 100;"">Numele, prenumele candidatului(ei): {item.UserProfile.FirstName} {item.UserProfile.LastName}</h2>
                    <h2 style=""text-align: right; font-size: 18px; font-weight: 100;"">Procentul minim de trecere: {item.TestTemplates.MinPercent}</h2>
                </div>
                <div style=""margin-top: 50px;"">
                    <h2 style=""text-align: center; font-size: 22px;"">{item.TestTemplates.Name}</h2>
                    <h2 style=""text-align: center; font-size: 18px;""> Durata testului: {item.TestTemplates.Duration} min</h2>
                </div>";

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

                content += $@"<div style=""margin-bottom: 20px; width: 930px;""><b>{testQuestion.i + 1}. {testQuestion.value.QuestionUnit.Question}</b> ({testQuestion.value.QuestionUnit.QuestionPoints}p)</div>";

                content = await GetQuestionTemplateByType(testQuestion.value.QuestionUnit, content);
            }

            return content;
        }
        private async Task<string> GetFooterContent(Test item)
        {
            var content = string.Empty;

            content += $@"<div style=""margin-top: 50px;"">
                            <h2 style=""text-align: left; font-size: 22px;"">Evaluator: {GetEvaluatorName(item)} ______________</h2>
                            <h2 style=""text-align: left; font-size: 18px;"">{item.ProgrammedTime.ToString("dd-MM-yyyy")}</h2>
                        </div>";

            return content;
        }
    }
}
