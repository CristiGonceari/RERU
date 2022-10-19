﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
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
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.TestTemplateQuestionCategories)
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

        private async Task<string> GetTableContentForTest(Test item)
        {
            var content = string.Empty;

            foreach (var testCategory in item.TestTemplate.TestTemplateQuestionCategories)
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
                                    <th colspan=""2"" style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;""><b>{testCategory.QuestionCount}</b> din <b>{testCategory.QuestionCategory.QuestionUnits.Count}</b>, oridnea - {EnumMessages.GetQuestionSequence(testCategory.SequenceType)}</th>
                                </tr>
                                <tr>
                                    <th style=""border: 1px solid black; border-collapse: collapse; text-align: left; background-color: #1f3864; color: white; height: 30px;"">Lista de întrebări</th>
                                    <th style=""border: 1px solid black; border-collapse: collapse; text-align: left; background-color: #1f3864; color: white; height: 30px;"">Tipul întrebării</th>
                                </tr>";

                var testCategoryQuestionData = await _mediator.Send(new TestCategoryQuestionsQuery { TestTemplateQuestionCategoryId = testCategory.Id });
                
                foreach (var question in testCategoryQuestionData.Questions)
                {
                    content += $@"<tr>
                                <th style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px; max-width: 500px;"">{question.Question}</th>
                                <th style=""border: 1px solid black; border-collapse: collapse; height: 30px; font-size: 15px;"">{EnumMessages.GetQuestionType(question.QuestionType)}</th>
                            </tr> ";
                }

                if (item.TestTemplate.TestTemplateQuestionCategories.Count() >= 2)
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
            var path = new FileInfo("PdfTemplates/Test.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            var myDictionary = await GetOrderDictionary(item);


            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }

            //source = source.Replace("{test_name}", item.TestTemplate.FirstName);
            //source = source.Replace("{nr_test_question}", item.TestTemplate.QuestionCount.ToString());
            //source = source.Replace("{test_time}", item.ProgrammedTime.ToString("dd/MM/yyyy, HH:mm"));
            //source = source.Replace("{min_percentage}", item.TestTemplate.MinPercent.ToString());
            //source = source.Replace("{event_name}", item.EventId != null ? item.Event.FirstName : "-");
            //source = source.Replace("{location_name}", item.LocationId != null ? item.Location.FirstName : "-");
            //source = source.Replace("{evaluat_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName);
            //source = source.Replace("{evaluator_name}", getEvaluatorName(item));
            //source = source.Replace("{tr_area_replace}", await GetTableContentAsync(item));

            var res = Parse(source);

            return FileDataDto.GetPdf("Test.pdf", res);
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

        private async Task<Dictionary<string, string>> GetOrderDictionary(Test item)
        {

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{test_name}", item.TestTemplate.Name);
            myDictionary.Add("{nr_test_question}", item.TestTemplate.QuestionCount.ToString());
            myDictionary.Add("{test_time}", item.ProgrammedTime.ToString("dd/MM/yyyy, HH:mm"));
            myDictionary.Add("{min_percentage}", item.TestTemplate.MinPercent.ToString());
            myDictionary.Add("{event_name}", item.EventId != null ? item.Event.Name : "-");
            myDictionary.Add("{location_name}", item.LocationId != null ? item.Location.Name : "-");
            myDictionary.Add("{evaluat_name}", item.UserProfile.LastName + " " + item.UserProfile.FirstName);
            myDictionary.Add("{evaluator_name}", getEvaluatorName(item));
            myDictionary.Add("{tr_area_replace}", await GetTableContentForTest(item));

            return myDictionary;
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

                list.AddRange(evaluators.Select(evaluator => evaluator.Evaluator.LastName + " " + evaluator.Evaluator.FirstName));
                var combineString = string.Join(", ", list);

                return combineString;
            }

            if(item.EvaluatorId != null)
            {
                return item.Evaluator.LastName + " " + item.Evaluator.FirstName;
            }

            return "-";
        }
    }
}
