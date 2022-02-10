using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;
using File = System.IO.File;

namespace CODWER.RERU.Personal.Application.Reports.PrintReports
{
    public class PrintReportsCommandHandler : IRequestHandler<PrintReportsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly StorageDbContext _storageDbContext;

        public PrintReportsCommandHandler(AppDbContext appDbContext, IGeneratePdf generatePdf, StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _storageDbContext = storageDbContext;
        }

        public async Task<FileDataDto> Handle(PrintReportsCommand request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.ContractorFiles
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Positions)
                .ThenInclude(x => x.Department)
                .AsQueryable();

            var files = _storageDbContext.Files
                .Where(x => x.FileType == FileTypeEnum.IdentityFiles &&
                            items.Any(i => i.FileId == x.Id.ToString()))
                .Select(f => new CVU.ERP.StorageService.Entities.File
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    Type = f.Type,
                    FileType = f.FileType
                })
                .AsQueryable();

            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                files = files.Where(x => x.FileName.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                items = items.FilterOrdersByContractorName(request.ContractorName);
            }

            if (request.DepartmentId != null)
            {
                items = items.Where(x =>
                    x.Contractor.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            if (request.FromDate != null)
            {
                files = files.Where(x => x.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                files = files.Where(x => x.CreateDate <= request.ToDate);
            }

            return await GetPdf(request, items.ToList(), files);
        }

        private string GetTableContent(List<ContractorFile> contractorFiles, IQueryable<CVU.ERP.StorageService.Entities.File> files)
        {
            var content = string.Empty;


            foreach (var item in contractorFiles)
            {
                var file = files.FirstOrDefault(x => x.Id.ToString() == item.FileId);

                content += $@"
                          <tr>
                            <td>{file.FileName}</td>
                            <td>{item.Contractor.LastName} {item.Contractor.FirstName} {item.Contractor.FatherName}</td>
                            <td>{file.CreateDate:dd/MM/yyyy}</td>
                            <td>{file.Type}</td>
                          </tr>";
            }

            return content;
        }

        private string GetFilters(PrintReportsCommand request)
        {
            var filters = string.Empty;

            if (request.FileType != null)
            {
                filters += $"<p>Tipul selectat: {request.FileType}</p>";
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                filters += $"<p>Denumire document: {request.Name}</p>";
            }

            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                filters += $"<p>N.P.P. angajat: {request.ContractorName}</p>";
            }

            if (request.FromDate != null)
            {
                filters += $"<p>De la: {request.FromDate:dd/MM/yyyy}</p>";
            }

            if (request.ToDate != null)
            {
                filters += $"<p>Pana la: {request.ToDate:dd/MM/yyyy}</p>";
            }

            return filters;
        }

        public async Task<FileDataDto> GetPdf(PrintReportsCommand request, List<ContractorFile> contractorFiles, IQueryable<CVU.ERP.StorageService.Entities.File> files)
        {
            byte[] res;
            var path = new FileInfo("ContractorTemplates/Reports/Report List.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            source = source.Replace("{filters_area_replace}", GetFilters(request));
            source = source.Replace("{tr_area_replace}", GetTableContent(contractorFiles, files));

            try
            {
                res = _generatePdf.GetPDF(source);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Report_List.pdf"
            };
        }
    }
}
