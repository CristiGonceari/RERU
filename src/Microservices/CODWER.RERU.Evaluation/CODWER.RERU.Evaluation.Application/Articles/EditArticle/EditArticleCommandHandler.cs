using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles.EditArticle
{
    public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;

        public EditArticleCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
        }

        public async Task<int> Handle(EditArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.EvaluationArticles.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, article);

            if (request.Data.FileDto != null)
            {
                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                article.MediaFileId = addFile;
            }
            else
            {
                article.MediaFileId = request.Data.MediaFileId;
            }

            await _appDbContext.SaveChangesAsync();

            return article.Id;
        }
    }
}
