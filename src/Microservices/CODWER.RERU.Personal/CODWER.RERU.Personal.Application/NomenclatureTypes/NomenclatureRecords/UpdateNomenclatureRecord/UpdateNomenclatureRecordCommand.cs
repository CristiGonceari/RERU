using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.UpdateNomenclatureRecord
{
    public class UpdateNomenclatureRecordCommand : IRequest<Unit>
    {
        public NomenclatureRecordDto Data { get; set; }
    }
}
