using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.EditLocation
{
    public class EditLocationCommandHandler : IRequestHandler<EditLocationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EditLocationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(EditLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToEdit = await _appDbContext.Locations.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, locationToEdit);
            await _appDbContext.SaveChangesAsync();

            return locationToEdit.Id;
        }
    }
}
