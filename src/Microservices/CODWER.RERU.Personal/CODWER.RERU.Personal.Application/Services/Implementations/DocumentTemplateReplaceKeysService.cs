using CODWER.RERU.Personal.Application.Services.VacationInterval;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Common;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class DocumentTemplateReplaceKeysService : IDocumentTemplateReplaceKeysService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;
        private readonly IOptions<EmployerData> config;


        public DocumentTemplateReplaceKeysService(AppDbContext appDbContext, IOptions<EmployerData> config, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            this.config = config;
            _dateTime = dateTime;
        }
        public async Task<Dictionary<string, string>> GetContractorGeneralValues(int contractorId)
        {
            var contractorValues = _appDbContext.Contractors
                .Include(x => x.UserProfile)
                .Include(c => c.Bulletin)
                .ThenInclude(b => b.ResidenceAddress)
                .Where(c => c.Id == contractorId)
                .Include(c => c.Positions)
                .ThenInclude(c => c.Role)
                .Include(c => c.Positions)
                .ThenInclude(p => p.DismissalRequests)
                .Include(c => c.Contracts)
                .ToList();

            var result = SetContractorValuesToDictionary(contractorValues);

            return result;
        }

        public Dictionary<string, string> SetContractorValuesToDictionary(List<Contractor> contractorValues)
        {

            var keys = _appDbContext.DocumentTemplateKeys.Select(dk => dk.KeyName).ToList();

            var myDictionary = new Dictionary<string, string>();


            foreach (var value in contractorValues)
            {
                foreach (var item in keys)
                {
                    switch (item)
                    {
                        case "{today_date_key}":

                            myDictionary.Add(item, _dateTime.Now.ToString());
                            break;

                        case "{c_name_key}":
                            myDictionary.Add(item, value.FirstName);
                            break;

                        case "{c_last_name_key}":

                            myDictionary.Add(item, value.LastName);
                            break;

                        case "{c_father_name_key}":

                            myDictionary.Add(item, value.FatherName);
                            break;
                        case "{c_idnp_key}":

                            myDictionary.Add(item, value.UserProfile.Idnp);
                            break;

                        case "{c_bulletin_series_key}":
                            myDictionary.Add(item, value.UserProfile.Contractor.Bulletin.Series);
                            break;

                        case "{c_bulletin_release_by_key}":
                            myDictionary.Add(item, value.UserProfile.Contractor.Bulletin.EmittedBy);
                            break;

                        case "{c_bulletin_release_day_key}":
                            myDictionary.Add(item, value.UserProfile.Contractor.Bulletin.ReleaseDay.ToString());
                            break;

                        case "{c_birthday_key}":
                            myDictionary.Add(item, value.BirthDate.ToString());
                            break;

                        case "{c_work_place_key}":

                            myDictionary.Add(item, value.Positions.FirstOrDefault().WorkPlace);
                            break;

                        case "{c_employment_date_key}":

                            myDictionary.Add(item, value.Positions.FirstOrDefault().FromDate.ToString());
                            break;

                        case "{c_work_hours_key}":
                            myDictionary.Add(item, value.Positions.FirstOrDefault().WorkHours.ToString());
                            break;

                        case "{c_salary_key}":
                            myDictionary.Add(item, value.Contracts.FirstOrDefault().NetSalary.ToString());
                            break;

                        case "{c_sex_type_key}":
                            myDictionary.Add(item, value.Sex.ToString());
                            break;

                        case "{c_role_key}":
                            myDictionary.Add(item, value.Positions.Select(p => p.Role).FirstOrDefault().Name);
                            break;

                        case "{c_dissmisal_date_key}":
                            myDictionary.Add(item, value.Positions.Select(p => p.DismissalRequests.Select(dr => dr.From).FirstOrDefault()).FirstOrDefault().ToString());
                            break;

                        case "{c_internship_days_key}":
                            myDictionary.Add(item, value.Positions.FirstOrDefault().ProbationDayPeriod.ToString());
                            break;

                        case "{c_address_key}":
                            myDictionary.Add(item, value.UserProfile.Contractor.Bulletin.ResidenceAddress.Street);
                            break;

                        case "{company_key}":
                            myDictionary.Add(item, this.config.Value.Name);
                            break;

                        case "{company_post_code_key}":
                            myDictionary.Add(item, this.config.Value.PostCode);
                            break;

                        case "{company_city_key}":
                            myDictionary.Add(item, this.config.Value.City);
                            break;

                        case "{company_street_key}":
                            myDictionary.Add(item, this.config.Value.Address);
                            break;

                        case "{company_idno_key}":
                            myDictionary.Add(item, this.config.Value.Idno);
                            break;

                        case "{director_name_key}":
                            myDictionary.Add(item, this.config.Value.DirectorName);
                            break;

                        case "{director_last_name_key}":
                            myDictionary.Add(item, this.config.Value.DirectorLastName);
                            break;

                        case "{minister_srl_key}":
                            myDictionary.Add(item, this.config.Value.Type);
                            break;
                    }
                }
            }

            return myDictionary;
        }
    }
}