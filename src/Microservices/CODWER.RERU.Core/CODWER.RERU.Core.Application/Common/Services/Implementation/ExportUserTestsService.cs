using CODWER.RERU.Core.DataTransferObjects.Files;
using CODWER.RERU.Core.DataTransferObjects.Users;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Common.Services.Implementation
{
    public class ExportUserTestsService : IExportUserTestsService
    {
        public async Task<ExportExcel> DonwloadUserTestsExcel(List<UserTestDto> data)
        {
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            var items = data.ToArray();

            for (var i = 0; i <= items.Length; i++)
            {
                if(i == 0) 
                {
                    if(items.Length != 0)
                    {
                        workSheet.Cells[1, 1].Value = items[i].UserName;
                        workSheet.Cells[1, 1].Style.Font.Bold = true;
                        SetBordersStyleOnCells(workSheet, 1, 1);
                    }

                    workSheet.Cells[2, 1].Value = "Nume testului";
                    workSheet.Cells[2, 1].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 1);

                    workSheet.Cells[2, 2].Value = "Procent minim";
                    workSheet.Cells[2, 2].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 2);

                    workSheet.Cells[2, 3].Value = "Numarul intrebarilor";
                    workSheet.Cells[2, 3].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 3);

                    workSheet.Cells[2, 4].Value = "Resultat";
                    workSheet.Cells[2, 4].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 4);

                    workSheet.Cells[2, 5].Value = "Procent Acumulat";
                    workSheet.Cells[2, 5].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 5);

                    workSheet.Cells[2, 6].Value = "Numele Evenimentului";
                    workSheet.Cells[2, 6].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 6);

                    workSheet.Cells[2, 7].Value = "Statutul Testului";
                    workSheet.Cells[2, 7].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 7);

                    workSheet.Cells[2, 8].Value = "Procesul verificarii";
                    workSheet.Cells[2, 8].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 2, 8);

                    StyleHeadersCells(workSheet);
                }
                else
                {
                    workSheet.Cells[i + 2, 1].Value = items[i - 1].TestTemplateName;
                    workSheet.Cells[i + 2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 1);

                    workSheet.Cells[i + 2, 2].Value = items[i - 1].MinPercent;
                    workSheet.Cells[i + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 2);

                    workSheet.Cells[i + 2, 3].Value = items[i - 1].QuestionCount;
                    workSheet.Cells[i + 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 3);

                    workSheet.Cells[i + 2, 4].Value = items[i - 1].Result;
                    workSheet.Cells[i + 2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 4);

                    workSheet.Cells[i + 2, 5].Value = items[i - 1].AccumulatedPercentage;
                    workSheet.Cells[i + 2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 5);

                    workSheet.Cells[i + 2, 6].Value = items[i - 1].EventName;
                    workSheet.Cells[i + 2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 6);

                    workSheet.Cells[i + 2, 7].Value = items[i - 1].TestStatus;
                    workSheet.Cells[i + 2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 7);

                    workSheet.Cells[i + 2, 8].Value = items[i - 1].VerificationProgress;
                    workSheet.Cells[i + 2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SetBordersStyleOnCells(workSheet, i + 2, 8);
                }
            }

            var excelName = $"User Test.xlsx";
            var streamBytesArray = package.GetAsByteArray();

            return new ExportExcel()
            {
                Content = streamBytesArray,
                Name = excelName,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        private static void StyleHeadersCells(ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1].Style.Font.Color.SetColor(Color.DarkBlue);
            worksheet.Column(1).Width = 25;
            worksheet.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(1).Width = 25;
            worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(2).Width = 15;
            worksheet.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(3).Width = 18;
            worksheet.Cells[2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(4).Width = 11;
            worksheet.Cells[2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(5).Width = 18;
            worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(6).Width = 25;
            worksheet.Cells[2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(7).Width = 18;
            worksheet.Cells[2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(8).Width = 18;
        }

        private static void SetBordersStyleOnCells(ExcelWorksheet worksheet, int row, int column)
        {
            worksheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }
    }
}
