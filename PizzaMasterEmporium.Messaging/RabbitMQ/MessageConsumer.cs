using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using PizzaMasterEmporium.Framework.Logging;
using PizzaMasterEmporium.Messaging.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace PizzaMasterEmporium.Messaging.RabbitMQ
{
    public class 
        MessageConsumer : IDisposable
    {
        private const int RETRY_SLEEP_TIME_MS = 15000;

        private readonly ushort _maxMessagesInBulk;

        private IModel _model;
        private IConnection _connection;
        private string _queueName;
        private readonly string[] _routingKeys;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMessageHandler _messageHandler;
        private readonly ILogger _logger;
        protected bool IsConsuming;
        protected bool IsInitializing;

        public MessageConsumer(ConnectionFactory connectionFactory, IMessageHandler messageHandler, 
            string queueName, ushort maxMessagesInBulk, ILogger logger)
        {
            _connectionFactory = connectionFactory;
            _queueName = queueName;
            _routingKeys = messageHandler.RoutingKeys;
            _maxMessagesInBulk = maxMessagesInBulk;
            _messageHandler = messageHandler;
            _logger = logger;
        }

        public void Initialize()
        {
            IsInitializing = true;
            while (IsInitializing)
            {
                EnsureEverythingIsClosed();
                try
                {
                    _logger.LogInfoMessage(GetType(),
                                    "Initializing MessageConsumer to point at queue {0} at {1} for MessageHandler {2} with RoutingKeys <{3}> for ThreadId <{4}>",
                                    _queueName, _connectionFactory.HostName, _messageHandler.GetType().Name, string.Join(";",_routingKeys), Thread.CurrentThread.ManagedThreadId);
                    _connection = _connectionFactory.CreateConnection();
                    _model = _connection.CreateModel();
                    _model.EnsureExchange();
                    _queueName = _model.EnsureQueue(MessageDataConstants.EXCHANGE, _queueName, _routingKeys, _maxMessagesInBulk);
                    
                    IsInitializing = false;
                }
                catch (Exception ex)
                {
                    if (ex is BrokerUnreachableException)
                    {
                        _logger.LogErrorMessage(
                            GetType(), ex,
                            "Connection broken when Initializing MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                            _messageHandler.GetType().Name, string.Join(";", _routingKeys),
                            Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        _logger.LogErrorMessage(
                            GetType(), ex,
                            "Possible problem finding RabbitMQ_HostName in the config file when Initializing MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                            _messageHandler.GetType().Name, string.Join(";", _routingKeys), Thread.CurrentThread.ManagedThreadId);
                    }
                    // Wait a reasonable amount of time not to spam our poor exchange with calls.
                    Thread.Sleep(RETRY_SLEEP_TIME_MS);
                }
            }
        }


        // internal delegate to run the queue consumer on a seperate thread
        private delegate void ConsumeDelegate();

        public void StartConsuming()
        {
            Initialize();
            IsConsuming = true;
            var consumeDelegate = (ConsumeDelegate) Consume;
            var asyncResult = consumeDelegate.BeginInvoke(null, null);
            //asyncResult.AsyncWaitHandle.WaitOne();
        }

        private void Consume()
        {
            _logger.LogInfoMessage(GetType(),
                "Starting MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                _messageHandler.GetType().Name, string.Join(";", _routingKeys), Thread.CurrentThread.ManagedThreadId);
            QueueingBasicConsumer consumer = null;

            while (IsConsuming)
            {
                try
                {
                    if (consumer == null)
                    {
                        _logger.LogInfoMessage(GetType(),
                                               "Creating QueueingBasicConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                                               _messageHandler.GetType().Name, string.Join(";", _routingKeys),
                                               Thread.CurrentThread.ManagedThreadId);
                        consumer = new QueueingBasicConsumer(_model);
                        _model.BasicConsume(_queueName, false, consumer);
                    }
                    _logger.LogInfoMessage(GetType(),
                                           "Waiting in MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                                           _messageHandler.GetType().Name, string.Join(";", _routingKeys),
                                           Thread.CurrentThread.ManagedThreadId);

                    var dequeuedMessageList = new List<DequeuedMessage>();
                    var e = (BasicDeliverEventArgs) consumer.Queue.Dequeue();
                    var counter = 1;
                    do
                    {
                        dequeuedMessageList.Add(
                            new DequeuedMessage
                                {
                                    RoutingKey = e.RoutingKey,
                                    Body = e.Body,
                                    DeliveryTag = e.DeliveryTag
                                });
                        e = (BasicDeliverEventArgs) consumer.Queue.DequeueNoWait(null);
                    } while ((e != null) && (counter++ < _maxMessagesInBulk));

                    HandleDequeuedMessages(dequeuedMessageList);
                }
                catch (EndOfStreamException endOfStreamException)
                {
                    if (IsConsuming)
                    {
                        _logger.LogErrorMessage(GetType(), endOfStreamException,
                                     "Crashing MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                                     _messageHandler.GetType().Name, string.Join(";", _routingKeys), Thread.CurrentThread.ManagedThreadId);
                        consumer = null;
                        Initialize();
                    }
                    else
                    {
                        _logger.LogWarnMessage(GetType(), endOfStreamException,
                                     "MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> threw expected exception due to closing of connection. ThreadId <{2}>",
                                     _messageHandler.GetType().Name, string.Join(";", _routingKeys), Thread.CurrentThread.ManagedThreadId);
                        
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogErrorMessage(GetType(), ex,
                                     "Crashing MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                                     _messageHandler.GetType().Name, string.Join(";", _routingKeys), Thread.CurrentThread.ManagedThreadId);
                    consumer = null;
                    Initialize();
                }
            }
        }

        private void HandleDequeuedMessages(List<DequeuedMessage> dequeuedMessages)
        {
            var successful = _messageHandler.Handle(dequeuedMessages);

            for (var i = 0; i < successful.Length; i++)
            {
                if (successful[i])
                {
                    _model.BasicAck(dequeuedMessages[i].DeliveryTag, false);
                }
            }
            var countOfSuccessfulMessageHandlings = successful.Count(x => x);
            _logger.LogInfoMessage(GetType(),
                                    "Successfully handled {0} messages in MessageConsumer for MessageHandler {1} with last RoutingKeys <{2}> for ThreadId <{3}>",
                                    countOfSuccessfulMessageHandlings, _messageHandler.GetType().Name, string.Join(";", _routingKeys),
                                    Thread.CurrentThread.ManagedThreadId);

            var countOfUnSuccessfulMessageHandlings = dequeuedMessages.Count - countOfSuccessfulMessageHandlings;
            if (countOfUnSuccessfulMessageHandlings > 0)
            {
                for (var i = 0; i < successful.Length; i++)
                {
                    if (successful[i]) continue;
                    _logger.LogErrorMessage(
                        GetType(),
                        "MessageHandler: {0} RoutingKeys: <{1}>{2}Body: {3}",
                        _messageHandler.GetType().Name, string.Join(";", _routingKeys), Environment.NewLine,
                        Encoding.UTF8.GetString(dequeuedMessages[i].Body));
                    _model.BasicAck(dequeuedMessages[i].DeliveryTag, false);
                }
            }
        }

        private void EnsureEverythingIsClosed()
        {
            if (_connection != null)
            {
                if (_connection.IsOpen) _connection.Close();
                _connection = null;
            }
            if (_model != null)
            {
                _model.Abort();
                _model = null;
            }
        }

        public void Dispose()
        {
            _logger.LogInfoMessage(GetType(),
                "Disposing MessageConsumer for MessageHandler {0} with RoutingKeys <{1}> for ThreadId <{2}>",
                _messageHandler.GetType().Name, string.Join(";", _routingKeys), Thread.CurrentThread.ManagedThreadId);
            IsConsuming = false;
            IsInitializing = false;
            EnsureEverythingIsClosed();
        }

        public void StopConsuming()
        {
            Dispose();
        }
    }
}