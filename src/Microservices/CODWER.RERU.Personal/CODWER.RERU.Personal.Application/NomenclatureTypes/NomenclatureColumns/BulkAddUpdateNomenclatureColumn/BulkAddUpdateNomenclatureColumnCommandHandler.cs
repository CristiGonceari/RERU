using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.BulkAddUpdateNomenclatureColumn
{
    public class BulkAddUpdateNomenclatureColumnCommandHandler : IRequestHandler<BulkAddUpdateNomenclatureColumnCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public BulkAddUpdateNomenclatureColumnCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddUpdateNomenclatureColumnCommand request, CancellationToken cancellationToken)
        {
            var dbNomenclatureColumns = _appDbContext.NomenclatureColumns
                .Where(x => x.NomenclatureTypeId == request.Data.NomenclatureTypeId);

            var columnsToAdd = request.Data.NomenclatureColumns.Where(dto => dto.Id == 0);
            var columnsToUpdate = dbNomenclatureColumns.Where(db => request.Data.NomenclatureColumns.Select(dto => dto.Id).Contains(db.Id));
            var columnsToRemove = dbNomenclatureColumns.Where(db => !request.Data.NomenclatureColumns.Select(dto => dto.Id).Contains(db.Id));

            RemoveExistentColumnAndRecordsValues(columnsToRemove);
            UpdateExistentColumns(columnsToUpdate, request.Data.NomenclatureColumns);
            await AddNewColumns(columnsToAdd, request.Data.NomenclatureTypeId);

            await _appDbContext.SaveChangesAsync();
            return Unit.Value;
        }

        private async Task AddNewColumns(IEnumerable<NomenclatureColumnItemDto> columns, int nomenclatureTypeId)
        {
            var itemsToAdd = new List<NomenclatureColumn>();

            foreach (var nomenclatureColumnItemDto in columns)
            {
                var columnToAdd = new NomenclatureColumn
                {
                    Name = nomenclatureColumnItemDto.Name,
                    Type = nomenclatureColumnItemDto.Type,
                    IsMandatory = nomenclatureColumnItemDto.IsMandatory,
                    Order = nomenclatureColumnItemDto.Order,
                    NomenclatureTypeId = nomenclatureTypeId // manual set
                };

                itemsToAdd.Add(columnToAdd);
            }

            await _appDbContext.NomenclatureColumns.AddRangeAsync(itemsToAdd);
        }

        private void UpdateExistentColumns(IQueryable<NomenclatureColumn> columns, List<NomenclatureColumnItemDto> nomenclatureColumns)
        {
            foreach (var nomenclatureColumn in columns)
            {
                var dto = nomenclatureColumns.First(nc => nc.Id == nomenclatureColumn.Id);

                nomenclatureColumn.Name = dto.Name;
                nomenclatureColumn.IsMandatory = dto.IsMandatory;
                nomenclatureColumn.Order = dto.Order;
            }
        }

        private void RemoveExistentColumnAndRecordsValues(IQueryable<NomenclatureColumn> columns)
        {
            var recordValuesToRemove = _appDbContext.RecordValues
                .Where(rv => columns.Select(c => c.Id).Contains(rv.NomenclatureColumnId));

            _appDbContext.RecordValues.RemoveRange(recordValuesToRemove);

            _appDbContext.NomenclatureColumns.RemoveRange(columns);
        }
    }
}
