using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.Application.BLL.Services;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.EditArticle
{
    public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;
        private readonly IAssignRolesToArticle _assignRolesToArticle;

        public EditArticleCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService, IAssignRolesToArticle assignRolesToArticle)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
            _assignRolesToArticle = assignRolesToArticle;
        }

        public async Task<int> Handle(EditArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.CoreArticles.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

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

            await _assignRolesToArticle.AssignRolesToArticle(request.Data.Roles, article.Id);

            return article.Id;
        }
    }
}
