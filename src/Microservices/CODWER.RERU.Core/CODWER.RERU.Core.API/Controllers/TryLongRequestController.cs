using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TryLongRequestController : BaseController
    {
        public TryLongRequestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public string GetUserDetails()
        {
           int sqh;

            for (int i = 0; i < 1000000000; i++)
            {
                var h = i;
                var p = i + 1;
                var b = i + 2;


                sqh = p * p + b * b;

                h = (int) Math.Sqrt(sqh);
            }


            Thread.Sleep(300000);
            return "Job Done";
        }
    }
}
