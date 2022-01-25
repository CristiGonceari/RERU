using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.GetNomenclatureRecords
{
    public class GetNomenclatureRecordsQueryHandler : IRequestHandler<GetNomenclatureRecordsQuery, PaginatedModel<NomenclatureRecordDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;

        public GetNomenclatureRecordsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _mapper = mapper;
        }

        public async Task<PaginatedModel<NomenclatureRecordDto>> Handle(GetNomenclatureRecordsQuery request, CancellationToken cancellationToken)
        {
            var nomenclatureRecords = _appDbContext.NomenclatureRecords
                .Where(x => x.NomenclatureTypeId == request.NomenclatureTypeId)
                .OrderBy(x => x.Name)
                .ThenByDescending(nr => nr.IsActive)
                .AsQueryable();

            var nomenclatureColumns = _appDbContext.NomenclatureColumns
                .Where(x => x.NomenclatureTypeId == request.NomenclatureTypeId)
                .OrderBy(x => x.Order)
                .ToList();

            nomenclatureRecords = Filter(request, nomenclatureRecords);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<NomenclatureRecord, NomenclatureRecordDto>(nomenclatureRecords, request);

            var allRecordValues = _appDbContext.RecordValues
                    .Where(rv => paginatedModel.Items.Select(x => x.Id).Contains(rv.NomenclatureRecordId))
                    .ToList();

            foreach (var nomenclatureRecord in paginatedModel.Items)
            {
                var recordValues = allRecordValues.Where(rv => rv.NomenclatureRecordId == nomenclatureRecord.Id).ToList();

                foreach (var nomenclatureColumn in nomenclatureColumns)
                {
                    var recordValue = recordValues.FirstOrDefault(x => x.NomenclatureColumnId == nomenclatureColumn.Id);

                    var itemToAdd = recordValue != null ? _mapper.Map<RecordValueDto>(recordValue) : GetNewItem(nomenclatureColumn.Id, nomenclatureRecord.Id);
                    itemToAdd.Type = nomenclatureColumn.Type;

                    nomenclatureRecord.RecordValues.Add(itemToAdd);
                }
            }

            return paginatedModel;
        }

        private IQueryable<NomenclatureRecord> Filter(GetNomenclatureRecordsQuery request, IQueryable<NomenclatureRecord> nomenclatureRecords)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                nomenclatureRecords = nomenclatureRecords.Where(x => x.Name.Contains(request.Name));
            }

            if (request.IsActive.HasValue)
            {
                nomenclatureRecords = request.IsActive == true ? nomenclatureRecords.Where(x => x.IsActive) : nomenclatureRecords.Where(x => !x.IsActive);
            }

            return nomenclatureRecords;
        }

        private RecordValueDto GetNewItem(int columnId, int recordId)
        {
            return new()
            {
                NomenclatureColumnId = columnId,
                NomenclatureRecordId = recordId
            };
        }
    }
}
