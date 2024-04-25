using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;


namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("BOT")]

    public class BOTController : ControllerBase
    {
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("TeamsMessage/Post")]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new(new Uri(activity.ServiceUrl));
                Activity reply = activity.CreateReply($"You sent {activity.Text} which was {activity.Text.Length} characters");
                var msgToUpdate = await connector.Conversations.ReplyToActivityAsync(reply);
                Activity updatedReply = activity.CreateReply($"This is an updated message");
                await connector.Conversations.UpdateActivityAsync(reply.Conversation.Id, msgToUpdate.Id, updatedReply);
            }
            return new HttpResponseMessage();
        }
    }
}
