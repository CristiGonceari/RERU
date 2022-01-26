using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Contacts.AddContact;
using CODWER.RERU.Personal.Application.Contacts.GetContact;
using CODWER.RERU.Personal.Application.Contacts.GetContacts;
using CODWER.RERU.Personal.Application.Contacts.RemoveContact;
using CODWER.RERU.Personal.Application.Contacts.UpdateContact;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ContactDto>> GetContacts([FromQuery] GetContactsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ContactDto> GetContact([FromRoute] int id)
        {
            var query = new GetContactQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateContact([FromBody] AddContactCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateContact([FromBody] UpdateContactCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveContact([FromRoute] int id)
        {
            var command = new RemoveContactCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
