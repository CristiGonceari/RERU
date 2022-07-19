using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue
{
    public class ValidUpdateNomenclatureRecordValidator : AbstractValidator<NomenclatureRecordDto>
    {
        public ValidUpdateNomenclatureRecordValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<NomenclatureRecord>(appDbContext, ValidationCodes.NOMENCLATURE_RECORD_NOT_FOUND, ValidationMessages.NotFound));

            RuleForEach(x => x.RecordValues.Where(x => x.Id != 0).Select(rv => rv.Id))
                .SetValidator(new ItemMustExistValidator<RecordValue>(appDbContext, ValidationCodes.NOMENCLATURE_RECORD_VALUE_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x).SetValidator(new NomenclatureRecordValueValidator(appDbContext));
        }
    }
}
