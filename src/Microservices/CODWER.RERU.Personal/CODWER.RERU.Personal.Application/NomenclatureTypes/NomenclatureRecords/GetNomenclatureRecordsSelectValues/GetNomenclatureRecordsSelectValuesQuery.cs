using System.Collections.Generic;
using RERU.Data.Entities.PersonalEntities.Enums;
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
