using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.GetNomenclatureColumnsQuery
{
    public class GetNomenclatureTypeColumnsQueryValidator : AbstractValidator<GetNomenclatureColumnsQuery>
    {
        public GetNomenclatureTypeColumnsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.NomenclatureTypeId)
                .SetValidator(new ItemMustExistValidator<NomenclatureType>(appDbContext, ValidationCodes.NOMENCLATURE_TYPE_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
