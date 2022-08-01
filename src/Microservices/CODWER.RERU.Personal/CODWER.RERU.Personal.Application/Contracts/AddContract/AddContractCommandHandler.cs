using AutoMapper;
using CODWER.RERU.Personal.Application.TemplateParsers;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Employers;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using FileTypeEnum = CVU.ERP.StorageService.Entities.FileTypeEnum;

namespace CODWER.RERU.Personal.Application.Contracts.AddContract
{
    public class AddContractCommandHandler : IRequestHandler<AddContractCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly IPersonalStorageClient _personalStorageClient;
        private readonly EmployerData _employerData;

        public AddContractCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ITemplateConvertor templateConvertor, 
            IOptions<EmployerData> options, 
            IStorageFileService storageFileService, 
            IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _personalStorageClient = personalStorageClient;
            _employerData = options.Value;
        }

        public async Task<int> Handle(AddContractCommand request, CancellationToken cancellationToken)
        {
            var contract = _mapper.Map<IndividualContract>(request.Data);
            var firstInstruction = _mapper.Map<Instruction>(request.Data.Instruction);

            contract.No = (_appDbContext.Contracts.Count() + 1).ToString("000");
            contract.FileId = await SaveFile(request.Data.ContractorId, contract);

            await _appDbContext.Contracts.AddAsync(contract);
            await _appDbContext.Instructions.AddAsync(firstInstruction);
            
            await _appDbContext.SaveChangesAsync();

            return contract.Id;
        }

        private async Task<string> SaveFile(int contractorId, IndividualContract contract)
        {
            var myDictionary = await GetMyDictionary(contractorId, contract);
            var fileName = "ContractorTemplates/Contract Individual De Munca.html";

            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, fileName);

            var fileId = await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.cim,
                parsedPdf.ContentType,
                parsedPdf.Content);

            await _personalStorageClient.AddFileToContractor(contractorId, fileId);

            return fileId;
        }

        private async Task<Dictionary<string, string>> GetMyDictionary(int contractorId, IndividualContract contract)
        {
            var myDictionary = new Dictionary<string, string>();

            var contractor = _appDbContext.Contractors
                .Include(x => x.UserProfile)
                .Include(x=>x.Bulletin)
                    .ThenInclude(x => x.ResidenceAddress)
                .Include(x => x.Positions)
                    .ThenInclude(x => x.Role)
                .First(x => x.Id == contractorId);

            var currency = _appDbContext.NomenclatureRecords.FirstOrDefault(x => x.Id == contract.CurrencyTypeId)?.Name;
            var bulletin = contractor.UserProfile.Contractor.Bulletin;
            var position = contractor.GetLastPosition();
            var address = bulletin.ResidenceAddress;

            myDictionary.Add("{nr_contract_replace}", contract.No);

            myDictionary.Add("{day_replace}", DateTime.Now.ToString("dd"));
            myDictionary.Add("{month_replace}", DateTime.Now.ToString("MM"));
            myDictionary.Add("{year_replace}", DateTime.Now.ToString("yyyy"));

            myDictionary.Add("{salariatName}", contractor.FirstName);
            myDictionary.Add("{salariatLastName}", contractor.LastName);

            myDictionary.Add("{functia_replace}", position.Role.Name);
            myDictionary.Add("{data_contract_replace}", position.GeneratedDate?.ToString("dd/MM/yyyy"));

            myDictionary.Add("{locul_de_munca_replace}", position.WorkPlace);

            myDictionary.Add("{salariu_replace}", contract.NetSalary.ToString());
            myDictionary.Add("{valuta_replace}", currency);

            myDictionary.Add("{WorkHoursEnum}", ((int)position.WorkHours).ToString());
            myDictionary.Add("{perioada_de_proba_replace}", position.ProbationDayPeriod.ToString());

            myDictionary.Add("{nr_buletin_replace}", bulletin.Series);

            myDictionary.Add("{eliberat_buletin_replace}", bulletin.EmittedBy);
            myDictionary.Add("{buletin_data_replace}", bulletin.ReleaseDay.ToString("dd/MM/yyyy"));

            myDictionary.Add("{idnp_replace}", bulletin.Contractor.Idnp);

            myDictionary.Add("{adresa_replace}", $"{address.City}, {address.Street}");

            //company data
            myDictionary.Add("{city_replace}", _employerData.City);
            myDictionary.Add("{company_replace}", _employerData.Name);
            myDictionary.Add("{minister_srl_replace}", _employerData.Type);

            myDictionary.Add("{directorName}", _employerData.DirectorName);
            myDictionary.Add("{directorLastName}", _employerData.DirectorLastName);
            myDictionary.Add("{postCode_replace}", _employerData.PostCode);
            myDictionary.Add("{street_adress_replace}", _employerData.Address);
            myDictionary.Add("{idno_replace}", _employerData.Idno);

            return myDictionary;
        }
    }
}
