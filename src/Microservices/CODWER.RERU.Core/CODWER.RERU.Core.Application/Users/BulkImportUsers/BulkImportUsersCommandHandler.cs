using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : BaseHandler, IRequestHandler<BulkImportUsersCommand, Unit>
    {
        public BulkImportUsersCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<Unit> Handle(BulkImportUsersCommand request, CancellationToken cancellationToken)
        {
            await Import(request.Data.File);

            return Unit.Value;
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
                    string lastName = workSheet?.Cells[i, 2]?.Value?.ToString();
                    string patronymic = workSheet?.Cells[i, 3]?.Value?.ToString();
                    string idnp = workSheet?.Cells[i, 4]?.Value?.ToString();
                    string email = workSheet?.Cells[i, 5]?.Value?.ToString();

                    var command = new CreateUserCommand
                    {
                        FirstName = name,
                        LastName = lastName,
                        FatherName = patronymic,
                        Email = email,
                        Idnp = idnp,
                        EmailNotification = true
                    };

                    await Mediator.Send(command);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Excel error, please try with new created excel document {e.Message}");
            }
        }

    }
}
