﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.ApproveRejectVacation
{
    public class ApproveRejectVacationCommandHandler : IRequestHandler<ApproveRejectVacationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly string _fileName;

        public ApproveRejectVacationCommandHandler(AppDbContext appDbContext, ITemplateConvertor templateConvertor, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _fileName = "ContractorTemplates/Orders/Ordin Cu Privire La Concediu.html";
        }

        public async Task<Unit> Handle(ApproveRejectVacationCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Vacations.FirstAsync(x => x.Id == request.Data.VacationId);

            if (request.Data.Approve)
            {
                item.Status = StageStatusEnum.Approved;
                item.VacationOrderId = await SaveFile(item.ContractorId, item.Id);
            }
            else
            {
                item.Status = StageStatusEnum.Rejected;
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<string> SaveFile(int contractorId, int vacationId)
        {
            var myDictionary = await GetMyDictionary(contractorId, vacationId);
            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, _fileName);

            return await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.Order,
                parsedPdf.ContentType,
                parsedPdf.Content);
        }

        private async Task<Dictionary<string, string>> GetMyDictionary(int contractorId, int vacationId)
        {
            var contractor = await _appDbContext.Contractors
                .FirstAsync(x => x.Id == contractorId);

            var vacation = await _appDbContext.Vacations
                .FirstAsync(x => x.Id == vacationId);

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{nr_replace}", (_appDbContext.Vacations.Count(x=>x.Status == StageStatusEnum.Approved) + 1).ToString("000"));
            myDictionary.Add("{data_replace}", DateTime.Now.ToString("dd/MM/yyyy"));

            myDictionary.Add("{nume_replace}", contractor.LastName);
            myDictionary.Add("{prenume_replace}", contractor.FirstName);

            myDictionary.Add("{tip_concediu_replace}", GetVacationType(vacation.VacationType));
            myDictionary.Add("{count_days_replace}", vacation.CountDays.ToString());

            myDictionary.Add("{from_replace}", vacation.FromDate.ToString("dd/MM/yyyy"));
            myDictionary.Add("{to_replace}", vacation.ToDate?.ToString("dd/MM/yyyy"));

            return myDictionary;
        }

        private string GetVacationType(VacationType type) =>
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
    }
}
