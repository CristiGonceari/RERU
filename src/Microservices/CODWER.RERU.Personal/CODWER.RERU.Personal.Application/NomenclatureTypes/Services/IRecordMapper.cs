using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Services
{
    public interface IRecordMapper
    {
        Task<RecordValue> ToRecordValue(RecordValueDto recordValueDto);
    }
}
