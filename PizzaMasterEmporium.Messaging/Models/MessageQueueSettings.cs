namespace PizzaMasterEmporium.Messaging.Models
{
    public class MessageQueueSettings
    {
        public string HostName { get; set; }
        public int Timeout { get; set; }
    }
}