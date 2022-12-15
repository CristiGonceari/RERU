using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions;
using CVU.ERP.Common.DataTransferObjects.Files;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
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
            var testTemplate = await _appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                    .ThenInclude(x => x.TestCategoryQuestions)
                .Include(x => x.TestTemplateQuestionCategories)
                    .ThenInclude(x => x.QuestionCategory)
                        .ThenInclude(x => x.QuestionUnits)
                .Include(x => x.Settings)
                .FirstOrDefaultAsync(x => x.Id == testTemplateId);

            return await GetPdf(testTemplate);
        }

        public async Task<FileDataDto> GetPdf(TestTemplate testTemplate)
        {
            var path = new FileInfo("PdfTemplates/TestTemplate.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetDictionary(testTemplate);

            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }

            var res = Parse(source);

            return FileDataDto.GetPdf("Sablon_De_Test.pdf", res);
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
            myDictionary.Add("{category_replace}", await GetTableContent(testTemplate.TestTemplateQuestionCategories.ToList()));

            return  myDictionary;
        }

        private async Task<string> GetTableContent(List<TestTemplateQuestionCategory> testTemplateQuestionCategories)
        {
            var content = string.Empty;

            foreach (var item in testTemplateQuestionCategories)
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
                          <b>{item.QuestionCount}</b> {ParseQuestion(item.QuestionCount)} din <b>{item.QuestionCategory.QuestionUnits.Count}</b>, ordinea intrebarilor - {EnumMessages.Translate(item.SequenceType)}
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

        private string BuildHtmlContentForQuestions(TestCategoryQuestionContentDto testTemplateQuestionCategory)
        {
            return testTemplateQuestionCategory.Questions.Aggregate(string.Empty, (current, questionUnit)
                =>
                current + $@"
            <tr>
                <th colspan=""3"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px; max-width: 500px"">{questionUnit.Question}</th>
                <th colspan=""1"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;"">{EnumMessages.Translate(questionUnit.QuestionType)}</th>
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
        private async Task<TestCategoryQuestionContentDto> GetQuestionsForCategoryContent(int testTemplateQuestionCategoryId)
        {
            var command = new TestCategoryQuestionsQuery
            {
                TestTemplateQuestionCategoryId = testTemplateQuestionCategoryId
            };

            return  await _mediator.Send(command);
        }
    }
}

