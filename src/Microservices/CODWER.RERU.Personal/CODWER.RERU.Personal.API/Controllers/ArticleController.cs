using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CODWER.RERU.Personal.Application.Articles.GetArticle;
using CODWER.RERU.Personal.Application.Articles.GetArticles;
using CODWER.RERU.Personal.Application.Articles.AddEditArticle;
using CODWER.RERU.Personal.Application.Articles.RemoveArticle;
using CODWER.RERU.Personal.Application.Articles.PrintArticles;

namespace CODWER.RERU.Personal.API.Controllers
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
            return await Mediator.Send(new RemoveArticleCommand { Id = id });
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
