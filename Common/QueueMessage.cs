using System;

namespace Common
{
    public class QueueMessage
    {
        public string Type => "QueueMessage";

        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
