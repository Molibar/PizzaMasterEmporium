using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PizzaMasterEmporium.Framework.DataAccess
{
    public interface IFileSerializer
    {
        void Store<T>(string fileFullPath, T content);
        T Load<T>(string fileFullPath);
        T Load<T>(Stream inputJsonStream);
        Stream GetStream<T>(T content);
        string ContentType { get; }
        string FileExtension { get; }
    }


    public class XmlFileSerializer : IFileSerializer
    {
        public void Store<T>(string fileFullPath, T content)
        {
            using (TextWriter tw = new StreamWriter(fileFullPath))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                xmlSerializer.Serialize(tw,content);
            }
        }

        public T Load<T>(string fileFullPath)
        {
            if (!File.Exists(fileFullPath))
                throw new ArgumentException("fileFullPath");

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (TextReader tr = new StreamReader(fileFullPath))
            {
                return (T)xmlSerializer.Deserialize(tr);
            }
        }

        public T Load<T>(Stream inputXmlStream)
        {
            if (inputXmlStream == null || !inputXmlStream.CanRead)
                throw new ArgumentException("inputXmlStream");

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (TextReader tr = new StreamReader(inputXmlStream))
                {
                    return (T)xmlSerializer.Deserialize(tr);
                }
            }
            catch (Exception  exception)
            {
                throw new InvalidDataException(exception.Message, exception);
            }
        }

        public Stream GetStream<T>(T content)
        {
            var ms = new MemoryStream();
            var sr = new StreamWriter(ms);
            var xmlSerializer = new XmlSerializer(typeof (T));
            xmlSerializer.Serialize(sr,content);
            sr.Flush();
            ms.Position = 0;
            return ms;
        }

        public string ContentType
        {
            get { return "application/xml"; }
        }

        public string FileExtension
        {
            get { return "xml"; }
        }
    }


    public class JsonFileSerializer : IFileSerializer
    {
        public void Store<T>(string fileFullPath, T content)
        {
            using (TextWriter tw = new StreamWriter(fileFullPath))
            {
                var json = JsonSerializer.Create(new JsonSerializerSettings(){ });
                json.Serialize(tw, content);
            }
        }
        
        public T Load<T>(string fileFullPath)
        {
            if(!File.Exists(fileFullPath))
                throw new ArgumentException("fileFullPath");

            var json = JsonSerializer.Create(new JsonSerializerSettings());
            using (TextReader tr = new StreamReader(fileFullPath))
            {
                using (var jsonReader = new JsonTextReader(tr))
                {
                    return json.Deserialize<T>(jsonReader);
                }
            }
        }

        public T Load<T>(Stream inputJsonStream)
        {
            if (inputJsonStream == null || !inputJsonStream.CanRead)
                throw new ArgumentException("inputJsonStream");

            try
            {
                var json = JsonSerializer.Create(new JsonSerializerSettings());
                using (TextReader tr = new StreamReader(inputJsonStream))
                {
                    using (var jsonReader = new JsonTextReader(tr))
                    {
                        return json.Deserialize<T>(jsonReader);
                    }
                }

            }
            catch (JsonReaderException exception)
            {
                throw new InvalidDataException(exception.Message,exception);
            }
        }

        public Stream GetStream<T>(T content)
        {
            var ms = new MemoryStream();
            var sr = new StreamWriter(ms);
            var json = JsonSerializer.Create(new JsonSerializerSettings());
            json.Serialize(sr,content);
            sr.Flush();
            ms.Position = 0;
            return ms;
        }

        public string ContentType
        {
            get { return "application/json"; }
        }

        public string FileExtension
        {
            get { return "json"; }
        }
    }
}
