using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Addresses.AddAddress
{
    public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddAddressCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var mappedData = _mapper.Map<Address>(request.Data);

            _appDbContext.Addresses.Add(mappedData);
            await _appDbContext.SaveChangesAsync();

            return mappedData.Id;
        }
    }
}
