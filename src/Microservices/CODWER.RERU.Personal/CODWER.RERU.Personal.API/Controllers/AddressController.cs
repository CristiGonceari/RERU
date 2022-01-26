using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Addresses.AddAddress;
using CODWER.RERU.Personal.Application.Addresses.GetAddress;
using CODWER.RERU.Personal.Application.Addresses.RemoveAddress;
using CODWER.RERU.Personal.Application.Addresses.UpdateAddress;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Address;
using CVU.ERP.Module.Application.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController
    {
        private readonly ICoreClient _coreClient;
        private readonly AppDbContext _appDbContext;

        public AddressController(ICoreClient coreClient,AppDbContext dbContext)
        {
            _coreClient = coreClient;
            _appDbContext = dbContext;
        }

        //[HttpGet("getalluserProfiles")]
        //public async Task<string> GetAllUserProfiles([FromRoute] string id)
        //{
        //    var result = _appDbContext.UserProfiles
        //        .Include(x => x.Contractor).Select(up =>
        //            $"Contractor data : {up.ContractorId} {up.Contractor.FirstName} {up.Contractor.LastName} \nCoreUser data {up.UserId} {up.Email}")
        //        .ToList();

        //    return string.Join("\n", result);
        //}

        [HttpGet("getcore/{id}")]
        public async Task<bool> GetCore([FromRoute] string id)
        {
            return await _coreClient.ExistUserInCore(id);
        }

        [HttpGet("{id}")]
        public async Task<AddressDto> GetAddress([FromRoute] int id)
        {
            var query = new GetAddressQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateAddress([FromBody] AddAddressCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateAddress([FromBody] UpdateAddressCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveAddress([FromRoute] int id)
        {
            var command = new RemoveAddressCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
