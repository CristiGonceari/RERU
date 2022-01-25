using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.AddDocumentTemplate
{
    public class AddDocumentTemplateCommandHandler : IRequestHandler<AddDocumentTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddDocumentTemplateCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var mapperData = _mapper.Map<DocumentTemplate>(request.Data);

            await _appDbContext.DocumentTemplates.AddAsync(mapperData);
            await _appDbContext.SaveChangesAsync();

            return mapperData.Id;
        }
    }
}
