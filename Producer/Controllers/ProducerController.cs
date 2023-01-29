using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Producer.Controllers
{

    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProducerController(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        [Route("sendtoqueue")]
        [HttpGet()]
        public async Task<IActionResult> SendToQueue()
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{AppSettings.QueueName}"));
            var message = new QueueMessage { Id = Guid.NewGuid().ToString(), CreatedDateTime = DateTime.Now };
            await endpoint.Send(message, message.GetType());

            return Ok();
        }

        [Route("publishtotopic")]
        [HttpGet()]
        public async Task<IActionResult> PublishToTopic()
        {
            var message = new TopicMessage() { Id = Guid.NewGuid().ToString(), CreatedDateTime = DateTime.Now };
            await _publishEndpoint.Publish(message, message.GetType());

            return Ok();
        }


    }

  
}
