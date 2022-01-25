using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.AddNomenclatureRecord
{
    public class AddNomenclatureRecordCommand : IRequest<int>
    {
        public NomenclatureRecordDto Data { get; set; }
    }
}
