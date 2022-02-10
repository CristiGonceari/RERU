using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.AddPosition
{
    public class AddPositionCommandHandler : IRequestHandler<AddPositionCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly string _fileName;

        public AddPositionCommandHandler(AppDbContext appDbContext, IMapper mapper, ITemplateConvertor templateConvertor, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _fileName = "ContractorTemplates/Orders/Ordin Cu Privire La Angajare.html";
        }

        public async Task<int> Handle(AddPositionCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Position>(request.Data);

            item.No = (_appDbContext.Positions.Count() + 1).ToString("000");
            item.OrderId = await SaveFile(request, item);

            await _appDbContext.Positions.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }

        private async Task<string> SaveFile(AddPositionCommand request, Position item)
        {
            var myDictionary = await GetMyDictionary(request.Data.ContractorId, item);

            var parsedPdf = await _templateConvertor.GetPdfFromHtml(myDictionary, _fileName);

            return await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.Order,
                parsedPdf.ContentType,
                parsedPdf.Content);
        }

        private async Task<Dictionary<string, string>> GetMyDictionary(int contractorId, Position position)
        {
            var contractor = await _appDbContext.Contractors
                .FirstAsync(x => x.Id == contractorId);

            var organizationRole = await _appDbContext.OrganizationRoles
                .FirstAsync(x => x.Id == position.OrganizationRoleId);

            var myDictionary = new Dictionary<string, string>();

            var sexType = contractor.Sex == SexTypeEnum.Male ? "domnului" : "doamnei";

            myDictionary.Add("{ordin_replace}", position.No);
            myDictionary.Add("{data_today_replace}", DateTime.Now.ToString("dd/MM/yyyy"));
            myDictionary.Add("{cererea_replace}",  $"{sexType} {contractor.FirstName} {contractor.LastName}");
            myDictionary.Add("{name_replace}", contractor.FirstName);
            myDictionary.Add("{lastName_replace}", contractor.LastName);
            myDictionary.Add("{functia_replace}", organizationRole.Name);
            myDictionary.Add("{termen_de_proba_replace}", position.ProbationDayPeriod.ToString());
            myDictionary.Add("{data_replace}", position.FromDate?.ToString("dd/MM/yyyy"));

            return myDictionary;
        }
    }
}

