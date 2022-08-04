using CODWER.RERU.Personal.Application.Services.VacationInterval;
using CODWER.RERU.Personal.Application.TemplateParsers;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Employers;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class DismissalTemplateParserService : IDismissalTemplateParserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly IVacationIntervalService _vacationIntervalService;
        private readonly IPersonalStorageClient _personalStorageClient;
        private readonly EmployerData _employerData;
        private readonly string _fileNameRequest;
        private readonly string _fileNameOrder;

        public DismissalTemplateParserService(AppDbContext appDbContext,
            ITemplateConvertor templateConvertor,
            IStorageFileService storageFileService,
            IVacationIntervalService vacationIntervalService,
            IOptions<EmployerData> options,
            IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _vacationIntervalService = vacationIntervalService;
            _personalStorageClient = personalStorageClient;
            _employerData = options.Value;

            _fileNameRequest = "ContractorTemplates/Requests/Cerere Cu Privire La Demisionare.html";
            _fileNameOrder = "ContractorTemplates/Orders/Ordin Cu Privire La Demisionare.html";
        }
        public async Task<string> SaveRequestFile(int contractorId, DateTime from)
        {
            var myDictionary = await GetRequestDictionary(contractorId, from);
            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, _fileNameRequest);

            var fileId = await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.request,
                parsedPdf.ContentType,
                parsedPdf.Content);

            await _personalStorageClient.AddFileToContractor(contractorId, fileId);

            return fileId;
        }

        public async Task<string> SaveOrderFile(int contractorId, DateTime from)
        {
            var myDictionary = await GetOrderDictionary(contractorId, from);
            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, _fileNameOrder);

            var fileId = await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.order,
                parsedPdf.ContentType,
                parsedPdf.Content);

            await _personalStorageClient.AddFileToContractor(contractorId, fileId);

            return fileId;
        }

        private async Task<Dictionary<string, string>> GetRequestDictionary(int contractorId, DateTime from)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Positions)
                .ThenInclude(x => x.Role)
                .FirstAsync(x => x.Id == contractorId);

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{company_replace}", _employerData.Name);

            myDictionary.Add("{directorName}", _employerData.DirectorName);
            myDictionary.Add("{directorLastName}", _employerData.DirectorLastName);

            myDictionary.Add("{nume_replace}", contractor.LastName);
            myDictionary.Add("{prenume_replace}", contractor.FirstName);

            myDictionary.Add("{functia_replace}", contractor.GetCurrentPositionOnData(DateTime.Now)?.Role.Name);

            myDictionary.Add("{data_demisia_replace}", from.ToString("dd/MM/yyyy"));
            myDictionary.Add("{data_replace}", DateTime.Now.ToString("dd/MM/yyyy"));

            return myDictionary;
        }

        private async Task<Dictionary<string, string>> GetOrderDictionary(int contractorId, DateTime from)
        {
            var contractor = await _appDbContext.Contractors
              .Include(x => x.Positions)
              .ThenInclude(x => x.Role)
              .FirstAsync(x => x.Id == contractorId);

            var position = contractor.GetCurrentPositionOnData(DateTime.Now);

            var nonUsedVacationDays = $"{await _vacationIntervalService.GetContractorAvailableDays(contractorId):0.0}";

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{data_replace}", DateTime.Now.ToString("dd/MM/yyyy"));
            myDictionary.Add("{nr_replace}", (_appDbContext.DismissalRequests.Count(x => x.Status == StageStatusEnum.Approved) + 1).ToString("000"));

            myDictionary.Add("{sex_type_replace}", contractor.Sex == SexTypeEnum.Male ? "Domnul" : "Doamna");

            myDictionary.Add("{nume_replace}", contractor.LastName);
            myDictionary.Add("{prenume_replace}", contractor.FirstName);

            myDictionary.Add("{functia_replace}", position?.Role.Name);

            myDictionary.Add("{data_demisia_replace}", from.ToString("dd/MM/yyyy"));

            myDictionary.Add("{concediu_neutilizat}", nonUsedVacationDays);

            return myDictionary;
        }
    }
}
