using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Infrastructure.Extensions;
using CVU.ERP.Logging;
using CVU.ERP.StorageService.Models;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : BaseHandler, IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<CreateUserCommandHandler> _loggerService;
        public BulkImportUsersCommandHandler(ICommonServiceProvider commonServiceProvider, 
            ILoggerService<CreateUserCommandHandler> loggerService, 
            IEnumerable<IIdentityService> identityServices) : base(commonServiceProvider)
        {
            _loggerService = loggerService;
            _identityServices = identityServices;
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
                    FirstName = workSheet?.Cells[i, 1]?.Value?.ToString(),
                    LastName = workSheet?.Cells[i, 2]?.Value?.ToString(),
                    FatherName = workSheet?.Cells[i, 3]?.Value?.ToString(),
                    Idnp = workSheet?.Cells[i, 4]?.Value?.ToString(),
                    Email = workSheet?.Cells[i, 5]?.Value?.ToString(),
                    EmailNotification = bool.Parse(workSheet?.Cells[i, 6]?.Value?.ToString())
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
