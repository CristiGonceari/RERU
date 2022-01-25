using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.DeleteContractorFile
{
    public class DeleteContractorFileCommandValidator : AbstractValidator<DeleteContractorFileCommand>
    {
        public DeleteContractorFileCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.FileId)
                .SetValidator(new ItemMustExistValidator<ByteArrayFile>(appDbContext, ValidationCodes.FILE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
