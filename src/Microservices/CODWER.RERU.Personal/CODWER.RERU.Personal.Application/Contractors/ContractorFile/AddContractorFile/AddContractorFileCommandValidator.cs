using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Extensions;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    public class AddContractorFileCommandValidator : AbstractValidator<AddContractorFileCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddContractorFileCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.File).SetValidator(new FormFileValidator(ValidationMessages.InvalidInput));

            RuleFor(x => x.Data).Custom(ValidateRepetitiveName);
        }

        private void ValidateRepetitiveName(AddFileDto command, CustomContext context)
        {
            var existent = _appDbContext.ByteFiles.Any(x =>
                x.ContractorId == command.ContractorId && x.FileName == command.File.FileName);

            if (existent)
            {
                context.AddFail(ValidationCodes.FILE_NAME_IS_NOT_UNIQUE, ValidationMessages.InvalidInput);
            }
        }
    }
}
