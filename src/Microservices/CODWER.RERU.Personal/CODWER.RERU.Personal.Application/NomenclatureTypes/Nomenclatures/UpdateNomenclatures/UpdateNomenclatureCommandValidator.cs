using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

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
