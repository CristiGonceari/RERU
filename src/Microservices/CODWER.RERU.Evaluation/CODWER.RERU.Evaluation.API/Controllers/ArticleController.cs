using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation.API.Config;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Articles.AddArticle;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using CODWER.RERU.Evaluation.Application.Articles.GetArticle;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Evaluation.Application.Articles.GetArticles;
using MediatR;
using CODWER.RERU.Evaluation.Application.Articles.DeleteArticle;
using CODWER.RERU.Evaluation.Application.Articles.EditArticle;
using CODWER.RERU.Evaluation.Application.Articles.PrintArticles;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ArticleEvaluationDto> GetArticle([FromRoute] int id)
        {
            return await Mediator.Send(new GetArticleQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<ArticleEvaluationDto>> GetArticles([FromQuery] GetArticlesQuery query)
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
