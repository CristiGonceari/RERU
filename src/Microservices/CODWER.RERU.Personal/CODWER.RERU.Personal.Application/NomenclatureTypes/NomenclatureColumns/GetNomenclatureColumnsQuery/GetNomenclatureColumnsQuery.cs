using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.GetNomenclatureColumnsQuery
{
    public class GetNomenclatureColumnsQuery : IRequest<NomenclatureTableHeaderDto>
    {
        public int NomenclatureTypeId { get; set; }
    }
}
