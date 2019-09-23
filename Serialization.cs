using System;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace OneZip
{
    class Serialization
    {
        /// <summary>
        /// Serialize an object to yaml document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="target"></param>
        public static bool YamlSerialize<T>(string filePath, T target)
        {
            var serializer = new SerializerBuilder().EmitDefaults().Build();
            try
            {
                var yaml = serializer.Serialize(target);
                File.WriteAllText(filePath, yaml);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deserialize an object from yaml document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T YamlDeserialize<T>(string filePath) where T : class
        {
            var deserializer = new DeserializerBuilder().Build();
            var yaml = File.ReadAllText(filePath);
            T target = deserializer.Deserialize<T>(yaml);
            if (target == null)
            {
                throw new YamlException("The document is empty");
            }
            return target;
        }
    }
}