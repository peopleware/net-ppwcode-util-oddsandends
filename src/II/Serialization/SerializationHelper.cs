// Copyright 2014 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

using PPWCode.Util.OddsAndEnds.II.Streaming;

namespace PPWCode.Util.OddsAndEnds.II.Serialization
{
    /// <summary>
    ///     Helper class for Serialization.
    /// </summary>
    public class SerializationHelper
    {
        /// <summary>
        ///     Serializes an object to xml string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>An xml string.</returns>
        public static string SerializeToXmlString(object obj)
        {
            string xml = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                if (obj != null)
                {
                    new NetDataContractSerializer().WriteObject(ms, obj);

                    ms.Flush();
                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    xml = sr.ReadToEnd();
                }
            }

            return xml;
        }

        /// <summary>
        ///     Deserializes an object to object of given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="obj">The xml string to deserialize.</param>
        /// <returns>Deserialized object of given type.</returns>
        public static T DeserializeFromXmlString<T>(string obj)
            where T : class
        {
            if (!string.IsNullOrEmpty(obj))
            {
                using (new MemoryStream())
                {
                }

                using (StringReader stringReader = new StringReader(obj))
                {
                    using (XmlReader reader = XmlReader.Create(stringReader))
                    {
                        return (T)new NetDataContractSerializer().ReadObject(reader);
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        ///     Deserializes a stream into an object of T.
        /// </summary>
        /// <typeparam name="T">The given type.</typeparam>
        /// <param name="stream">The stream that contains the XML to deserialize.</param>
        /// <returns>An object of type T.</returns>
        public static T Deserialize<T>(Stream stream)
            where T : class
        {
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        ///     Serializes the specified object graph using the specified writer.
        /// </summary>
        /// <param name="stream">The stream to serialize with.</param>
        /// <param name="obj">The object to serialize.</param>
        public static void Serialize(Stream stream, object obj)
        {
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            serializer.Serialize(stream, obj);
        }

        /// <summary>
        ///     Deserializes a byte array into an object of T.
        /// </summary>
        /// <typeparam name="T">The given type.</typeparam>
        /// <param name="data">The byte array to deserialize.</param>
        /// <returns>An object of type T.</returns>
        public static T DeserializeFromBytes<T>(byte[] data)
            where T : class
        {
            if (data == null || data.Length == 0)
            {
                return default(T);
            }

            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Deserialize<T>(memoryStream);
            }
        }

        /// <summary>
        ///     Deserializes a byte array into an object of T.
        /// </summary>
        /// <typeparam name="T">The given type.</typeparam>
        /// <param name="data">The byte array to deserialize.</param>
        /// <param name="requiredUnCompress">Whether data is compressed.</param>
        /// <returns>An object of type T.</returns>
        public static T DeserializeFromBytes<T>(byte[] data, bool requiredUnCompress)
            where T : class
        {
            return requiredUnCompress
                       ? DeserializeFromBytes<T>(Compression.DeCompress(data))
                       : DeserializeFromBytes<T>(data);
        }

        /// <summary>
        ///     Serializes an object to a byte array.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A byte array.</returns>
        public static byte[] SerializeToBytes(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                NetDataContractSerializer serializer = new NetDataContractSerializer();
                serializer.Serialize(memoryStream, obj);
                memoryStream.Seek(0, 0);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        ///     Serializes an object to a byte array.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="requiredCompress">Whether result has to be compressed.</param>
        /// <returns>A byte array.</returns>
        public static byte[] SerializeToBytes(object obj, bool requiredCompress)
        {
            return requiredCompress
                       ? Compression.Compress(SerializeToBytes(obj))
                       : SerializeToBytes(obj);
        }

        /// <summary>
        ///     Deserializes a file to an object of type T.
        /// </summary>
        /// <typeparam name="T">The given type.</typeparam>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>An object of type T.</returns>
        public static T DeserializeFromFile<T>(string fileName)
            where T : class
        {
            return DeserializeFromFile<T>(fileName, false);
        }

        /// <summary>
        ///     Deserializes a file to an object of type T.
        /// </summary>
        /// <typeparam name="T">The given type.</typeparam>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="requiredUnCompress">Whether the file needs to be uncompressed.</param>
        /// <returns>An object of type T.</returns>
        public static T DeserializeFromFile<T>(string fileName, bool requiredUnCompress)
            where T : class
        {
            if (requiredUnCompress)
            {
                using (Stream stream = Compression.DecompressingStream(new FileStream(fileName, FileMode.Open)))
                {
                    return Deserialize<T>(stream);
                }
            }

            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return Deserialize<T>(stream);
            }
        }

        /// <summary>
        ///     Serializes an object to a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="obj">The object to serialize.</param>
        public static void SerializeToFile(string fileName, object obj)
        {
            SerializeToFile(fileName, obj, false);
        }

        /// <summary>
        ///     Serializes an object to a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="requiredCompress">Whether the file needs to be compressed.</param>
        public static void SerializeToFile(string fileName, object obj, bool requiredCompress)
        {
            if (requiredCompress)
            {
                using (Stream str = Compression.CompressingStream(new FileStream(fileName, FileMode.Create)))
                {
                    Serialize(str, obj);
                }
            }
            else
            {
                using (Stream str = new FileStream(fileName, FileMode.Create))
                {
                    Serialize(str, obj);
                }
            }
        }

        /// <summary>
        ///     Deserializes the specified manifest resource from this assembly.
        /// </summary>
        /// <typeparam name="T">The given type.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <param name="nameSpacename">The name of the namespace.</param>
        /// <param name="resourceName">The name of the resource.</param>
        /// <param name="requiredUnCompress">Decompress or not.</param>
        /// <returns>Object of type T.</returns>
        public static T DeserializeFromManifestResourceStream<T>(
            Assembly assembly,
            string nameSpacename,
            string resourceName,
            bool requiredUnCompress)
            where T : class
        {
            Stream resourceStream = assembly.GetManifestResourceStream(string.Concat(nameSpacename, resourceName));
            if (resourceStream == null)
            {
                return default(T);
            }

            if (requiredUnCompress)
            {
                using (Stream stream = Compression.DecompressingStream(resourceStream))
                {
                    return Deserialize<T>(stream);
                }
            }

            using (resourceStream)
            {
                return Deserialize<T>(resourceStream);
            }
        }
    }
}