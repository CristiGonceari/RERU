using System;
using System.IO;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class ExcelImporter : IExcelImporter
    {
        private readonly AppDbContext _appDbContext;

        public ExcelImporter(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Import(IFormFile data)
        {
            var fileStream = new MemoryStream();
            await data.CopyToAsync(fileStream);
            using ExcelPackage package = new ExcelPackage(fileStream);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
            int totalRows = workSheet.Dimension.Rows;

            try
            {
                for (int i = 1; i <= totalRows; i++)
                {
                    string name = workSheet?.Cells[i, 1]?.Value?.ToString();
                    string code = workSheet?.Cells[i, 2]?.Value?.ToString();
                    string shortCode = workSheet?.Cells[i, 3]?.Value?.ToString();

                    await _appDbContext.Roles.AddAsync(new Role
                    {
                        Name = name,
                        //Code = code,
                        //ShortCode = shortCode
                    });

                    if (i % 100 == 0 || i == totalRows)
                    {
                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Excel error, please try with new created excel document {e.Message}");
            }
        }
    }
}
