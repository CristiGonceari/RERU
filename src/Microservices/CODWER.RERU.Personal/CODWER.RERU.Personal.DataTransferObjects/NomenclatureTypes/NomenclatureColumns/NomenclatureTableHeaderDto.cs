using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns
{
    public class NomenclatureTableHeaderDto
    {
        public int NomenclatureTypeId { get; set; }
        public List<NomenclatureColumnItemDto> NomenclatureColumns { get; set; }
    }
}
