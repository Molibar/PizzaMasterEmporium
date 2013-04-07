using System;
using System.Text;
using Newtonsoft.Json;

namespace PizzaMasterEmporium.Messaging
{
    public static class DataSerializerExtensions
    {
        public static byte[] Serialize(this object data)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public static T Deserialize<T>(this byte[] body)
        {
            var jsonString = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T Deserialize<T>(this byte[] body, Type t)
        {
            var jsonString = Encoding.UTF8.GetString(body);
            return (T)JsonConvert.DeserializeObject(jsonString, t);
        }
    }
}
