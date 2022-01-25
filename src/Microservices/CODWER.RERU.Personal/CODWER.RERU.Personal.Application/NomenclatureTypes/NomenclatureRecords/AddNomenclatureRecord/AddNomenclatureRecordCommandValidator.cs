using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.AddNomenclatureRecord
{
    public class AddNomenclatureRecordCommandValidator : AbstractValidator<AddNomenclatureRecordCommand>
    {
        public AddNomenclatureRecordCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new ValidCreateNomenclatureRecordValidator(appDbContext));
        }
    }
}
