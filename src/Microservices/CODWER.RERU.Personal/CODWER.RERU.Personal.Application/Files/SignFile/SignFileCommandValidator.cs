using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Files.SignFile
{
    public class SignFileCommandValidator : AbstractValidator<SignFileCommand>
    {
        private readonly AppDbContext _appDbContext;

        public SignFileCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _ = userProfileService.GetCurrentContractorId().Result;

            _appDbContext = appDbContext;

            RuleFor(x => x.Data)
                .Custom(ValidateExtension);
        }

        private void ValidateExtension(SignFileDto file, CustomContext context)
        {
            var dbFile =  _appDbContext.ByteFiles.FirstOrDefault(x => x.Id == file.Id);

            if (dbFile == null)
            {
                context.AddFail(ValidationCodes.FILE_NOT_FOUND, ValidationMessages.NotFound);
            }
            else
            {
                var splitArray = file.File.FileName.Split('.');

                if (splitArray.Length < 2)
                {
                    context.AddFail(ValidationCodes.INVALID_FILE_NAME, ValidationMessages.InvalidInput);
                }

                var extension = splitArray.Last().ToLower();
                if (!FileExtensions().Contains(extension))
                {
                    context.AddFail(ValidationCodes.INVALID_FILE_EXTENSION, ValidationMessages.InvalidInput);
                }
            }
        }

        private List<string> FileExtensions() => new() { "pdf" };
    }
}
