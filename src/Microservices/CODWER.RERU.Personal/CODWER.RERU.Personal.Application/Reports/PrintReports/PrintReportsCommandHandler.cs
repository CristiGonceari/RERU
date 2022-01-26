using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Personal.Application.Reports.PrintReports
{
    public class PrintReportsCommandHandler : IRequestHandler<PrintReportsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;

        public PrintReportsCommandHandler(AppDbContext appDbContext, IGeneratePdf generatePdf)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
        }

        public async Task<FileDataDto> Handle(PrintReportsCommand request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.ByteFiles
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Positions)
                        .ThenInclude(x => x.Department)
                .Where(x => x.Type != FileTypeEnum.Identity)
                .AsQueryable();

            if (request.Type != null)
            {
                items = items.Where(x => x.Type == request.Type);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.FileName.Contains(request.Name));
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
                items = items.Where(x => x.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                items = items.Where(x => x.CreateDate <= request.ToDate);
            }

            return await GetPdf(request, items.ToList());
        }

        private string GetTableContent(List<ByteArrayFile> items)
        {
            var content = string.Empty;

            foreach (var item in items)
            {
                content += $@"
                          <tr>
                            <td>{item.FileName}</td>
                            <td>{item.Contractor.LastName} {item.Contractor.FirstName} {item.Contractor.FatherName}</td>
                            <td>{item.CreateDate:dd/MM/yyyy}</td>
                            <td>{item.Type}</td>
                          </tr>";
            }

            return content;
        }

        private string GetFilters(PrintReportsCommand request)
        {
            var filters = string.Empty;

            if (request.Type != null)
            {
                filters += $"<p>Tipul selectat: {request.Type}</p>";
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

        public async Task<FileDataDto> GetPdf(PrintReportsCommand request, List<ByteArrayFile> items)
        {
            byte[] res;
            var path = new FileInfo("ContractorTemplates/Reports/Report List.html").FullName;
            var source = await File.ReadAllTextAsync(path);

            source = source.Replace("{filters_area_replace}", GetFilters(request));
            source = source.Replace("{tr_area_replace}", GetTableContent(items));

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
