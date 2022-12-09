using System;
using System.Collections.Generic;
using CODWER.RERU.Core.Application.Addresses.AddAddress;
using CODWER.RERU.Core.Application.Addresses.GetAddress;
using CODWER.RERU.Core.Application.Addresses.RemoveAddress;
using CODWER.RERU.Core.Application.Addresses.UpdateAddress;
using CODWER.RERU.Core.DataTransferObjects.Address;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Age.Integrations.MNotify.Models;

namespace CODWER.RERU.Core.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        private readonly IMNotifyClient _mNotifyClient;

        public AddressController(IMediator mediator, IMNotifyClient mNotifyClient) : base(mediator)
        {
            _mNotifyClient = mNotifyClient;
        }

        [HttpGet("test")]
        public async Task<string> SendMail()
        {
            var notif = new NotificationRequest
            {
                Body = new NotificationContent { Romanian = "test content" },
                Recipients = new List<NotificationRecipient>()
                {
                    new NotificationRecipient { Type = NotificationRecipientType.Email, Value = "hubencu.andrian@gmail.com" }
                },
                Priority = NotificationPriority.Medium,
                ShortBody = new NotificationContent { Romanian = "short body test" },
                Subject = new NotificationContent { Romanian = "subject teest" }
            };

            try
            {
                await _mNotifyClient.SendNotification(notif);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return string.Empty;
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
