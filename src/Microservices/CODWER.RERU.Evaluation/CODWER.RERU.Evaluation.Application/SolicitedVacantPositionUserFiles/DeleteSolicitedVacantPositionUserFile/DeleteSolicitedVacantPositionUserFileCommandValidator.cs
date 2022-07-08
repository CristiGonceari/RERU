using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.StorageFileServices.Validators;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.DeleteSolicitedVacantPositionUserFile
{
    public class DeleteSolicitedVacantPositionUserFileCommandValidator : AbstractValidator<DeleteSolicitedVacantPositionUserFileCommand>
    {
        public DeleteSolicitedVacantPositionUserFileCommandValidator(StorageDbContext storageDbContext)
        {
            RuleFor(x => x.FileId)
                .NotEmpty()
                .SetValidator(x => new StorageFileMustExistValidator<File>(storageDbContext, ValidationCodes.INVALID_RECORD,
                    ValidationMessages.NotFound));
        }
    }
}
