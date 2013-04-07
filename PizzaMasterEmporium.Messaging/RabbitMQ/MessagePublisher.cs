using System;
using PizzaMasterEmporium.Framework.Logging;
using PizzaMasterEmporium.Messaging.Models;
using RabbitMQ.Client;

namespace PizzaMasterEmporium.Messaging.RabbitMQ
{
    public class MessageDataConstants
    {
        public const string EXCHANGE = "TotallyMoney.MoneyMatch.DataExchange";
    }

    public interface IMessagePublisher
    {
        void Publish<T>(T data);
    }

    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly string _exchange;
        private readonly ILogger _logger;

        public MessagePublisher(IConnection connection, IModel model, string exchange, ILogger logger)
        {
            _connection = connection;
            _model = model;
            _exchange = exchange;
            _logger = logger;
        }


        private delegate void PublishDelegate(string exchange, string routingKey, IBasicProperties basicProperties, byte[] bytes);

        public void Publish<T>(T data)
        {
            // If connection to broker was broken then we don't want to throw an exception.
            // The error should be logged in the MessagePublisherFactory
            if (_connection == null)
            {
                _logger.LogErrorMessage(GetType(),
                    "Can't publish message of type {0} because of missing connection",
                    data.GetType().Name);
                return;
            }
            var routingKey = GetRoutingKey(data);
            var message = new Message<T> {DateTime = DateTime.Now, Data = data };
            var bytes = message.Serialize();
            var basicProperties = _model.CreateBasicProperties();
            // Makes sure the messages are stored to disk so that if the
            // queue goes down the messages in it won't disappear.
            basicProperties.SetPersistent(true);

            var publishDelegate = (PublishDelegate)_model.BasicPublish;
            publishDelegate.BeginInvoke(_exchange, routingKey, basicProperties, bytes, null, null);
        }

        private static string GetRoutingKey<T>(T data)
        {
            return data.GetType().Name;
        }

        public void Dispose()
        {
            if ((_connection != null) && _connection.IsOpen) _connection.Close();
            if (_model != null) _model.Abort();
        }
    }
}