using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Address;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Addresses.GetAddress
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, AddressDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAddressQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<AddressDto> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Addresses.FirstAsync(a => a.Id == request.Id);
            var mappedItem = _mapper.Map<AddressDto>(item);

            return mappedItem;
        }
    }
}
