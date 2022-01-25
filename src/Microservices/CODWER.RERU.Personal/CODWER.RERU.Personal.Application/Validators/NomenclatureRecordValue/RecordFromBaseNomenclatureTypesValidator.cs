using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue
{
    public class RecordFromBaseNomenclatureTypesValidator : AbstractValidator<RecordToValidate>
    {
        private readonly AppDbContext _appDbContext;
        public RecordFromBaseNomenclatureTypesValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom(ValidateRecord);
        }

        private void ValidateRecord(RecordToValidate record, CustomContext context)
        {
            var existent = _appDbContext.NomenclatureRecords
                .Include(x => x.NomenclatureType)
                .Any(x => x.Id == record.NomenclatureRecordId 
                          && x.NomenclatureType.BaseNomenclature == record.BaseNomenclatureTypes
                          && x.IsActive);

            if (!existent)
            {
                context.AddFail(ValidationCodes.NOMENCLATURE_RECORD_NOT_FOUND, ValidationMessages.InvalidReference);
            }
        }
    }
}
