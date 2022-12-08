using System;
using CODWER.RERU.Core.Application.Addresses.AddAddress;
using CODWER.RERU.Core.Application.Addresses.GetAddress;
using CODWER.RERU.Core.Application.Addresses.RemoveAddress;
using CODWER.RERU.Core.Application.Addresses.UpdateAddress;
using CODWER.RERU.Core.DataTransferObjects.Address;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        public AddressController(IMediator mediator) : base(mediator) { }

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
