using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.AddEditRequiredDocument
{
    public class AddEditRequiredDocumentsCommandHandler : IRequestHandler<AddEditRequiredDocumentsCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEditRequiredDocumentsCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddEditRequiredDocumentsCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.RequiredDocuments
                .FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            if (item != null)
            {
                _mapper.Map(request.Data, item);
                await _appDbContext.SaveChangesAsync();

                return item.Id;
            }

            var newItem = _mapper.Map<RequiredDocument>(request.Data);

            await _appDbContext.RequiredDocuments.AddAsync(newItem);
            await _appDbContext.SaveChangesAsync();

            return newItem.Id;
        }
    }
}
