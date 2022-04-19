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
            using ExcelPackage package = new ExcelPackage(fileStream);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
            int totalRows = workSheet.Dimension.Rows;

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
                    EmailNotification = false
                };

                try
                {
                    await Mediator.Send(command);
                }
                catch (Exception e)
                {
                    workSheet.Cells[i, 6].Value = e.Message;
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
