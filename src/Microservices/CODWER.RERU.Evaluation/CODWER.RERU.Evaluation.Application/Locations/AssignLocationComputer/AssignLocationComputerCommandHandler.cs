using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.AssignLocationComputer
{
    public class AssignLocationComputerCommandHandler : IRequestHandler<AssignLocationComputerCommand, string>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignLocationComputerCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(AssignLocationComputerCommand request, CancellationToken cancellationToken)
        {
            var clientToAdd = _mapper.Map<LocationClient>(request.Data);
            clientToAdd.Token = GenerateToken(request.Data.LocationId, request.Data.Number);

            await _appDbContext.LocationClients.AddAsync(clientToAdd);
            await _appDbContext.SaveChangesAsync();

            return clientToAdd.Token;
        }

        private string GenerateToken(int locationId, int number)
        {
            var locationClientToken = new LocationClientTokenDto()
            {
                LocationId = locationId,
                ComputerNumber = number
            };

            var tokenJson = JsonConvert.SerializeObject(locationClientToken);

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenJson));
        }
    }
}
