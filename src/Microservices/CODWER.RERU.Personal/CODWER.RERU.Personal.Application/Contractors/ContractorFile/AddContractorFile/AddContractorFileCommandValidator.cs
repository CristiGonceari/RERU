using CODWER.RERU.Personal.Application.Validators.Files;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using CVU.ERP.StorageService.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    public class AddContractorFileCommandValidator : AbstractValidator<AddContractorFileCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly StorageDbContext _storageDbContext;
        public AddContractorFileCommandValidator(AppDbContext appDbContext, StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _storageDbContext = storageDbContext;

            RuleFor(x => x.Data.File).SetValidator(new FormFileValidator(ValidationMessages.InvalidInput));

            //RuleFor(x => x.ContractorId).Custom(ValidateRepetitiveName());
        }

        //private void ValidateRepetitiveName(int contractorId, CustomContext context)
        //{
        //    
        //    if (existent)
        //    {
        //        context.AddFail(ValidationCodes.FILE_NAME_IS_NOT_UNIQUE, ValidationMessages.InvalidInput);
        //    }
        //}
    }
}
