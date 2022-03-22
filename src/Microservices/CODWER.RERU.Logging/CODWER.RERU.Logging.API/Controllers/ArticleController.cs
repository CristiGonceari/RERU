using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Logging.API.Config;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CODWER.RERU.Logging.DataTransferObjects;
using CODWER.RERU.Logging.Application.Articles.GetArticle;
using CODWER.RERU.Logging.Application.Articles.GetArticles;
using CODWER.RERU.Logging.Application.Articles.AddEditArticle;
using CODWER.RERU.Logging.Application.Articles.DeleteArticle;
using CODWER.RERU.Logging.Application.Articles.PrintArticles;

namespace CODWER.RERU.Logging.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ArticleDto> GetArticle([FromRoute] int id)
        {
            return await Mediator.Send(new GetArticleQuery { Id = id });
        }

        [HttpGet]
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
