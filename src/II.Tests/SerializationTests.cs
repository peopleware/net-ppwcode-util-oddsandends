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

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.Serialization;
using PPWCode.Util.OddsAndEnds.II.Streaming;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class SerializationTests
    {
        [Test, Description("Serialize/Deserialize an instance of class PersonA, with a string as intermediare value")]
        public void XmlSerialization_SerializePersonA()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA personA = new PersonA(@"Bob", @"Bones", addressA);
            string xmlData = SerializationHelper.SerializeToXmlString(personA);
            Assert.IsNotNull(xmlData);
            Assert.IsTrue(xmlData.Length > 0);
            PersonA deserializedPersonA = SerializationHelper.DeserializeFromXmlString<PersonA>(xmlData);
            Assert.AreEqual(personA, deserializedPersonA);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, using interfaces, with a string as intermediare value")]
        public void XmlSerialization_SerializePersonAUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson person = new PersonA(@"Bob", @"Bones", address);
            string xmlData = SerializationHelper.SerializeToXmlString(person);
            Assert.IsNotNull(xmlData);
            Assert.IsTrue(xmlData.Length > 0);
            IPerson deserializedPerson = SerializationHelper.DeserializeFromXmlString<IPerson>(xmlData);
            Assert.AreEqual(person, deserializedPerson);
            Assert.IsInstanceOf<PersonA>(person);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, with a string as intermediare value")]
        public void XmlSerialization_SerializePersonB()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA partner = new PersonA(@"Kate", @"Moss", addressA);
            PersonA personB = new PersonB(@"Bob", @"Bones", addressA, partner);
            string xmlData = SerializationHelper.SerializeToXmlString(personB);
            Assert.IsNotNull(xmlData);
            Assert.IsTrue(xmlData.Length > 0);
            PersonB deserializedPersonB = SerializationHelper.DeserializeFromXmlString<PersonB>(xmlData);
            Assert.AreEqual(personB, deserializedPersonB);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, using interfaces, with a string as intermediare value")]
        public void XmlSerialization_SerializePersonBUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson partner = new PersonA(@"Kate", @"Moss", address);
            IPerson person = new PersonB(@"Bob", @"Bones", address, partner);
            string xmlData = SerializationHelper.SerializeToXmlString(person);
            Assert.IsNotNull(xmlData);
            Assert.IsTrue(xmlData.Length > 0);
            IPerson deserializedPerson = SerializationHelper.DeserializeFromXmlString<IPerson>(xmlData);
            Assert.AreEqual(person, deserializedPerson);
            Assert.IsInstanceOf<PersonB>(person);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, with a ByteArray as intermediare value")]
        public void ByteArray_SerializePersonA()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA personA = new PersonA(@"Bob", @"Bones", addressA);
            byte[] data = SerializationHelper.SerializeToBytes(personA);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            PersonA deserializedPersonA = SerializationHelper.DeserializeFromBytes<PersonA>(data);
            Assert.AreEqual(personA, deserializedPersonA);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, using interfaces, with a ByteArray as intermediare value")]
        public void ByteArray_SerializePersonAUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson person = new PersonA(@"Bob", @"Bones", address);
            byte[] data = SerializationHelper.SerializeToBytes(person);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            IPerson deserializedPerson = SerializationHelper.DeserializeFromBytes<IPerson>(data);
            Assert.AreEqual(person, deserializedPerson);
            Assert.IsInstanceOf<PersonA>(person);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, with a ByteArray as intermediare value")]
        public void ByteArray_SerializePersonB()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA partner = new PersonA(@"Kate", @"Moss", addressA);
            PersonA personB = new PersonB(@"Bob", @"Bones", addressA, partner);
            byte[] data = SerializationHelper.SerializeToBytes(personB);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            PersonB deserializedPersonB = SerializationHelper.DeserializeFromBytes<PersonB>(data);
            Assert.AreEqual(personB, deserializedPersonB);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, using interfaces, with a ByteArray as intermediare value")]
        public void ByteArray_SerializePersonBUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson partner = new PersonA(@"Kate", @"Moss", address);
            IPerson person = new PersonB(@"Bob", @"Bones", address, partner);
            byte[] data = SerializationHelper.SerializeToBytes(person);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            IPerson deserializedPerson = SerializationHelper.DeserializeFromBytes<IPerson>(data);
            Assert.AreEqual(person, deserializedPerson);
            Assert.IsInstanceOf<PersonA>(person);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, with a compressed ByteArray as intermediare value")]
        public void CompressedByteArray_SerializePersonA()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA personA = new PersonA(@"Bob", @"Bones", addressA);
            byte[] data = SerializationHelper.SerializeToBytes(personA, true);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            PersonA deserializedPersonA = SerializationHelper.DeserializeFromBytes<PersonA>(data, true);
            Assert.AreEqual(personA, deserializedPersonA);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, using interfaces, with a compressed ByteArray as intermediare value")]
        public void CompressedByteArray_SerializePersonAUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson person = new PersonA(@"Bob", @"Bones", address);
            byte[] data = SerializationHelper.SerializeToBytes(person, true);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            IPerson deserializedPerson = SerializationHelper.DeserializeFromBytes<IPerson>(data, true);
            Assert.AreEqual(person, deserializedPerson);
            Assert.IsInstanceOf<PersonA>(person);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, with a compressed ByteArray as intermediare value")]
        public void CompressedByteArray_SerializePersonB()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA partner = new PersonA(@"Kate", @"Moss", addressA);
            PersonA personB = new PersonB(@"Bob", @"Bones", addressA, partner);
            byte[] data = SerializationHelper.SerializeToBytes(personB, true);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            PersonB deserializedPersonB = SerializationHelper.DeserializeFromBytes<PersonB>(data, true);
            Assert.AreEqual(personB, deserializedPersonB);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, using interfaces, with a compressed ByteArray as intermediare value")]
        public void CompressedByteArray_SerializePersonBUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson partner = new PersonA(@"Kate", @"Moss", address);
            IPerson person = new PersonB(@"Bob", @"Bones", address, partner);
            byte[] data = SerializationHelper.SerializeToBytes(person, true);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);
            IPerson deserializedPerson = SerializationHelper.DeserializeFromBytes<IPerson>(data, true);
            Assert.AreEqual(person, deserializedPerson);
            Assert.IsInstanceOf<PersonB>(person);
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, with a stream as intermediare value")]
        public void Stream_SerializePersonA()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA personA = new PersonA(@"Bob", @"Bones", addressA);
            using (Stream stream = new MemoryStream())
            {
                SerializationHelper.Serialize(stream, personA);
                Assert.IsTrue(stream.Length > 0);
                stream.Position = 0;
                PersonA deserializedPersonA = SerializationHelper.Deserialize<PersonA>(stream);
                Assert.AreEqual(personA, deserializedPersonA);
            }
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, using interfaces, with a stream as intermediare value")]
        public void Stream_SerializePersonAUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson person = new PersonA(@"Bob", @"Bones", address);
            using (Stream stream = new MemoryStream())
            {
                SerializationHelper.Serialize(stream, person);
                Assert.IsTrue(stream.Length > 0);
                stream.Position = 0;
                PersonA deserializedPerson = SerializationHelper.Deserialize<PersonA>(stream);
                Assert.AreEqual(person, deserializedPerson);
                Assert.IsInstanceOf<PersonA>(person);
            }
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, with a ByteArray as intermediare value")]
        public void Stream_SerializePersonB()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA partner = new PersonA(@"Kate", @"Moss", addressA);
            PersonA personB = new PersonB(@"Bob", @"Bones", addressA, partner);
            using (Stream stream = new MemoryStream())
            {
                SerializationHelper.Serialize(stream, personB);
                Assert.IsTrue(stream.Length > 0);
                stream.Position = 0;
                PersonA deserializedPersonB = SerializationHelper.Deserialize<PersonA>(stream);
                Assert.AreEqual(personB, deserializedPersonB);
            }
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonB, using interfaces, with a ByteArray as intermediare value")]
        public void Stream_SerializePersonBUsingAnInterface()
        {
            IAddress address = new AddressA(@"CornerStreet", @"00501", @"New york");
            IPerson partner = new PersonA(@"Kate", @"Moss", address);
            IPerson person = new PersonB(@"Bob", @"Bones", address, partner);
            using (Stream stream = new MemoryStream())
            {
                SerializationHelper.Serialize(stream, person);
                Assert.IsTrue(stream.Length > 0);
                stream.Position = 0;
                IPerson deserializedPerson = SerializationHelper.Deserialize<IPerson>(stream);
                Assert.AreEqual(person, deserializedPerson);
                Assert.IsInstanceOf<PersonA>(person);
            }
        }

        [Test, Description("Serialize/Deserialize an instance of class PersonA, with a compressed stream as intermediate value")]
        public void CompressedStream_SerializePersonA()
        {
            AddressA addressA = new AddressA(@"CornerStreet", @"00501", @"New york");
            PersonA personA = new PersonA(@"Bob", @"Bones", addressA);

            byte[] memory;

            using (MemoryStream stream = new MemoryStream())
            {
                // compressing stream only forced to flush when the stream is closed
                using (Stream compressed = Compression.CompressingStream(stream))
                {
                    SerializationHelper.Serialize(compressed, personA);
                    Assert.IsTrue(stream.Length > 0);
                }

                // copy only after the compressing stream has been closed
                memory = stream.ToArray();
            }

            PersonA deserializedPersonA;
            using (Stream stream = new MemoryStream(memory))
            using (Stream decompressed = Compression.DecompressingStream(stream))
            {
                deserializedPersonA = SerializationHelper.Deserialize<PersonA>(decompressed);
            }

            Assert.AreEqual(personA, deserializedPersonA);
        }
    }
}