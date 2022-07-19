using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;

namespace CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue
{
    public static class NomenclatureExtensions
    {
        public static RecordToValidate NewRecordToValidate(this BaseNomenclatureTypesEnum baseNomenclature, int recordId)
        {
            return new()
            {
                NomenclatureRecordId = recordId,
                BaseNomenclatureTypes = baseNomenclature
            };
        }
    }
}
