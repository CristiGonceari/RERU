using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.AddUserProfileGeneralData
{
    public class AddUserProfileGeneralDataCommandHandler : IRequestHandler<AddUserProfileGeneralDataCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddUserProfileGeneralDataCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddUserProfileGeneralDataCommand request, CancellationToken cancellationToken)
        {
            var mappedUserProfileGeneralData = _mapper.Map<UserProfileGeneralData>(request.Data);

            await _appDbContext.UserProfileGeneralDatas.AddAsync(mappedUserProfileGeneralData);
            await _appDbContext.SaveChangesAsync();

            return mappedUserProfileGeneralData.Id;
        }
    }
}
