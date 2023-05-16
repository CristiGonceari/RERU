using CVU.ERP.Common.DataTransferObjects.Files;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVU.ERP.Common;
using CVU.ERP.StorageService.Entities;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices.Implementations
{
    public class GetTestTemplateDocumentReplacedKeys : IGetTestTemplateDocumentReplacedKeys
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly IDateTime _dateTime;

        public GetTestTemplateDocumentReplacedKeys(AppDbContext appDbContext, IGeneratePdf generatePdf, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _dateTime = dateTime;
        }

        public async Task<string> GetTestTemplateDocumentReplacedKey(TestTemplate testTemplate, int documentTemplateId)
        {
            var documentTemplate = _appDbContext.DocumentTemplates.FirstOrDefault(dt => dt.Id == documentTemplateId);

            return await ReplaceTestTemplateDocumentKeys(documentTemplate.Value, testTemplate);
        }

        public async Task<FileDataDto> GetPdf(string source, string testTemplateName)
        {
            var res = Parse(source);

            return FileDataDto.GetPdf(testTemplateName, res);
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
                                        .Where(dtk => dtk.FileType == FileTypeEnum.testtemplate)
                                        .Select(dtk => dtk.KeyName)
                                        .ToList();

            var myDictionary = new Dictionary<string, string>();

            foreach (var item in keys)
            {
                switch (item)
                {
                    case "{cheie_cu_data_de_azi}":

                        var date = _dateTime.Now;

                        myDictionary.Add(item, date.ToString("dd/MM/yyyy").Replace("-","/"));

                        break;

                    case "{cheia_numelui_șablonului_de_testare}":

                        myDictionary.Add(item, testTemplate.Name);

                        break;

                    case "{cheia_regulilor}":

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

                    case "{cheie_cu_numarul_total_de_intrebări}":

                        myDictionary.Add(item, testTemplate.QuestionCount.ToString());

                        break;

                    case "{cheia_numărului_de_categorii_șablonului_de_testare}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, testTemplate.TestTemplateQuestionCategories.Count.ToString()));

                        break;

                    case "{cheie_minim_punctaj}":

                        myDictionary.Add(item, testTemplate.MinPercent.ToString() + " %");

                        break;

                    case "{cheie_de_durată}":

                        myDictionary.Add(item, testTemplate.Duration.ToString() + " min");

                        break;

                    case "{cheie_cu_numarul_de_maxim_posibile_erori}":

                        if(testTemplate.Rules != null)
                            {
                                myDictionary.Add(item, testTemplate.Settings?.MaxErrors.ToString());
                            }
                            else
                            {
                                myDictionary.Add(item, "-");
                            }

                        break;

                    case "{cheie_cu_formula_pentru_un_singur_raspuns}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, EnumMessages.Translate(testTemplate.Settings?.FormulaForOneAnswer)));

                        break;

                    case "{cheie_cu_formula_pentru_răspunsuri_multiple}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, EnumMessages.Translate(testTemplate.Settings?.FormulaForMultipleAnswers)));

                        break;

                    case "{cheie_cu_statutul_șablonului}":

                        myDictionary.Add(item, EnumMessages.Translate(testTemplate.Status));

                        break;

                    case "{cheie_tipul_șablonului}":

                        myDictionary.Add(item, EnumMessages.Translate(testTemplate.Mode));

                        break;

                    case "{cheie_cu_ordinea_intrebărilor_in_test}":

                        myDictionary.Add(item, testTemplate.CategoriesSequence.ToString());

                        break;

                    case "{cheie_cu_șablon_de_bază}":

                        myDictionary.Add(item, EnumMessages.Translate(testTemplate.BasicTestTemplate));

                        break;

                    case "{cheie_cu_tipul_de_calificare}":

                        myDictionary.Add(item, EnumMessages.Translate(testTemplate.QualifyingType));

                        break;
                }
            }

            return myDictionary;
        }

        private string ValidateItemsForDictionary(string keyName, string testTemplateValue)
        {

            if (testTemplateValue != null)
            {
                return testTemplateValue;
            }
            else
            {
                var finalKeyName = keyName.Substring(1, keyName.Length - 2);

                return finalKeyName + " nu a fost setată";
            }
        }
    }
}
