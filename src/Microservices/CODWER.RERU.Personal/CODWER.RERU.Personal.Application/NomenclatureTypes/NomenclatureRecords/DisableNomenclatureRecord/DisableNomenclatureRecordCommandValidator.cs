using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.DisableNomenclatureRecord
{
    public class DisableNomenclatureRecordCommandValidator: AbstractValidator<DisableNomenclatureRecordCommand>
    {
        public DisableNomenclatureRecordCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<NomenclatureRecord>(appDbContext, ValidationCodes.NOMENCLATURE_RECORD_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
