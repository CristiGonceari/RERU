using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.UpdateNomenclatures
{
    public class UpdateNomenclatureCommandValidator : AbstractValidator<UpdateNomenclatureCommand>
    {
        public UpdateNomenclatureCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Name).NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<NomenclatureType>(appDbContext, ValidationCodes.NOMENCLATURE_TYPE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
