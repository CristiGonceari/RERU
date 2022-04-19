using CVU.ERP.Common.DataTransferObjects.Files;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices.Implementations
{
    public class GetTestTemplateDocumentReplacedKeys : IGetTestTemplateDocumentReplacedKeys
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;

        public GetTestTemplateDocumentReplacedKeys(AppDbContext appDbContext, IGeneratePdf generatePdf)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
        }

        public async Task<string> GetTestTemplateDocumentReplacedKey(TestTemplate testTemplate, int documentTemplateId)
        {
            var documentTemplate = _appDbContext.DocumentTemplates.FirstOrDefault(dt => dt.Id == documentTemplateId);

            return await ReplaceTestTemplateDocumentKeys(documentTemplate.Value, testTemplate);
        }

        public async Task<FileDataDto> GetPdf(string source, string testTemplateName)
        {
            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = testTemplateName
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

        public async Task<string> ReplaceTestTemplateDocumentKeys(string source, TestTemplate testTemplate) 
        {
            var myDictionary = await GetDictionay(testTemplate);

            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }
            return source;
        }

        public async Task<Dictionary<string, string>> GetDictionay(TestTemplate testTemplate)
        {
            var keys = _appDbContext.DocumentTemplateKeys
                                        .Where(dtk => dtk.FileType == FileTypeEnum.TestTemplate)
                                        .Select(dtk => dtk.KeyName)
                                        .ToList();

            var myDictionary = new Dictionary<string, string>();

            foreach (var item in keys)
            {
                switch (item)
                {
                    case "{today_date_key_key}":

                        var date = DateTime.Now;

                        myDictionary.Add(item, date.ToString("dd/MM/yyyy").Replace("-","/"));

                        break;

                    case "{test_template_name_key}":

                        myDictionary.Add(item, testTemplate.Name);

                        break;

                    case "{rules_key}":

                        if(testTemplate.Rules != null)
                        {

                            var base64EncodedBytes = Convert.FromBase64String(testTemplate.Rules);
                            var rules = Encoding.UTF8.GetString(base64EncodedBytes);

                            myDictionary.Add(item, rules);

                        }
                        else
                        {
                            myDictionary.Add(item, "-");
                        }
                        break;

                    case "{question_count_key}":

                        myDictionary.Add(item, testTemplate.QuestionCount.ToString());

                        break;

                    case "{min_percent_key}":

                        myDictionary.Add(item, testTemplate.MinPercent.ToString() + " %");

                        break;

                    case "{duration_key}":

                        myDictionary.Add(item, testTemplate.Duration.ToString() + " min");

                        break;

                    case "{settings_max_errors_key}":

                        if (testTemplate.Settings.MaxErrors != null)
                        {
                            myDictionary.Add(item, testTemplate.Settings.MaxErrors.ToString());
                        }
                        else 
                        {
                            myDictionary.Add(item, "max_errors_key has not been set");
                        }

                        break;

                    case "{settings_formula_for_one_answer_key}":

                        if (testTemplate.Settings.FormulaForOneAnswer != null)
                        {
                           myDictionary.Add(item, testTemplate.Settings.FormulaForOneAnswer.ToString()); 
                        }
                        else
                        {
                            myDictionary.Add(item, "formula_for_one_answer_key has not been set");
                        }

                        break;

                    case "{settings_formula_for_multiple_answer_key}":

                        if (testTemplate.Settings.FormulaForMultipleAnswers != null)
                        {
                           myDictionary.Add(item, testTemplate.Settings.FormulaForMultipleAnswers.ToString()); 
                        }
                        else
                        {
                            myDictionary.Add(item, "formula_for_multiple_answer_key has not been set");
                        }

                        break;

                    case "{status_key}":

                        myDictionary.Add(item, testTemplate.Status.ToString());

                        break;

                    case "{mode_key}":

                        myDictionary.Add(item, testTemplate.Mode.ToString());

                        break;

                    case "{categories_sequence_key}":

                        myDictionary.Add(item, testTemplate.CategoriesSequence.ToString());

                        break;
                }
            }

            return myDictionary;
        }
    }
}
