using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.DisableNomenclatureRecord
{
    public class DisableNomenclatureRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
