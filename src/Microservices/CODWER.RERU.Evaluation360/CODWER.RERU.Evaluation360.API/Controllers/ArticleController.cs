using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticle;
using CODWER.RERU.Evaluation360.Application.BLL.Articles.DeleteArticle;
using CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticles;
using CODWER.RERU.Evaluation360.Application.BLL.Articles.AddArticle;
using CODWER.RERU.Evaluation360.Application.BLL.Articles.EditArticle;
using CODWER.RERU.Evaluation360.Application.BLL.Articles.PrintArticles;
using CODWER.RERU.Evaluation360.API.Config;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ArticleCoreDto> GetArticle([FromRoute] int id)
        {
            return await Sender.Send(new GetArticleQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<ArticleCoreDto>> GetArticles([FromQuery] GetArticlesQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpPost]
        public async Task<int> AddArticle([FromForm] AddArticleCommand command)
        {
            return await Sender.Send(command);
        }

        [HttpPatch]
        public async Task<int> EditArticle([FromForm] EditArticleCommand command)
        {
            return await Sender.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteArticle([FromRoute] int id)
        {
            return await Sender.Send(new DeleteArticleCommand { Id = id });
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintArticlesPdf([FromBody] PrintArticlesCommand command)
        {
            var result = await Sender.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}