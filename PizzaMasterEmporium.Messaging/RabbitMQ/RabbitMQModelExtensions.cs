using System.Collections.Generic;
using RabbitMQ.Client;

namespace PizzaMasterEmporium.Messaging.RabbitMQ
{
    public static class RabbitMQModelExtensions
    {
        public static void EnsureExchange(this IModel model)
        {
            model.ExchangeDeclare(MessageDataConstants.EXCHANGE, ExchangeType.Topic, true, false, new Dictionary<string, string>());
        }
         
        public static string EnsureQueue(this IModel model, string exchangeName, string queueName, string[] routingKeys, ushort maxMessagesInBulk)
        {
            // If queueName == empty string the generated name will be returned.
            model.BasicQos(0, maxMessagesInBulk, false);
            var usedQueueName = model.QueueDeclare(queueName, true, false, false, null).QueueName;
            foreach (var routingKey in routingKeys)
            {
                model.QueueBind(usedQueueName, exchangeName, routingKey, null);    
            }
            
            return usedQueueName;
        }
    }
}