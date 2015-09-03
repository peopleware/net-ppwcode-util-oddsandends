using System;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.Identification;

namespace PPWCode.Util.OddsAndEnds.II.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test, TestCaseSource(typeof(ValidationFactory), "IsValidDmfaNumber")]
        public long? IsValidDmfaNumber(string dmfaNumber)
        {
            return Validation.ValidDmfaNumber(dmfaNumber);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "ValidRrn")]
        public bool ValidRrn(string rijksregisternummer)
        {
            return Validation.ValidRrn(rijksregisternummer);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "StrictValidRrn")]
        public bool StrictValidRrn(string rijksregisternummer)
        {
            return Validation.StrictValidRrn(rijksregisternummer);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "PadRrn")]
        public string PadRrn(string rijksregisternummer)
        {
            return Validation.PadRrn(rijksregisternummer);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "GetBirthDateFromRrn")]
        public DateTime? GetBirthDateFromRrn(string rijksregisternummer)
        {
            return Validation.GetBirthDateFromRrn(rijksregisternummer);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "GetDigitStream")]
        public string GetDigitStream(string stream)
        {
            return Validation.GetDigitStream(stream);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "ValidRsz")]
        public bool ValidRsz(string rsz)
        {
            return Validation.ValidRsz(rsz);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "StrictValidRsz")]
        public bool StrictValidRsz(string rsz)
        {
            return Validation.StrictValidRsz(rsz);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "PadRsz")]
        public string PadRsz(string rsz)
        {
            return Validation.PadRsz(rsz);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "ValidKbo")]
        public bool ValidKbo(string kbo)
        {
            return Validation.ValidKbo(kbo);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "StrictValidKbo")]
        public bool StrictValidKbo(string kbo)
        {
            return Validation.StrictValidKbo(kbo);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "PadKbo")]
        public string PadKbo(string kbo)
        {
            return Validation.PadKbo(kbo);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "ValidVat")]
        public bool ValidVat(string vat)
        {
            return Validation.ValidVat(vat);
        }

        [Test, TestCaseSource(typeof(ValidationFactory), "StrictValidVat")]
        public bool StrictValidVat(string vat)
        {
            return Validation.StrictValidVat(vat);
        }
    }
}
