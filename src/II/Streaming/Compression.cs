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
using System.IO.Compression;

namespace PPWCode.Util.OddsAndEnds.II.Streaming
{
    /// <summary>
    ///     Helper class for Compression.
    /// </summary>
    public class Compression
    {
        /// <summary>
        ///     Compresses a byte array.
        /// </summary>
        /// <param name="bytData">The byte array to compress.</param>
        /// <returns>Compressed byte array.</returns>
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

        /// <summary>
        ///     Decompresses a byte array.
        /// </summary>
        /// <param name="data">The given byte array.</param>
        /// <returns>A byte array.</returns>
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

        /// <summary>
        ///     Initializes a new instance of the GZipStream class to compress data by using the given stream.
        /// </summary>
        /// <param name="stream">The given stream.</param>
        /// <returns>The stream the compressed data is written to.</returns>
        public static Stream CompressingStream(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Compress);
        }

        /// <summary>
        ///     Initializes a new instance of the GZipStream class to decompress data by using the given stream.
        /// </summary>
        /// <param name="stream">The given stream.</param>
        /// <returns>The stream the decompressed data is written to.</returns>
        public static Stream DecompressingStream(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Decompress);
        }
    }
}