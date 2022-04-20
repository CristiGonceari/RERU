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
    public class GetTestDocumentReplacedKeys : IGetTestDocumentReplacedKeys
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;

        public GetTestDocumentReplacedKeys(AppDbContext appDbContext, IGeneratePdf generatePdf)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
        }

        public async Task<string> GetTestDocumentReplacedKey(Test test, int documentTemplateId)
        {
            var documentTemplate = _appDbContext.DocumentTemplates.FirstOrDefault(dt => dt.Id == documentTemplateId);

            return await ReplaceTestDocumentKeys(documentTemplate.Value, test);
        }

        public async Task<FileDataDto> GetPdf(string source, string testName)
        {
            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = testName
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

        public async Task<string> ReplaceTestDocumentKeys(string source, Test test)
        {
            var myDictionary = await GetDictionay(test);

            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }

            return source;
        }

        public async Task<Dictionary<string, string>> GetDictionay(Test test)
        {
            var keys = _appDbContext.DocumentTemplateKeys
                                        .Where(dtk => dtk.FileType == FileTypeEnum.Test)
                                        .Select(dtk => dtk.KeyName)
                                        .ToList();

            var myDictionary = new Dictionary<string, string>();

            foreach (var item in keys)
            {
                switch (item)
                {
                    case "{cheie_pentru_data_de_azi}":

                        var date = DateTime.Now;

                        myDictionary.Add(item, date.ToString("dd/MM/yyyy").Replace("-", "/"));

                        break;

                    case "{cheie_cu_punctaj_acumulat}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.AccumulatedPercentage.ToString()));

                        break;

                    case "{cheie_cu_maxim_posibile_erori}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.MaxErrors.ToString()));

                        break;

                    case "{cheie_cu_statutul_testului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.TestPassStatus.ToString()));

                        break;

                    case "{cheie_cu_rezultatul_testului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.ResultStatus.ToString()));

                        break;

                    case "{cheie_cu_data_programata_a_testului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.ProgrammedTime.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{cheie_cu_data_de_început_a_testului}":

                        var startTime = test.StartTime;

                         myDictionary.Add(item, ValidateItemsForDictionary(item, startTime?.ToString("dd/MM/yyyy").Replace("-", "/")) ); 

                        break;

                    case "{cheie_cu_data_de_încheiere_a_testului}":

                        var endTime = test.EndTime;
                        
                        myDictionary.Add(item, ValidateItemsForDictionary(item, endTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{cheie_cu_numele_evenimentului}":

                         myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Name));

                        break;

                    case "{cheie_cu_descrierea_evenimentului}":
                        
                         myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Description));
                       
                        break;

                    case "{cheie_cu_data_de_început_a_evenimentului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.FromDate.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{cheie_cu_data_de_încheiere_a_evenimentului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.TillDate.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{cheie_cu_numele_locatiei}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Name));

                        break;

                    case "{cheie_cu_descrierea_locatiei}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Description));

                        break;

                    case "{cheie_cu_adresa_locatiei}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Address));

                        break;

                    case "{cheie_cu_tipul_locatiei}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Type.ToString()));

                        break;

                    case "{cheie_cu_numarul_de_locuri_ale_locatiei}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Places.ToString()));

                        break;

                    case "{cheie_cu_numele_evaluatului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.FirstName));

                        break;

                    case "{cheie_cu_prenumele_evaluatului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.LastName));

                        break;

                    case "{cheie_cu_patronimicul_evaluatului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.FatherName));

                        break;

                    case "{cheie_cu_IDNP_evaluatului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.Idnp));

                        break;

                    case "{cheie_cu_email_evaluatului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.Email));

                        break;

                    case "{cheie_cu_numele_evaluatorului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FirstName));

                        break;

                    case "{cheie_cu_prenumele_evaluatorului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.LastName));

                        break;

                    case "{cheie_cu_patronimicul_evaluatorului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FirstName));

                        break;

                    case "{cheie_cu_IDNP_evaluatorului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Idnp));

                        break;

                    case "{cheie_cu_email_evaluatorului}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Email));

                        break;
                }
            }

            return myDictionary;
        }

        private string ValidateItemsForDictionary(string keyName, string testValue)
        {

            if (testValue != null)
            {
                return testValue;
            }
            else 
            {
                var finalKeyName = keyName.Substring(1, keyName.Length - 2);

                return finalKeyName + " has not been set";
            }
        }
    }
}
