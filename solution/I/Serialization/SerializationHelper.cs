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
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Serialization
{
    public class SerializationHelper
    {
        public static byte[] Serialize(object obj)
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

        public static byte[] Serialize(object obj, bool requiredCompress)
        {
            return requiredCompress
                       ? Compress(Serialize(obj))
                       : Serialize(obj);
        }

        public static void SerializeToFile(string fileName, object obj)
        {
            XmlWriterSettings xws = new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Document,
                Indent = true,
                IndentChars = "\t",
                Encoding = Encoding.UTF8
            };
            using (XmlWriter xWriter = XmlWriter.Create(fileName, xws))
            {
                if (obj != null)
                {
                    new NetDataContractSerializer().WriteObject(xWriter, obj);
                }
            }
        }

        public static void SerializeToFile(string fileName, object obj, bool requiredCompress)
        {
            if (!requiredCompress)
            {
                SerializeToFile(fileName, obj);
            }
            else
            {
                byte[] data = Serialize(obj, true);
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    fileStream.Write(data, 0, data.Length);
                }
            }
        }

        public static string SerializeToXmlString(object obj)
        {
            string xml = string.Empty;

            new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Document,
                Indent = true,
                IndentChars = "\t",
                Encoding = Encoding.UTF8,
            };

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

        public static T Deserialize<T>(byte[] data)
            where T : class, new()
        {
            if (data == null || data.Length == 0)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                NetDataContractSerializer serializer = new NetDataContractSerializer();
                memoryStream.Write(data, 0, data.Length);
                memoryStream.Flush();
                memoryStream.Seek(0, 0);
                return (T)serializer.Deserialize(memoryStream);
            }
        }

        public static T Deserialize<T>(byte[] data, bool requiredUnCompress)
            where T : class, new()
        {
            return requiredUnCompress
                       ? Deserialize<T>(DeCompress(data))
                       : Deserialize<T>(data);
        }

        public static T DeserializeFromFile<T>(string fileName)
            where T : class, new()
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    return (T)new NetDataContractSerializer().ReadObject(reader, true);
                }
            }
        }

        public static T DeserializeFromFile<T>(string fileName, bool requiredUnCompress)
            where T : class, new()
        {
            if (!requiredUnCompress)
            {
                return DeserializeFromFile<T>(fileName);
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                return Deserialize<T>(data, true);
            }
        }

        public static byte[] Compress(byte[] bytData)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Stream zipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    zipStream.Write(bytData, 0, bytData.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public static byte[] DeCompress(byte[] data)
        {
            byte[] writeData = new byte[8192];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Stream zipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    int size;
                    while ((size = zipStream.Read(writeData, 0, writeData.Length)) > 0)
                    {
                        memoryStream.Write(writeData, 0, size);
                    }
                    memoryStream.Flush();
                    return memoryStream.ToArray();
                }
            }
        }

        public static T GetObjectFromManifestStream<T>(Assembly assembly, string nameSpacename, string resourceName, bool requiredUnCompress)
            where T : class, new()
        {
            Stream resourceStream = assembly.GetManifestResourceStream(string.Concat(nameSpacename, resourceName));
            if (resourceStream != null)
            {
                byte[] data = new byte[resourceStream.Length];
                resourceStream.Read(data, 0, data.Length);
                return Deserialize<T>(data, requiredUnCompress);
            }
            return default(T);
        }
    }
}
