using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclaturesSelectValues
{
    public class GetNomenclaturesSelectValuesQuery : IRequest<IEnumerable<SelectItem>>
    {
    }
}
