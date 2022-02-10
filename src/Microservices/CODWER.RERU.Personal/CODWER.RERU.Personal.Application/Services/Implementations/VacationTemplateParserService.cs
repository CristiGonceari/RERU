using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Employers;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class VacationTemplateParserService : IVacationTemplateParserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly EmployerData _employerData;
        private readonly string _fileName;

        public VacationTemplateParserService(
            AppDbContext appDbContext, 
            ITemplateConvertor templateConvertor, 
            IStorageFileService storageFileService, 
            IOptions<EmployerData> options)
        {
            _appDbContext = appDbContext;
            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _employerData = options.Value;
            _fileName = "ContractorTemplates/Orders/Ordin Cu Privire La Concediu.html";
        }

        #region Request
        public async Task<string> SaveRequestFile(int contractorId, Vacation vacation)
        {
            var myDictionary = await GetRequestDictionary(contractorId, vacation);
            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, GetFullRequestTemplateName(vacation.VacationType));

            var fileId = await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.request,
                parsedPdf.ContentType,
                parsedPdf.Content);

            var contractorFile = new ContractorFile
            {
                ContractorId = contractorId,
                FileId = fileId
            };

            await _appDbContext.ContractorFiles.AddAsync(contractorFile);
            await _appDbContext.SaveChangesAsync();

            return fileId;
        }

        private async Task<Dictionary<string, string>> GetRequestDictionary(int contractorId, Vacation vacation)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Positions)
                .ThenInclude(x => x.OrganizationRole)
                .FirstAsync(x => x.Id == contractorId);

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{directorName}", _employerData.DirectorName);
            myDictionary.Add("{directorLastName}", _employerData.DirectorLastName);

            myDictionary.Add("{nume_replace}", contractor.LastName);
            myDictionary.Add("{prenume_replace}", contractor.FirstName);

            myDictionary.Add("{company_replace}", _employerData.Name);

            myDictionary.Add("{functia_replace}", contractor.GetCurrentPositionOnData(DateTime.Now)?.OrganizationRole.Name);

            myDictionary.Add("{from_replace}", vacation.FromDate.ToString("dd/MM/yyyy"));
            myDictionary.Add("{to_replace}", vacation.ToDate != null ? vacation.ToDate?.ToString("dd/MM/yyyy") : vacation.FromDate.ToString("dd/MM/yyyy"));

            myDictionary.Add("{data_replace}", DateTime.Now.ToString("dd/MM/yyyy"));

            myDictionary.Add("{count_days_replace}", vacation.CountDays.ToString());
            myDictionary.Add("{age_replace}", vacation.ChildAge.ToString());
            myDictionary.Add("{institution_replace}", vacation.Institution);

            return myDictionary;
        }

        private string GetFullRequestTemplateName(VacationType type) =>
            type switch
            {
                VacationType.PaidAnnual => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu Anual Platit.html",
                VacationType.Studies => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu de Studii.html",
                VacationType.Death => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu de Deces.html",
                VacationType.ChildCare => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu Ingrijirea Copilului.html",
                VacationType.BirthOfTheChild => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu Pentru Nasterea Copilului.html",
                VacationType.Marriage => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu de Casatorie.html",
                VacationType.Paternal => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu de Paternitate.html",
                VacationType.OwnVacation => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu Cont Propriu.html",

                _ => "ContractorTemplates/VacationsRequests/Cerere de acordare Concediu Anual Platit.html"
            };
        #endregion

        #region Order
        public async Task<string> SaveOrderFile(int contractorId, Vacation vacation)
        {
            var myDictionary = await GetOrderDictionary(contractorId, vacation);
            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, _fileName);

            var fileId = await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.order,
                parsedPdf.ContentType,
                parsedPdf.Content);

            var contractorFile = new ContractorFile
            {
                ContractorId = contractorId,
                FileId = fileId
            };

            await _appDbContext.ContractorFiles.AddAsync(contractorFile);
            await _appDbContext.SaveChangesAsync();

            return fileId;
        }

        private async Task<Dictionary<string, string>> GetOrderDictionary(int contractorId, Vacation vacation)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Positions)
                .ThenInclude(x => x.OrganizationRole)
                .FirstAsync(x => x.Id == contractorId);

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{nr_replace}", (_appDbContext.Vacations.Count(x => x.Status == StageStatusEnum.Approved) + 1).ToString("000"));
            myDictionary.Add("{data_replace}", DateTime.Now.ToString("dd/MM/yyyy"));

            myDictionary.Add("{nume_replace}", contractor.LastName);
            myDictionary.Add("{prenume_replace}", contractor.FirstName);

            myDictionary.Add("{tip_concediu_replace}", GetVacationOrderType(vacation.VacationType));
            myDictionary.Add("{count_days_replace}", vacation.CountDays.ToString());

            myDictionary.Add("{from_replace}", vacation.FromDate.ToString("dd/MM/yyyy"));
            myDictionary.Add("{to_replace}", vacation.ToDate?.ToString("dd/MM/yyyy"));

            return myDictionary;
        }

        private string GetVacationOrderType(VacationType type) =>
            type switch
            {
                VacationType.PaidAnnual => "Concediu Anual Platit",
                VacationType.Studies => "Concediu de Studii",
                VacationType.Death => "Concediu de Deces",
                VacationType.ChildCare => "Concediu Ingrijirea Copilului",
                VacationType.BirthOfTheChild => "Concediu Pentru Nasterea Copilului",
                VacationType.Marriage => "Concediu de Casatorie",
                VacationType.Paternal => "Concediu de Paternitate",
                VacationType.OwnVacation => "Concediu Cont Propriu",

                _ => "Concediu Anual Platit"
            };

        #endregion
    }
}
