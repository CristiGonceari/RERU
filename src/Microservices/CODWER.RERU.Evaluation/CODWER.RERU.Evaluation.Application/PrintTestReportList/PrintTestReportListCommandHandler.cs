using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation.Application.PrintTestReportList
{
    public class PrintTestReportListCommandHandler : IRequestHandler<PrintTestReportListCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;

        public PrintTestReportListCommandHandler(IGeneratePdf generatePdf, AppDbContext appDbContext)
        {
            _generatePdf = generatePdf;
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(PrintTestReportListCommand request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Tests
                .Select(x => new Test()
                {
                    ProgrammedTime = x.ProgrammedTime,
                    TestStatus = x.TestStatus,
                    ShowUserName = x.ShowUserName
                })
                .AsQueryable();


            return await GetPdf(items.ToList());
        }
        private string GetTableContent(List<Test> items)
        {
            var content = string.Empty;

            foreach (var item in items)
            {
                content += $@"
                          <tr>
                            <td>{item.ProgrammedTime:dd/MM/yyyy}</td>
                            <td>{item.TestStatus}</td>
                            <td>{item.ShowUserName}</td>
                          </tr>";
            }

            return content;
        }

        public async Task<FileDataDto> GetPdf(List<Test> items)
        {
            byte[] res;
            var path = new FileInfo("PdfTemplates/TestPage.html").FullName;
            var source = await File.ReadAllTextAsync(path);

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
                Name = "TestReport_List.pdf"
            };
        }
    }
}
