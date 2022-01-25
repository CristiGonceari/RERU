using System.Collections.Generic;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords
{
    public class NomenclatureRecordDto
    {
        public NomenclatureRecordDto()
        {
            RecordValues = new List<RecordValueDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int NomenclatureTypeId { get; set; }

        public List<RecordValueDto> RecordValues { get; set; }
    }
}
