using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Company.Function
{
    public static class Messages
    {

        [FunctionName("messages")]
        public static Task SendMessage(
              [HttpTrigger(AuthorizationLevel.Anonymous, "post")] MessageModel message,
              [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            // THIS MESSAGE WILL BE RECEIVED BY THE LISTENERS THAT NEGOCIATED BEFORE WITH THIS SERVICE
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] { message.text }
                });
        }
    }
}