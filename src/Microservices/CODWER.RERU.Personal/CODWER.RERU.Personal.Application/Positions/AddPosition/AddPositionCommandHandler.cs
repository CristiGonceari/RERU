using AutoMapper;
using CODWER.RERU.Personal.Application.TemplateParsers;
using RERU.Data.Persistence.Context;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using FileTypeEnum = CVU.ERP.StorageService.Entities.FileTypeEnum;

namespace CODWER.RERU.Personal.Application.Positions.AddPosition
{
    public class AddPositionCommandHandler : IRequestHandler<AddPositionCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly IPersonalStorageClient _personalStorageClient;
        private readonly string _fileName;

        public AddPositionCommandHandler(AppDbContext appDbContext, IMapper mapper, ITemplateConvertor templateConvertor, IStorageFileService storageFileService, IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _personalStorageClient = personalStorageClient;
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

            var fileId = await _storageFileService.AddFile(
                parsedPdf.Name,
                FileTypeEnum.order,
                parsedPdf.ContentType,
                parsedPdf.Content);

            await _personalStorageClient.AddFileToContractor(request.Data.ContractorId, fileId);

            return fileId;
        }

        private async Task<Dictionary<string, string>> GetMyDictionary(int contractorId, Position position)
        {
            var contractor = await _appDbContext.Contractors
                .FirstAsync(x => x.Id == contractorId);

            var Role = await _appDbContext.Roles
                .FirstAsync(x => x.Id == position.RoleId);

            var myDictionary = new Dictionary<string, string>();

            var sexType = contractor.Sex == SexTypeEnum.Male ? "domnului" : "doamnei";

            myDictionary.Add("{ordin_replace}", position.No);
            myDictionary.Add("{data_today_replace}", DateTime.Now.ToString("dd/MM/yyyy"));
            myDictionary.Add("{cererea_replace}",  $"{sexType} {contractor.FirstName} {contractor.LastName}");
            myDictionary.Add("{name_replace}", contractor.FirstName);
            myDictionary.Add("{lastName_replace}", contractor.LastName);
            myDictionary.Add("{functia_replace}", Role.Name);
            myDictionary.Add("{termen_de_proba_replace}", position.ProbationDayPeriod.ToString());
            myDictionary.Add("{data_replace}", position.FromDate?.ToString("dd/MM/yyyy"));

            return myDictionary;
        }
    }
}

