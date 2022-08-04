using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclature
{
    public class GetNomenclatureQueryValidator : AbstractValidator<GetNomenclatureQuery>
    {
        public GetNomenclatureQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<NomenclatureType>(appDbContext,
                    ValidationCodes.NOMENCLATURE_TYPE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
