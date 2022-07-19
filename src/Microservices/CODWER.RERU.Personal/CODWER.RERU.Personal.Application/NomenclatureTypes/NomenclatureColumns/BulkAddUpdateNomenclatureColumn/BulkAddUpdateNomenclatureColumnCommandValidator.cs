using CODWER.RERU.Personal.Application.Validators.NomenclatureTableHeader;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.BulkAddUpdateNomenclatureColumn
{
    public class BulkAddUpdateNomenclatureColumnCommandValidator : AbstractValidator<NomenclatureTableHeaderDto>
    {
        public BulkAddUpdateNomenclatureColumnCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x).SetValidator(new ValidNomenclatureHeaderValidator(appDbContext));
        }
    }
}
