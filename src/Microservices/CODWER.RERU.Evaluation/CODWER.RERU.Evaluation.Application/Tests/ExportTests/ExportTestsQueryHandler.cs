using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.ExportTests
{
    public class ExportTestsQueryHandler : IRequestHandler<ExportTestsQuery, byte[]>
    {
        private readonly AppDbContext _appDbContext;

        public ExportTestsQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<byte[]> Handle(ExportTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await _appDbContext.Tests
                .Include(x => x.TestTemplates)
                .Include(x => x.TestQuestions)
                .Include(x => x.UserProfile)
                .ToListAsync();

            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("Teste");

                ws.Cells["A1"].Value = "Idnp";
                ws.Cells["B1"].Value = "Numele";
                ws.Cells["C1"].Value = "Prenumele";
                ws.Cells["D1"].Value = "Patronimicul";
                ws.Cells["E1"].Value = "Denumirea Testului";
                ws.Cells["F1"].Value = "Data";
                ws.Cells["G1"].Value = "Rezultat";
                ws.Cells["H1"].Value = "Raspunsuri corecte";
                ws.Cells["I1"].Value = "Total intrebari";

                var i = 2;
                foreach (var test in tests)
                {
                    var rezultStatus = string.Empty;
                    if (test.ResultStatus == TestResultStatusEnum.Passed)
                    {
                        rezultStatus = "Admis";
                    }
                    else if (test.ResultStatus == TestResultStatusEnum.NotPassed)
                    {
                        rezultStatus = "Neadmis";
                    }
                    else
                    {
                        rezultStatus = "Neterminat";
                    }

                    ws.Cells[i, 1].Value = test.UserProfile.Idnp;
                    ws.Cells[i, 2].Value = test.UserProfile.LastName;
                    ws.Cells[i, 3].Value = test.UserProfile.FirstName;
                    ws.Cells[i, 4].Value = test.UserProfile.Patronymic;
                    ws.Cells[i, 5].Value = test.TestTemplates.Name;
                    ws.Cells[i, 6].Value = $"{test.ProgrammedTime.Hour}:{test.ProgrammedTime.Minute} {test.ProgrammedTime.Day.ToString("00")}.{test.ProgrammedTime.Month.ToString("00")}.{test.ProgrammedTime.Year}";
                    ws.Cells[i, 7].Value = rezultStatus;
                    ws.Cells[i, 8].Value = test.TestQuestions.Count(x => x.IsCorrect.HasValue && x.IsCorrect == true);
                    ws.Cells[i, 9].Value = test.TestQuestions.Count;

                    i++;
                }

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                ws.Column(8).AutoFit();
                ws.Column(9).AutoFit();

                return await p.GetAsByteArrayAsync();
            }
        }
    }
}
