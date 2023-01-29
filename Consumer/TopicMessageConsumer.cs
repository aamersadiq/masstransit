using Common;
using MassTransit;
using System.Threading.Tasks;

namespace Consumer
{
    public class TopicMessageConsumer : IConsumer<TopicMessage>
    {
        public Task Consume(ConsumeContext<TopicMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
