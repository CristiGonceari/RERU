using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Instructions.UpdateInstruction
{
    public class UpdateInstructionCommandHandler : IRequestHandler<UpdateInstructionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateInstructionCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateInstructionCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Instructions.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
