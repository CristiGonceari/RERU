using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Services
{
    public class RecordMapper : IRecordMapper
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public RecordMapper(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RecordValue> ToRecordValue(RecordValueDto recordValueDto)
        {
            var recordColumn = await _appDbContext.NomenclatureColumns
                .Select(x => new {x.Id, x.Type})
                .FirstOrDefaultAsync(x => x.Id == recordValueDto.NomenclatureColumnId);

            if (recordColumn != null)
                return recordColumn.Type switch
                {
                    FieldTypeEnum.Boolean => _mapper.Map<RecordValueBoolean>(recordValueDto),
                    FieldTypeEnum.Character => _mapper.Map<RecordValueChar>(recordValueDto),
                    FieldTypeEnum.Date => _mapper.Map<RecordValueDate>(recordValueDto),
                    FieldTypeEnum.DateTime => _mapper.Map<RecordValueDateTime>(recordValueDto),
                    FieldTypeEnum.Double => _mapper.Map<RecordValueDouble>(recordValueDto),
                    FieldTypeEnum.Email => _mapper.Map<RecordValueEmail>(recordValueDto),
                    FieldTypeEnum.Integer => _mapper.Map<RecordValueInteger>(recordValueDto),
                    FieldTypeEnum.Text => _mapper.Map<RecordValueText>(recordValueDto),
                    _ => throw new System.NotImplementedException()
                };

            return null;
        }
    }
}
