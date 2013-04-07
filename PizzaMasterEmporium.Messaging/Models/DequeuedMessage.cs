namespace PizzaMasterEmporium.Messaging.Models
{
    public class DequeuedMessage
    {
        public string RoutingKey { get; set; }
        public ulong DeliveryTag { get; set; }
        public byte[] Body { get; set; }
    }
}