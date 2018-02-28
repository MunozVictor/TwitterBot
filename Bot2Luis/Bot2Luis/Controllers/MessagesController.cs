using Bot2Luis.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bot2Luis.Controllers
{
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage>Post([FromBody] Activity activity)
        {
            if(activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity,() => new RootLuisDialog());
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

    }
}
