using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.BulkAddEditModernLanguageLevels
{
    public class BulkAddEditModernLanguageLevelsCommandHandler : IRequestHandler<BulkAddEditModernLanguageLevelsCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddEditModernLanguageLevelsCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddEditModernLanguageLevelsCommand request, CancellationToken cancellationToken)
        {
            var modernLanguagelevels = await _appDbContext.ModernLanguageLevels.Where(s => s.ContractorId == request.Data[0].ContractorId).ToListAsync();

            foreach (var addEditModernLanguageLevelDto in request.Data)
            {
                var existentModernLanguageLevels = modernLanguagelevels.FirstOrDefault(s => s.Id == addEditModernLanguageLevelDto.Id);

                if (existentModernLanguageLevels == null)
                {
                    var item = _mapper.Map<ModernLanguageLevel>(addEditModernLanguageLevelDto);

                    await _appDbContext.ModernLanguageLevels.AddAsync(item);
                }
                else
                {
                    _mapper.Map(addEditModernLanguageLevelDto, existentModernLanguageLevels);

                    modernLanguagelevels.Remove(existentModernLanguageLevels);
                }
            }

            if (modernLanguagelevels.Any())
            {
                _appDbContext.ModernLanguageLevels.RemoveRange(modernLanguagelevels);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
