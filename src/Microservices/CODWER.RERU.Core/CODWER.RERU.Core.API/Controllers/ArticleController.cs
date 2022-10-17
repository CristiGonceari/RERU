using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Articles.AddArticle;
using CVU.ERP.Common.Pagination;
using MediatR;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CODWER.RERU.Core.Application.Articles.GetArticle;
using CODWER.RERU.Core.Application.Articles.GetArticles;
using CODWER.RERU.Core.Application.Articles.DeleteArticle;
using CODWER.RERU.Core.Application.Articles.EditArticle;
using CODWER.RERU.Core.Application.Articles.PrintArticles;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        public ArticleController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<ArticleCoreDto> GetArticle([FromRoute] int id)
        {
            return await Mediator.Send(new GetArticleQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<ArticleCoreDto>> GetArticles([FromQuery] GetArticlesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddArticle([FromForm] AddArticleCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<int> EditArticle([FromForm] EditArticleCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteArticle([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteArticleCommand { Id = id });
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintArticlesPdf([FromBody] PrintArticlesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
