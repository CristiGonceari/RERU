using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestType
{
    public class EditTestTypeCommandHandler : IRequestHandler<EditTestTypeCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EditTestTypeCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditTestTypeCommand request, CancellationToken cancellationToken)
        {
            var updateTestType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, updateTestType);
            await _appDbContext.SaveChangesAsync();

            return updateTestType.Id;
        }
    }
}
