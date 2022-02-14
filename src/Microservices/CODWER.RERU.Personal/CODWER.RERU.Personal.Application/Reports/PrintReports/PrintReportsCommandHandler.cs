using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
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
        private readonly IPersonalStorageClient _personalStorageClient;

        public PrintReportsCommandHandler(AppDbContext appDbContext, 
            IGeneratePdf generatePdf, 
            IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _personalStorageClient = personalStorageClient;
        }

        public async Task<FileDataDto> Handle(PrintReportsCommand request, CancellationToken cancellationToken)
        {
            var contractorFiles = _appDbContext.ContractorFiles
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Positions)
                .ThenInclude(x => x.Department)
                .AsQueryable();

            contractorFiles = FilterContractors(contractorFiles, request);

            var files = await _personalStorageClient.GetContractorFiles(contractorFiles.Select(x => x.FileId).ToList());

            files = FilterFiles(files, request);

            return await GetPdf(request, contractorFiles.ToList(), files);
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

        private IQueryable<ContractorFile> FilterContractors(IQueryable<ContractorFile> contractorFiles, PrintReportsCommand request)
        {

            if (request.DepartmentId != null)
            {
                contractorFiles = contractorFiles.Where(x =>
                    x.Contractor.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                contractorFiles = contractorFiles.FilterOrdersByContractorName(request.ContractorName);
            }

            return contractorFiles;
        }

        private IQueryable<CVU.ERP.StorageService.Entities.File> FilterFiles(IQueryable<CVU.ERP.StorageService.Entities.File> files, PrintReportsCommand request)
        {
            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                files = files.Where(x => x.FileName.Contains(request.Name));
            }

            if (request.FromDate != null)
            {
                files = files.Where(x => x.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                files = files.Where(x => x.CreateDate <= request.ToDate);
            }

            return files;
        }

    }
}
