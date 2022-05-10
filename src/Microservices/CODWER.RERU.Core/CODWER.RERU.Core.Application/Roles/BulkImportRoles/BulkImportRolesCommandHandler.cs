using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Roles.AddRole;
using CODWER.RERU.Core.Application.Roles.UpdateRole;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.BulkImportRoles
{
    public class BulkImportRolesCommandHandler : BaseHandler, IRequestHandler<BulkImportRolesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;

        public BulkImportRolesCommandHandler(ICommonServiceProvider commonServiceProvider, AppDbContext appDbContext) : base(commonServiceProvider)
        {
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(BulkImportRolesCommand request, CancellationToken cancellationToken)
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

            var roles = _appDbContext.Roles.AsQueryable();

            for (var i = 1; i <= totalRows; i++)
            {
                var addCommand = new AddRoleCommand()
                {
                    ColaboratorId = int.Parse(workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty),
                    Name = workSheet.Cells[i, 2]?.Value?.ToString()
                };

                if (roles.Any(x => x.ColaboratorId == addCommand.ColaboratorId))
                {
                    try
                    {
                        var roleByColaborator = roles.FirstOrDefault(x => x.ColaboratorId == addCommand.ColaboratorId);

                        var editCommand = new UpdateRoleCommand()
                        {
                            Id = roleByColaborator.Id,
                            Name = workSheet.Cells[i, 2]?.Value?.ToString(),
                            ColaboratorId = roleByColaborator.ColaboratorId
                        };

                        await Mediator.Send(editCommand);
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 3].Value = e.Message;
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    try
                    {
                        await Mediator.Send(addCommand);
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 3].Value = e.Message;
                        Console.WriteLine(e);
                    }
                }
            }

            var streamBytesArray = package.GetAsByteArray();

            return new FileDataDto
            {
                Content = streamBytesArray,
                Name = "Role-Import-Result",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
