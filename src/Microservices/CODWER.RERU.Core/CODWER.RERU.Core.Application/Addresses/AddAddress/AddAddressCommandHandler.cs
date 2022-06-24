﻿using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Addresses.AddAddress
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
