using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation.API.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using CODWER.RERU.Evaluation.Application.Articles.GetArticle;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Evaluation.Application.Articles.GetArticles;
using CODWER.RERU.Evaluation.Application.Articles.AddEditArticle;
using MediatR;
using CODWER.RERU.Evaluation.Application.Articles.DeleteArticle;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ArticleDto> GetArticle([FromRoute] int id)
        {
            return await Mediator.Send(new GetArticleQuery { Id = id });
        }

        [HttpGet()]
        public async Task<PaginatedModel<ArticleDto>> GetArticles([FromQuery] GetArticlesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddEditArticle([FromBody] ArticleDto request)
        {
            return await Mediator.Send(new AddEditArticleCommand { Data = request });
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteArticle([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteArticleCommand { Id = id });
        }
    }
}
