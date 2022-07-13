using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.UpdateUserProfileGeneralData
{
    public class UpdateUserProfileGeneralDataCommandHandler : IRequestHandler<UpdateUserProfileGeneralDataCommand, Unit>
    {
        public readonly AppDbContext _appDbContext;
        public readonly IMapper _mapper;
        public UpdateUserProfileGeneralDataCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserProfileGeneralDataCommand request, CancellationToken cancellationToken)
        {
            var generalData = await _appDbContext.UserProfileGeneralDatas.FirstOrDefaultAsync(b => b.Id == request.Data.Id);

            _mapper.Map(request.Data, generalData);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
