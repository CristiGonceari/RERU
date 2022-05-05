using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : BaseHandler, IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        public BulkImportUsersCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<FileDataDto> Handle(BulkImportUsersCommand request, CancellationToken cancellationToken)
        {
            return await Import(request.Data.File); 
        }

        public async Task<FileDataDto> Import(IFormFile data)
        {
            var fileStream = new MemoryStream();
            await data.CopyToAsync(fileStream);
            using var package = new ExcelPackage(fileStream);
            var workSheet = package.Workbook.Worksheets[0];
            var totalRows = workSheet.Dimension.Rows;


            for (var i = 1; i <= totalRows; i++)
            {
                var command = new CreateUserCommand
                {
                    LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                    FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                    FatherName = workSheet.Cells[i, 3]?.Value?.ToString(),
                    Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                    Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                    EmailNotification = bool.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "True")
                };

                try
                {
                    await Mediator.Send(command);
                }
                catch (Exception e)
                {
                    workSheet.Cells[i, 7].Value = e.Message;
                    Console.WriteLine(e);
                }
            }

            var streamBytesArray = package.GetAsByteArray();

            return new FileDataDto
            {
                Content = streamBytesArray,
                Name = "User-Import-Result",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
