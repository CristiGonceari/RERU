using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Articles.AddArticle;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CODWER.RERU.Personal.Application.Articles.GetArticle;
using CODWER.RERU.Personal.Application.Articles.GetArticles;
using CODWER.RERU.Personal.Application.Articles.DeleteArticle;
using CODWER.RERU.Personal.Application.Articles.EditArticle;
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
