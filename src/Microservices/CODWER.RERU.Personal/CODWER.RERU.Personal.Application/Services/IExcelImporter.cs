using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IExcelImporter
    {
        public Task Import(IFormFile data);
    }
}
