using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Address;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Addresses.GetAddress
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
