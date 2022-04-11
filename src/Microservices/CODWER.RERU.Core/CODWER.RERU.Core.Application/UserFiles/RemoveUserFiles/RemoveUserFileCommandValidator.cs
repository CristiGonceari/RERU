using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.StorageFileServices.Validators;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using FluentValidation;

namespace CODWER.RERU.Core.Application.UserFiles.RemoveUserFiles
{
    public class RemoveUserFileCommandValidator : AbstractValidator<RemoveUserFileCommand>
    {
        public RemoveUserFileCommandValidator(StorageDbContext storageDbContext)
        {
            RuleFor(x => x.FileId)
                .SetValidator(x => new StorageFileMustExistValidator<File>(storageDbContext, ValidationCodes.INVALID_FILE_ID,
                    ValidationMessages.NotFound));
        }
    }
}
