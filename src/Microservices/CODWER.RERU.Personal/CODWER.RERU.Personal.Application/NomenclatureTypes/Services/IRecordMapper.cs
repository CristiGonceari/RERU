using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords.RecordValues;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Services
{
    public interface IRecordMapper
    {
        Task<RecordValue> ToRecordValue(RecordValueDto recordValueDto);
    }
}
