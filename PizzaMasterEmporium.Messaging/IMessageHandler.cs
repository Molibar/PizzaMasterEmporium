using System.Collections.Generic;
using PizzaMasterEmporium.Messaging.Models;

namespace PizzaMasterEmporium.Messaging
{
    public interface IMessageHandler
    {
        bool[] Handle(IList<DequeuedMessage> dequeuedMessages);
        string[] RoutingKeys { get; }
    }
}