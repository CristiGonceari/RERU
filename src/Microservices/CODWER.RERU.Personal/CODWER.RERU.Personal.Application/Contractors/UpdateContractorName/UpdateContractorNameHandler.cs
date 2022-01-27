using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractorName
{
    public class UpdateContractorNameHandler : IRequestHandler<UpdateContractorNameCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateContractorNameHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContractorNameCommand request, CancellationToken cancellationToken)
        {
            var contractorToUpdate = await _appDbContext.Contractors.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, contractorToUpdate);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
