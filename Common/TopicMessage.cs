using System;

namespace Common
{
    public class TopicMessage
    {
        public string Type => "TopicMessage";
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
