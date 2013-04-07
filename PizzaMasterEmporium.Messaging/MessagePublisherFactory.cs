using System;
using PizzaMasterEmporium.Framework.Logging;
using PizzaMasterEmporium.Messaging.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace PizzaMasterEmporium.Messaging
{
    public interface IMessagePublisherFactory
    {
        MessagePublisher Build();
    }

    public class MessagePublisherFactory : IMessagePublisherFactory
    {
        private readonly ILogger _logger;
        private ConnectionFactory _connectionFactory;

        public MessagePublisherFactory(ConnectionFactory connectionFactory, ILogger logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public MessagePublisher Build()
        {
            IConnection connection;
            try
            {
                connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex)
            {
                _logger.LogErrorMessage(GetType(), ex, "Connection factory couldn't create connection!");
                return new MessagePublisher(null, null, null, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogErrorMessage(GetType(), ex, "Missing RabbitMQ_HostName in the config file?");
                return new MessagePublisher(null, null, null, _logger);
            }
            var model = connection.CreateModel();
            model.EnsureExchange();

            var messagePublisher = new MessagePublisher(connection, model, MessageDataConstants.EXCHANGE, _logger);
            return messagePublisher;
        }
    }
}