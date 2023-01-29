using System.Threading.Tasks;
using Common;
using MassTransit;

namespace Consumer
{
    public class QueueMessageConsumer : IConsumer<QueueMessage>
    {
        public Task Consume(ConsumeContext<QueueMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}