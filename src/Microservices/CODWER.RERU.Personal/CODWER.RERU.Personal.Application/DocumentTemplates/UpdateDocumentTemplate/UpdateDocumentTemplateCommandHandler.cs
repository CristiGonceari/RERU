using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.UpdateDocumentTemplate
{
    public class UpdateDocumentTemplateCommandHandler : IRequestHandler<UpdateDocumentTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateDocumentTemplateCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.DocumentTemplates.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
