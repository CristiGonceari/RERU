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
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices.Implementations
{
    public class GetTestDocumentReplacedKeys : IGetTestDocumentReplacedKeys
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly IDateTime _dateTime;

        public GetTestDocumentReplacedKeys(AppDbContext appDbContext, IGeneratePdf generatePdf, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _dateTime = dateTime;
        }

        public async Task<string> GetTestDocumentReplacedKey(Test test, int documentTemplateId)
        {
            var documentTemplate = _appDbContext.DocumentTemplates.FirstOrDefault(dt => dt.Id == documentTemplateId);

            return await ReplaceTestDocumentKeys(documentTemplate.Value, test);
        }

        public async Task<FileDataDto> GetPdf(string source, string testName)
        {
            var res = Parse(source);

            return FileDataDto.GetPdf(testName, res);
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
                                        .Where(dtk => dtk.FileType == FileTypeEnum.test)
                                        .Select(dtk => dtk.KeyName)
                                        .ToList();

            var myDictionary = new Dictionary<string, string>();
            Location location;

            if (test.EventId != null)
            {

                location = _appDbContext.EventLocations
                                                    .Include(x => x.Location)
                                                    .Where(x => x.EventId == test.EventId)
                                                    .Select(x => x.Location)
                                                    .First();

                foreach (var item in keys)
                {
                    switch (item)
                    {
                        case "{cheie_pentru_data_de_azi}":

                            var date = _dateTime.Now;

                            myDictionary.Add(item, date.ToString("dd/MM/yyyy").Replace("-", "/"));

                            break;

                        case "{cheie_cu_punctaj_acumulat}":

                        if(test.AccumulatedPercentage == null)
                            {

                                myDictionary.Add(item, "- / " + test.TestTemplate.MinPercent + "%");
                            }
                            else
                            {
                                myDictionary.Add(item, test.AccumulatedPercentage + "% / " + test.TestTemplate.MinPercent + "%");
                            }

                            break;

                        case "{cheie_cu_numele_testului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.TestTemplate?.Name));

                            break;

                        case "{cheie_cu_statutul_testului}":

                            myDictionary.Add(item, EnumMessages.Translate(test.TestStatus));

                            break;

                        case "{cheie_cu_rezultatul_testului}":

                            myDictionary.Add(item, EnumMessages.Translate(test.ResultStatus));

                            break;

                        case "{cheie_cu_data_programata_a_testului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.ProgrammedTime.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_data_de_inceput_a_testului}":

                            var startTime = test.StartTime;

                            myDictionary.Add(item, ValidateItemsForDictionary(item, startTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_numele_evenimentului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Name));

                            break;

                        case "{cheie_cu_descrierea_evenimentului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Description));

                            break;

                        case "{cheie_cu_data_de_inceput_a_evenimentului}":

                            var eventStartTime = test.Event?.FromDate;

                            myDictionary.Add(item, ValidateItemsForDictionary(item, eventStartTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_data_de_incheiere_a_evenimentului}":

                            var eventEndTime = test.Event?.TillDate;

                            myDictionary.Add(item, ValidateItemsForDictionary(item, eventEndTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_numele_locatiei}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, location.Name));

                            break;

                        case "{cheie_cu_descrierea_locatiei}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, location.Description));

                            break;

                        case "{cheie_cu_adresa_locatiei}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, location.Address));

                            break;

                        case "{cheie_cu_numarul_de_locuri_ale_locatiei}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, location.Places.ToString()));

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

                        case "{cheie_cu_departament_rol_funcție_evaluat}":

                            string department = _appDbContext.Departments.Where(d => d.ColaboratorId == test.UserProfile.DepartmentColaboratorId).Select(d => d.Name).FirstOrDefault();
                            string role = _appDbContext.Roles.Where(r => r.ColaboratorId == test.UserProfile.RoleColaboratorId).Select(r => r.Name).FirstOrDefault();
                            string function = _appDbContext.EmployeeFunctions.Where(f => f.ColaboratorId == test.UserProfile.FunctionColaboratorId).Select(f => f.Name).FirstOrDefault();

                            myDictionary.Add(item, ValidateItemsForDictionary(item, department) + "/" + ValidateItemsForDictionary(item, role) + "/" + ValidateItemsForDictionary(item, function));

                            break;

                        case "{cheie_cu_numele_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FirstName));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_prenumele_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.LastName));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_patronimicul_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FatherName));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_IDNP_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Idnp));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_email_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Email));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;
                    }
                }

                return myDictionary;
            }
            else
            {
                location = null;
                foreach (var item in keys)
                {
                    switch (item)
                    {
                        case "{cheie_pentru_data_de_azi}":

                            var date = _dateTime.Now;

                            myDictionary.Add(item, date.ToString("dd/MM/yyyy").Replace("-", "/"));

                            break;

                        case "{cheie_cu_punctaj_acumulat}":

                            if (test.AccumulatedPercentage == null)
                            {

                                myDictionary.Add(item, "- / " + test.TestTemplate.MinPercent + "%");
                            }
                            else
                            {
                                myDictionary.Add(item, test.AccumulatedPercentage + "% / " + test.TestTemplate.MinPercent + "%");
                            }

                            break;

                        case "{cheie_cu_numele_testului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.TestTemplate?.Name));

                            break;

                        case "{cheie_cu_statutul_testului}":

                            myDictionary.Add(item, EnumMessages.Translate(test.TestStatus));

                            break;

                        case "{cheie_cu_rezultatul_testului}":

                            myDictionary.Add(item, EnumMessages.Translate(test.ResultStatus));

                            break;

                        case "{cheie_cu_data_programata_a_testului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.ProgrammedTime.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_data_de_inceput_a_testului}":

                            var startTime = test.StartTime;

                            myDictionary.Add(item, ValidateItemsForDictionary(item, startTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_numele_evenimentului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Name));

                            break;

                        case "{cheie_cu_descrierea_evenimentului}":

                            myDictionary.Add(item, ValidateItemsForDictionary(item, test.Event?.Description));

                            break;

                        case "{cheie_cu_data_de_inceput_a_evenimentului}":

                            var eventStartTime = test.Event?.FromDate;

                            myDictionary.Add(item, ValidateItemsForDictionary(item, eventStartTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_data_de_incheiere_a_evenimentului}":

                            var eventEndTime = test.Event?.TillDate;

                            myDictionary.Add(item, ValidateItemsForDictionary(item, eventEndTime?.ToString("dd/MM/yyyy").Replace("-", "/")));

                            break;

                        case "{cheie_cu_numele_locatiei}":

                            myDictionary.Add(item, LocationValidateItemsForDictionary(item));

                            break;

                        case "{cheie_cu_descrierea_locatiei}":

                            myDictionary.Add(item, LocationValidateItemsForDictionary(item));

                            break;

                        case "{cheie_cu_adresa_locatiei}":

                            myDictionary.Add(item, LocationValidateItemsForDictionary(item));

                            break;

                        case "{cheie_cu_numarul_de_locuri_ale_locatiei}":

                            myDictionary.Add(item, LocationValidateItemsForDictionary(item));

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

                        case "{cheie_cu_departament_rol_funcție_evaluat}":

                            string department = _appDbContext.Departments.Where(d => d.ColaboratorId == test.UserProfile.DepartmentColaboratorId).Select(d => d.Name).FirstOrDefault();
                            string role = _appDbContext.Roles.Where(r => r.ColaboratorId == test.UserProfile.RoleColaboratorId).Select(r => r.Name).FirstOrDefault();
                            string function = _appDbContext.EmployeeFunctions.Where(f => f.ColaboratorId == test.UserProfile.FunctionColaboratorId).Select(f => f.Name).FirstOrDefault();

                            myDictionary.Add(item, ValidateItemsForDictionary(item, department) + "/" + ValidateItemsForDictionary(item, role) + "/" + ValidateItemsForDictionary(item, function));

                            break;

                        case "{cheie_cu_numele_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FirstName));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_prenumele_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.LastName));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_patronimicul_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.FatherName));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_IDNP_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Idnp));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;

                        case "{cheie_cu_email_evaluatorului}":

                            if (test.EvaluatorId != null)
                            {
                                myDictionary.Add(item, ValidateItemsForDictionary(item, test.Evaluator?.Email));
                            }
                            else
                            {
                                myDictionary.Add(item, "Verificat de sistem");
                            }

                            break;
                    }
                }

                return myDictionary;
            }  
        }

        private string ValidateItemsForDictionary(string keyName, string testValue)
        {

            if (testValue != null)
            {
                return testValue;
            }

            var finalKeyName = keyName.Substring(1, keyName.Length - 2);

            return finalKeyName + " nu este setată";
        }

        private string LocationValidateItemsForDictionary(string keyName)
        {

            var finalKeyName = keyName.Substring(1, keyName.Length - 2);

            return finalKeyName + " nu este setată deoarece testul nu are locație";
        }
    }
}
