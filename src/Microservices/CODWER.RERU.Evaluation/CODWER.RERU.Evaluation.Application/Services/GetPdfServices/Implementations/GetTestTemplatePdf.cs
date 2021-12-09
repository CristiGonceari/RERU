using CODWER.RERU.Evaluation.Application.TestCategoryQuestions;
using CODWER.RERU.Evaluation.Data.Entities;
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

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices.Implementations
{
    public class GetTestTemplatePdf : IGetTestTemplatePdf
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly IMediator _mediator;

        public GetTestTemplatePdf(AppDbContext appDbContext,
            IGeneratePdf generatePdf,
            IMediator mediator)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _mediator = mediator;
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

        public async Task<FileDataDto> GetPdf(TestType testTemplate)
        {
            var path = new FileInfo("PdfTemplates/TestTemplate.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetOrderDictionary(testTemplate);

            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }


            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Sablon_De_Test.pdf"
            };
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

        private async Task<Dictionary<string, string>> GetOrderDictionary(TestType testTemplate)
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

            return  myDictionary;
        }

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
            var testTemplateRules = rules;

            if (string.IsNullOrEmpty(rules)) return "-";

            var base64EncodedBytes = Convert.FromBase64String(rules);
            testTemplateRules = Encoding.UTF8.GetString(base64EncodedBytes);

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

            return  await _mediator.Send(command);
        }
    }
}

