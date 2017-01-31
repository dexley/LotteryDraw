using SIS.Lottery.Api.Application;
using SIS.Lottery.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace SIS.Lottery.Api.Controllers
{
    [RoutePrefix("v1/api/lottery")]
    public class LotteryController : ApiController
    {
        public ILotteryService LotteryService { get; set; }

        [HttpGet]
        public async Task<HttpResponseMessage> Search([FromUri]DateTime date)
        {
            var matches = await LotteryService.SearchDraws(date);
            return Request.CreateResponse(HttpStatusCode.OK, matches);            
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(LotteryDraw draw)
        {
            var commandResult = await LotteryService.UpdateDraw(draw);

            if (commandResult == CommandResult.NotFound) return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(commandResult == CommandResult.Invalid ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(LotteryDraw draw)
        {
            var commandResult = await LotteryService.CreateDraw(draw);

            return Request.CreateResponse(commandResult == CommandResult.AlreadyExists ? HttpStatusCode.BadRequest : HttpStatusCode.Created);
        }
    }
}
