using System.Collections.Generic;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.GetNomenclatureRecordsSelectValues
{
    public class GetNomenclatureRecordsSelectValuesQuery : IRequest<IEnumerable<SelectItem>>
    {
        public int? NomenclatureTypeId { get; set; }

        public BaseNomenclatureTypesEnum? NomenclatureBaseType { get; set; }
    }
}
