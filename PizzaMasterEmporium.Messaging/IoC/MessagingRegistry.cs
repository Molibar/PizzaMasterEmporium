using System.Configuration;
using AutoMapper;
using PizzaMasterEmporium.Framework.Helpers;
using PizzaMasterEmporium.Framework.IoC.StructureMap;
using PizzaMasterEmporium.Framework.Logging;
using PizzaMasterEmporium.Messaging.Models;
using PizzaMasterEmporium.Messaging.RabbitMQ;
using RabbitMQ.Client;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.Messaging.IoC
{
    public class MessagingRegistry : Registry
    {

        public MessagingRegistry()
        {
            ObjectFactory.Configure(
                cfg =>
                    {
                        For<MessageQueueSettings>().Singleton()
                            .Use(() => new MessageQueueSettings
                            {
                                HostName = ConfigurationManager.AppSettings["RabbitMQ_HostName"],
                                Timeout =  Converter.ToInt32(ConfigurationManager.AppSettings["RabbitMQ_Timeout"], 600)
                            });
                        For<ConnectionFactory>().Singleton()
                            .Use(() =>
                            {
                                var messagePublisherSettings = ObjectFactory.GetInstance<MessageQueueSettings>();
                                Log.InfoMessage(GetType(), "RabbitMQ_HostName: {0}", messagePublisherSettings.HostName);
                                Log.InfoMessage(GetType(), "RabbitMQ_Timeout: {0}", messagePublisherSettings.Timeout);
                                var connectionFactory = new ConnectionFactory
                                {
                                    HostName = messagePublisherSettings.HostName,
                                    RequestedConnectionTimeout = messagePublisherSettings.Timeout
                                };
                                return connectionFactory;
                            });
                        For<IMessagePublisher>()
                            .LifecycleIs(new UnitOfWorkLifecycle())
                            .Use(() =>
                            {
                                var messagePublisherFactory = ObjectFactory.GetInstance<IMessagePublisherFactory>();
                                return messagePublisherFactory.Build();
                            });

                    cfg.Scan(scan =>
                                 {
                                     scan.AddAllTypesOf<Profile>();
                                     scan.TheCallingAssembly();
                                     scan.WithDefaultConventions();
                                 });
                    });
        }
    }
}