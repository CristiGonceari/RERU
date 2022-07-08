using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.StorageFileServices.Validators;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFile
{
    public class GetSolicitedVacantPositionUserFileQueryValidator : AbstractValidator<GetSolicitedVacantPositionUserFileQuery>
    {
        public GetSolicitedVacantPositionUserFileQueryValidator(StorageDbContext storageDbContext)
        {
            RuleFor(x => x.FileId)
                .SetValidator(x => new StorageFileMustExistValidator<File>(storageDbContext, ValidationCodes.EMPTY_FILE,
                    ValidationMessages.NotFound));
        }
    }
}
