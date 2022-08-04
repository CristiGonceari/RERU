using CVU.ERP.Common.DataTransferObjects.Files;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVU.ERP.StorageService.Entities;
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

                        var date = DateTime.Now;

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

                    case "{cheie_cu_numarul_total_de_întrebări}":

                        myDictionary.Add(item, testTemplate.QuestionCount.ToString());

                        break;

                    case "{cheie_minim_punctaj}":

                        myDictionary.Add(item, testTemplate.MinPercent.ToString() + " %");

                        break;

                    case "{cheie_de_durată}":

                        myDictionary.Add(item, testTemplate.Duration.ToString() + " min");

                        break;

                    case "{cheie_cu_numarul_de_maxim_posibile_erori}":
                       
                        myDictionary.Add(item, ValidateItemsForDictionary(item, testTemplate.Settings?.MaxErrors.ToString()));

                        break;

                    case "{cheie_cu_formula_pentru_un_singur_raspuns}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, testTemplate.Settings?.FormulaForOneAnswer.ToString()));

                        break;

                    case "{cheie_cu_formula_pentru_răspunsuri_multiple}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, testTemplate.Settings?.FormulaForMultipleAnswers.ToString()));

                        break;

                    case "{cheie_cu_statutul_șablonului}":

                        myDictionary.Add(item, testTemplate.Status.ToString());

                        break;

                    case "{cheie_modul_șablonului}":

                        myDictionary.Add(item, testTemplate.Mode.ToString());

                        break;

                    case "{cheie_cu_ordinea_întrebărilor_in_test}":

                        myDictionary.Add(item, testTemplate.CategoriesSequence.ToString());

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

                return finalKeyName + " nu a fost setata";
            }
        }
    }
}
