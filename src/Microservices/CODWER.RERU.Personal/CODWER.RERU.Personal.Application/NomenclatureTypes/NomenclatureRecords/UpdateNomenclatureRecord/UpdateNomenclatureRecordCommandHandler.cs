using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.UpdateNomenclatureRecord
{
    public class UpdateNomenclatureRecordCommandHandler : IRequestHandler<UpdateNomenclatureRecordCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IRecordMapper _recordMapper;

        public UpdateNomenclatureRecordCommandHandler(AppDbContext appDbContext, IMapper mapper, IRecordMapper recordMapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _recordMapper = recordMapper;
        }

        public async Task<Unit> Handle(UpdateNomenclatureRecordCommand request, CancellationToken cancellationToken)
        {
            var dbNomenclatureRecord = await _appDbContext.NomenclatureRecords
                .Include(x => x.RecordValues)
                .FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, dbNomenclatureRecord);

            UpdateExistentRecordValues(dbNomenclatureRecord, request.Data.RecordValues.Where(x=>x.Id != 0).ToList());
            await AddNewRecordValues(dbNomenclatureRecord, request.Data.RecordValues.Where(x => x.Id == 0).ToList());

            await _appDbContext.SaveChangesAsync();
            return Unit.Value;
        }

        private void UpdateExistentRecordValues(NomenclatureRecord nomenclatureRecord, List<RecordValueDto> recordValueDtos)
        {
            foreach (var recordValueDto in recordValueDtos)
            {
                var dbItem = nomenclatureRecord.RecordValues.First(x => x.Id == recordValueDto.Id);

                _mapper.Map(recordValueDto, dbItem);
            }
        }

        private async Task AddNewRecordValues(NomenclatureRecord nomenclatureRecord, List<RecordValueDto> recordValueDtos)
        {
            foreach (var recordValueDto in recordValueDtos)
            {
                var mapped = await _recordMapper.ToRecordValue(recordValueDto);

                nomenclatureRecord.RecordValues.Add(mapped);
            }
        }
    }
}
