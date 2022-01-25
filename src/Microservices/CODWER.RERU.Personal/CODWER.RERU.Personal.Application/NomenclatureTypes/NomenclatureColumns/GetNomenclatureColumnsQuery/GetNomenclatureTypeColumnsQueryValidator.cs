using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

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
