using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using MediatR;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.AddNomenclatureRecord
{
    public class AddNomenclatureRecordCommandHandler : IRequestHandler<AddNomenclatureRecordCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IRecordMapper _recordMapper;

        public AddNomenclatureRecordCommandHandler(AppDbContext appDbContext, IMapper mapper, IRecordMapper recordMapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _recordMapper = recordMapper;
        }

        public async Task<int> Handle(AddNomenclatureRecordCommand request, CancellationToken cancellationToken)
        {
            var recordToAdd = _mapper.Map<NomenclatureRecord>(request.Data);
            recordToAdd.IsActive = true;
            recordToAdd.RecordValues = await NewRecordValues(request.Data.RecordValues);

            await _appDbContext.NomenclatureRecords.AddAsync(recordToAdd);
            await _appDbContext.SaveChangesAsync();

            return recordToAdd.Id;
        }

        private async Task<List<RecordValue>> NewRecordValues(List<RecordValueDto> records)
        {
            var results = new List<RecordValue>();

            foreach (var recordValueDto in records)
            {
                var mapped = await _recordMapper.ToRecordValue(recordValueDto);

                results.Add(mapped);
            }

            return results;
        }
    }
}
