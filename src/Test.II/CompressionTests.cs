using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.Streaming;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    [TestFixture]
    public class CompressionTests
    {
        [Test]
        public void CompressedByteArrayNotNull()
        {
            Random random = new Random();
            Byte[] array = new byte[10];
            random.NextBytes(array);

            var result = Compression.Compress(array);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CompressThenDecompressReturnsSameArray()
        {
            Random random = new Random();
            Byte[] array = new byte[10];
            random.NextBytes(array);

            var compressed = Compression.Compress(array);
            var result = Compression.DeCompress(compressed);

            Assert.AreEqual(array, result);
        }
    }
}
