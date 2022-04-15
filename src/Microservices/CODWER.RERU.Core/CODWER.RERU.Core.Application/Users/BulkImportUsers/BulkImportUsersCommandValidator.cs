using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandValidator : AbstractValidator<BulkImportUsersCommand>
    {
        public BulkImportUsersCommandValidator()
        {
            RuleFor(x => x.Data.File.FileName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Data.File.FileName)
                .Custom(CheckExtension);

            RuleFor(x => x.Data.File)
                .Custom(CheckFileWorksheets);
        }

        private void CheckExtension(string fileName, CustomContext context)
        {
            if (!fileName.Split(".").Last().Contains("xlsx"))
            {
                context.AddFail(ValidationCodes.INVALID_INPUT, ValidationMessages.InvalidInput);
            }
        }

        private void CheckFileWorksheets(IFormFile file, CustomContext context)
        {
            try
            {
                var fileStream = new MemoryStream();
                file.CopyTo(fileStream);
                using ExcelPackage package = new ExcelPackage(fileStream);
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
            }
            catch (Exception e)
            {
                context.AddFail(ValidationCodes.FILE_IS_CORRUPTED, $"Excel error, please try with new created excel document : {e.Message}");
            }
        }
    }
}
