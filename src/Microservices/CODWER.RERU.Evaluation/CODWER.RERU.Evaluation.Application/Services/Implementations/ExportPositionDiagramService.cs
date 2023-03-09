using CVU.ERP.Common.DataTransferObjects.Files;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionDiagram;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class ExportPositionDiagramService : IExportPositionDiagramService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public ExportPositionDiagramService(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task<FileDataDto> ExportPositionDiagram(int positionId)
        {
            var candidatePosition = await _appDbContext.CandidatePositions.FirstOrDefaultAsync(t => t.Id == positionId);

            var file = await CreateExcelFile(candidatePosition);

            return file;
        }

        public async Task<FileDataDto> CreateExcelFile(CandidatePosition candidatePosition)
        {
            var memoryStream = new MemoryStream();
            using var package = new ExcelPackage(memoryStream);
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            var command = new GetPositionDiagramQuery { PositionId = candidatePosition.Id };
            var eventsDiagram = await _mediator.Send(command);

            await SetDiagramTitle(eventsDiagram, candidatePosition, workSheet);

            await SetDiagramHeaderDetails(eventsDiagram, workSheet);

            await SetDiagramContentDetails(eventsDiagram, workSheet);

            var excelFile = GetExcelFile(package);

            return excelFile;
        }

        private async Task SetDiagramTitle(PositionDiagramDto eventsDiagram, CandidatePosition candidatePosition, ExcelWorksheet workSheet)
        {
            workSheet.Rows.Height = 35;
            workSheet.Columns.Width = 32;
            workSheet.Columns.AutoFit();

            var toColl = eventsDiagram.EventsDiagram.Sum(x => x.TestTemplates.Count()) + 2;

            await SetExcelCommonCellsStyle(workSheet, 1, 1, 2, toColl,$"BORDEROUL POZIȚIEI: {candidatePosition.Name}", 18, true);
        }

        private async Task SetDiagramHeaderDetails(PositionDiagramDto eventsDiagram, ExcelWorksheet workSheet)
        {
            var eventCol = 3;
            var testsTemplateCol = 3;

            await SetExcelCommonCellsStyle(workSheet, 3, 1, 4, 2, "Utilizatori", 14, true);

            foreach (var eventDiagram in eventsDiagram.EventsDiagram)
            {
                await SetExcelCommonCellsStyle(workSheet, 3, eventCol, 3, eventCol + eventDiagram.TestTemplates.Count() - 1, eventDiagram.EventName, 12, true);

                eventCol += eventDiagram.TestTemplates.Count;

                foreach (var testTemplate in eventDiagram.TestTemplates)
                {
                    await SetExcelCommonCellsStyle(workSheet, 4, testsTemplateCol, 4, testsTemplateCol, testTemplate.Name, 12, true);

                    testsTemplateCol++;
                }
            }
        }

        private async Task SetDiagramContentDetails(PositionDiagramDto eventsDiagram, ExcelWorksheet workSheet)
        {
            var userRow = 5;
            var testCol = 3;

            foreach (var user in eventsDiagram.UsersDiagram)
            {
                await SetExcelCommonCellsStyle(workSheet, userRow, 1, userRow, 2, user.FullName, 12, true);

                foreach (var testTemplate in user.TestsByTestTemplate)
                {
                    await SetExcelCommonCellsStyle(workSheet, userRow, testCol, userRow, testCol, "", 12, false);

                    foreach (var test in testTemplate.Tests)
                    {
                        workSheet.Cells[userRow, testCol, userRow, testCol].Value = $@"- {EnumMessages.Translate(test.Result)}, {test.PassDate:dd/MM/yyyy HH:mm}, {EnumMessages.Translate(test.Status)}";
                    }

                    testCol++;
                }

                testCol = 3;
                userRow++;
            }
        }

        private async Task SetExcelCommonCellsStyle(ExcelWorksheet workSheet, int x, int y, int z, int w, string text, int fontSize, bool fontBold)
        {
            workSheet.Cells[x, y, z, w].Value = text;
            workSheet.Cells[x, y, z, w].Merge = true;
            workSheet.Cells[x, y, z, w].Style.Font.Bold = fontBold;
            workSheet.Cells[x, y, z, w].Style.Font.Size = fontSize;
            workSheet.Cells[x, y, z, w].Style.WrapText = true;
            workSheet.Cells[x, y, z, w].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[x, y, z, w].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[x, y, z, w].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }

        private FileDataDto GetExcelFile(ExcelPackage package)
        {
            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel("Borderoul_Pozitiei", streamBytesArray);
        }
    }
}
