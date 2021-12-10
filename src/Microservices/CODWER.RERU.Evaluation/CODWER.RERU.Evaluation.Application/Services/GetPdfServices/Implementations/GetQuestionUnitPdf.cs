using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices.Implementations
{
    public class GetQuestionUnitPdf : IGetQuestionUnitPdf
    {
        private readonly IGeneratePdf _generatePdf;
        private readonly AppDbContext _appDbContext;
        private readonly IQuestionUnitService _questionUnitService;

        public GetQuestionUnitPdf(IGeneratePdf generatePdf, AppDbContext appDbContext, IQuestionUnitService questionUnitService)
        {
            _generatePdf = generatePdf;
            _appDbContext = appDbContext;
            _questionUnitService = questionUnitService;
        }
        public async Task<FileDataDto> PrintQuestionUnitPdf(int questionId)
        {
            var questions = _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(op => op.Options)
                .FirstOrDefault(x => x.Id == questionId);

            return await GetPdf(questions);
        }

        private string GetQuestionOptions(QuestionUnit questionOption)
        {
            var content = string.Empty;
            if (questionOption.QuestionType == QuestionTypeEnum.MultipleAnswers || questionOption.QuestionType == QuestionTypeEnum.OneAnswer)
            {
               var options = questionOption.Options.ToList();

                if (options !=null)
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

        public async Task<FileDataDto> GetPdf(QuestionUnit items)
        {
            byte[] res;
            var path = new FileInfo("PdfTemplates/one_multiple_question.html").FullName;
            var source = await File.ReadAllTextAsync(path);

             source = source.Replace("{question_name}", await GetQuestionName(items.Id))
                            .Replace("{category_name}", items.QuestionCategory.Name)
                            .Replace("{question_type}", EnumMessages.EnumMessages.GetQuestionType(items.QuestionType))
                            .Replace("{question_points}", items.QuestionPoints.ToString())
                            .Replace("{question_status}", EnumMessages.EnumMessages.GetQuestionStatus(items.Status))
                            .Replace("{answer_option}", GetQuestionOptions(items));

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
                Name = "Intrebarea.pdf"
            };
        }
        private async Task<string> GetQuestionName(int id)
        {
            var questionUnit = _appDbContext.QuestionUnits.FirstOrDefault(x => x.Id == id);

            if (questionUnit != null)
            {
                if (questionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
                {
                    questionUnit = await _questionUnitService.GetUnHashedQuestionUnit(questionUnit.Id);
                    questionUnit.Options = null;
                }
            }
            return questionUnit.Question;
        }
    }
}
