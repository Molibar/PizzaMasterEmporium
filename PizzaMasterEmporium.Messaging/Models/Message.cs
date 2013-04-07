using System;

namespace PizzaMasterEmporium.Messaging.Models
{
    public class Message<T>
    {
        public DateTime DateTime { get; set; }
        public T Data { get; set; }
    }
}