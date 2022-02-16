using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.StorageFileServices.Validators;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.GetContractorFile
{
    public class GetContractorFileQueryValidator : AbstractValidator<GetContractorFileQuery>
    {
        public GetContractorFileQueryValidator(StorageDbContext storageDbContext)
        {
            RuleFor(x => x.FileId)
                     .SetValidator(new StorageFileMustExistValidator<File>(storageDbContext, ValidationCodes.BONUS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
