using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue
{
    public class ValidCreateNomenclatureRecordValidator: AbstractValidator<NomenclatureRecordDto>
    {
        public ValidCreateNomenclatureRecordValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x).SetValidator(new NomenclatureRecordValueValidator(appDbContext));
        }
    }
}
