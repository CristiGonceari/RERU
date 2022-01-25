using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclatures
{
    public class GetNomenclaturesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<NomenclatureTypeDto>>
    {
        public bool? IsActive { get; set; }
    }
}
