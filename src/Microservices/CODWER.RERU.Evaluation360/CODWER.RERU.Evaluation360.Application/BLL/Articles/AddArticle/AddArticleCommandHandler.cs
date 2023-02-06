using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.StorageService;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;
using CODWER.RERU.Evaluation360.Application.BLL.Services;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.AddArticle
{
    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;
        private readonly IAssignRolesToArticle _assignRolesToArticle;

        public AddArticleCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService, IAssignRolesToArticle assignRolesToArticle)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
            _assignRolesToArticle = assignRolesToArticle;
        }

        public async Task<int> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var storage = await _storageFileService.AddFile(request.FileDto);
            //var storage = "0000000-0000000";

            if (string.IsNullOrEmpty(storage)) storage = null;

            var newArticle = new ArticleEv360Dto()
            {
                Name = request.Name,
                Content = request.Content,
                MediaFileId = storage,
            };

            var articleToCreate = _mapper.Map<ArticleEv360>(newArticle);

            await _appDbContext.Ev360Articles.AddAsync(articleToCreate);

            await _appDbContext.SaveChangesAsync();

            await _assignRolesToArticle.AssignRolesToArticle(request.Roles, articleToCreate.Id);

            return articleToCreate.Id;
        }
    }
}
