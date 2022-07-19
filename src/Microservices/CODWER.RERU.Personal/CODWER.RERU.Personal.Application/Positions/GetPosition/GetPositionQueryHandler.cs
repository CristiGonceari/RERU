using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.GetPosition
{
    public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;

        public GetPositionQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
        }

        public async Task<PositionDto> Handle(GetPositionQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Positions
                .Include(x=>x.Department)
                .Include(x=>x.Role)
                .Include(x => x.Contractor)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<PositionDto>(item);

            mappedItem.OrderName = await _storageFileService.GetFileName(mappedItem.OrderId);

            return mappedItem;
        }
    }
}
