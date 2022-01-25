using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.GetNomenclatureRecords
{
    public class GetNomenclatureRecordsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<NomenclatureRecordDto>>
    {
        public int NomenclatureTypeId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
