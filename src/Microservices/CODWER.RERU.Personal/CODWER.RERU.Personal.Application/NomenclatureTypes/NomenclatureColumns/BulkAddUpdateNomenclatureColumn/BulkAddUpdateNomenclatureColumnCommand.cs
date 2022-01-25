using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.BulkAddUpdateNomenclatureColumn
{
    public class BulkAddUpdateNomenclatureColumnCommand : IRequest<Unit>
    {
        public NomenclatureTableHeaderDto Data { get; set; }
    }
}
