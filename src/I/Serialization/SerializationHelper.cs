//Copyright 2004 - $Date: 2008-11-15 23:58:07 +0100 (za, 15 nov 2008) $ by PeopleWare n.v..

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

#region Using

using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

using PPWCode.Util.OddsAndEnds.I.Streaming;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Serialization
{
    public class SerializationHelper
    {
        #region Serialize and deserialize to xml string

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

        #endregion

        #region Serialize and deserialize to and from stream

        public static T Deserialize<T>(Stream stream)
            where T : class
        {
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            return (T)serializer.Deserialize(stream);
        }

        public static T Deserialize<T>(Stream stream, bool decompress)
            where T : class
        {
            if (!decompress)
            {
                return Deserialize<T>(stream);
            }
            using (Stream wrappingstream = Compression.DecompressingStream(stream))
            {
                return Deserialize<T>(wrappingstream);
            }
        }

        public static void Serialize(Stream stream, object obj)
        {
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            serializer.Serialize(stream, obj);
        }

        public static void Serialize(Stream stream, object obj, bool compress)
        {
            if (!compress)
            {
                Serialize(stream, obj);
            }
            else
            {
                using (Stream str = Compression.CompressingStream(stream))
                {
                    Serialize(str, obj);
                }
            }
        }

        #endregion

        #region Serialize and deserialize to and from byte array

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

        public static T DeserializeFromBytes<T>(byte[] data, bool requiredUnCompress)
            where T : class
        {
            return requiredUnCompress
                       ? DeserializeFromBytes<T>(Compression.DeCompress(data))
                       : DeserializeFromBytes<T>(data);
        }

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

        public static byte[] SerializeToBytes(object obj, bool requiredCompress)
        {
            return requiredCompress
                       ? Compression.Compress(SerializeToBytes(obj))
                       : SerializeToBytes(obj);
        }

        #endregion

        #region Serialize and deserialize to and from file

        public static T DeserializeFromFile<T>(string fileName)
            where T : class
        {
            return DeserializeFromFile<T>(fileName, false);
        }

        public static T DeserializeFromFile<T>(string fileName, bool requiredUnCompress)
            where T : class
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                return Deserialize<T>(fileStream, requiredUnCompress);
            }
        }

        public static void SerializeToFile(string fileName, object obj)
        {
            SerializeToFile(fileName, obj, false);
        }

        public static void SerializeToFile(string fileName, object obj, bool requiredCompress)
        {
            using (Stream str = new FileStream(fileName, FileMode.Create))
            {
                Serialize(str, obj, requiredCompress);
            }
        }

        #endregion

        #region Deserialize from manifest resource stream

        public static T DeserializeForManifestResourceStream<T>(
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
            using (resourceStream)
            {
                return Deserialize<T>(resourceStream, requiredUnCompress);
            }
        }

        #endregion
    }
}