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
                    case "{today_date_key}":

                        var date = DateTime.Now;

                        myDictionary.Add(item, date.ToString("dd/MM/yyyy").Replace("-", "/"));

                        break;

                    case "{accumulated_percentage_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.AccumulatedPercentage.ToString()));

                        break;

                    case "{max_errors_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.MaxErrors.ToString()));

                        break;

                    case "{test_pass_status_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.TestPassStatus.ToString()));

                        break;

                    case "{result_status_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.ResultStatus.ToString()));

                        break;

                    case "{programmed_time_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.ProgrammedTime.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{start_time_key}":

                        var startTime = test.StartTime;

                         myDictionary.Add(item, ValidateItemsForDictionary(item, startTime?.ToString("dd/MM/yyyy").Replace("-", "/")) ); 

                        break;

                    case "{end_time_key}":

                        var endTime = test.EndTime;
                        
                        myDictionary.Add(item, ValidateItemsForDictionary(item, endTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{event_name_key}":

                         myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Name));

                        break;

                    case "{event_description_key}":
                        
                         myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Description));
                       
                        break;

                    case "{event_from_date_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.FromDate.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{event_till_date_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.TillDate.ToString("dd/MM/yyyy").Replace("-", "/")));

                        break;

                    case "{location_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Name));

                        break;

                    case "{location_description_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Description));

                        break;

                    case "{location_address_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Address));

                        break;

                    case "{location_type_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Type.ToString()));

                        break;

                    case "{location_places_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Location?.Places.ToString()));

                        break;

                    case "{appraiser_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.FirstName));

                        break;

                    case "{appraiser_last_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.LastName));

                        break;

                    case "{appraiser_father_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.FatherName));

                        break;

                    case "{appraiser_idnp_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.Idnp));

                        break;

                    case "{appraiser_email_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.UserProfile?.Email));

                        break;

                    case "{evaluator_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FirstName));

                        break;

                    case "{evaluator_last_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.LastName));

                        break;

                    case "{evaluator_father_name_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FirstName));

                        break;

                    case "{evaluator_idnp_key}":

                        myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Idnp));

                        break;

                    case "{evaluator_email_key}":

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
