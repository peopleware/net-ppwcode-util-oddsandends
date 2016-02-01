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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PPWCode.Util.OddsAndEnds.II.Identification
{
    /// <summary>
    ///     Helper class for validation.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        ///     The length of KBO.
        /// </summary>
        public const int LengthKbo = 10;

        /// <summary>
        ///     The length of VAT.
        /// </summary>
        public const int LengthVat = 9;

        /// <summary>
        ///     The length of RIJKSREGISTENUMMER.
        /// </summary>
        public const int LengthRrn = 11;

        /// <summary>
        ///     The length of RSZ.
        /// </summary>
        public const int LengthRsz = 10;

        /// <summary>
        ///     Converts string to valid DMFA number.
        /// </summary>
        /// <param name="dmfaLotNbr">The given string.</param>
        /// <returns>
        ///     Valid DMFA number as long.
        ///     Null if it is not a valid DMFA number.
        /// </returns>
        [Pure]
        public static long? ValidDmfaNumber(string dmfaLotNbr)
        {
            long? result;

            if (new Regex(@"DMFA(A|P)\d{9}(\d|[A-Z])").IsMatch(dmfaLotNbr))
            {
                result = long.Parse(dmfaLotNbr.Substring(5, 9));
            }
            else if (new Regex(@"\d{10}").IsMatch(dmfaLotNbr) || new Regex(@"\d{9}").IsMatch(dmfaLotNbr))
            {
                result = long.Parse(dmfaLotNbr.Substring(0, 9));
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        ///     Checks whether string is a valid 'RIJKSREGISTERNUMMER'.
        /// </summary>
        /// <param name="rrn">The 'RIJKSREGISTERNUMMER' as string.</param>
        /// <returns>True or false.</returns>
        /// <remarks>Is less strict. All characters that are not numbers are removed before validation is done.</remarks>
        [Pure]
        public static bool ValidRrn(string rrn)
        {
            string digitStream = PadRrn(GetDigitStream(rrn));
            return StrictValidRrn(digitStream);
        }

        /// <summary>
        ///     Checks whether string is a strict valid 'RIJKSREGISTERNUMMER'.
        /// </summary>
        /// <param name="rrn">The 'RIJKSREGISTERNUMMER' as string.</param>
        /// <returns>True or false.</returns>
        /// <remarks>The string cannot contain any characters that are not part of the 'RIJKSREGISTERNUMMER'.</remarks>
        [Pure]
        public static bool StrictValidRrn(string rrn)
        {
            if (rrn.Length == LengthRrn)
            {
                string number = rrn.Substring(0, 9);
                long numberBefore2000 = long.Parse(number);
                long numberAfter2000 = long.Parse(string.Concat('2', number));
                int rest = 97 - int.Parse(rrn.Substring(9, 2));
                return numberBefore2000 % 97 == rest || numberAfter2000 % 97 == rest;
            }

            return false;
        }

        /// <summary>
        ///     Adds zero to left of string so that the length of string is the required length for 'RIJKSREGISTERNUMMER'.
        /// </summary>
        /// <param name="rrn">The given 'RIJKSREGISTERNUMMER' as string.</param>
        /// <returns>A string padded with zeros.</returns>
        [Pure]
        public static string PadRrn(string rrn)
        {
            return string.IsNullOrEmpty(rrn) ? rrn : rrn.PadLeft(LengthRrn, '0');
        }

        private static void ParseRrn(string rrn, out DateTime? birthdate, out int sexe)
        {
            birthdate = null;
            sexe = 9;
            string digitStream = GetDigitStream(rrn);
            if (ValidRrn(rrn) && (digitStream.Length > 0))
            {
                bool calcSexe = false;
                bool calcBirthDate = false;
                int yy = int.Parse(digitStream.Substring(0, 2));
                int mm = int.Parse(digitStream.Substring(2, 2));
                int dd = int.Parse(digitStream.Substring(4, 2));
                int vvv = int.Parse(digitStream.Substring(6, 3));

                int yyOffset;
                {
                    long numberBefore2000 = long.Parse(digitStream.Substring(0, 9));
                    int rest = 97 - int.Parse(digitStream.Substring(9, 2));
                    yyOffset = numberBefore2000 % 97 == rest ? 1900 : 2000;
                }

                yy = yy + yyOffset;

                if (mm < 20)
                {
                    calcSexe = true;
                    calcBirthDate = true;
                }
                else if ((20 <= mm) && (mm < 40))
                {
                    mm -= 20;
                    calcBirthDate = true;
                }
                else if ((40 <= mm) && (mm < 60))
                {
                    mm -= 40;
                    calcSexe = true;
                    calcBirthDate = true;
                }

                if (calcSexe)
                {
                    sexe = (vvv == 0 || vvv == 999) ? 0 : ((vvv % 2) == 1) ? 1 : 2;
                }

                if (calcBirthDate)
                {
                    try
                    {
                        birthdate = new DateTime(yy, mm, dd);
                        if (birthdate > DateTime.Today)
                        {
                            birthdate = null;
                        }
                    }
                    catch
                    {
                        birthdate = null;
                    }
                }
            }
        }

        /// <summary>
        ///     Gets the birthday out of given 'RIJKSREGISTERNUMMER'.
        /// </summary>
        /// <param name="rrn">The given 'RIJKSREGISTERNUMMER' as string.</param>
        /// <returns>The birthday or null.</returns>
        [Pure]
        public static DateTime? GetBirthDateFromRrn(string rrn)
        {
            DateTime? birthDate;
            int sexe;
            ParseRrn(rrn, out birthDate, out sexe);
            return birthDate;
        }

        /// <summary>
        ///     Gets only the digits of string.
        /// </summary>
        /// <param name="stream">The given string.</param>
        /// <returns>The digits as string.</returns>
        [Pure]
        public static string GetDigitStream(string stream)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(stream.Length);
            foreach (char ch in stream.Where(char.IsDigit))
            {
                sb.Append(ch);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Checks whether given string is a valid RSZ number.
        /// </summary>
        /// <param name="rsz">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool ValidRsz(string rsz)
        {
            string digitStream = PadRsz(GetDigitStream(rsz));
            return StrictValidRsz(digitStream);
        }

        /// <summary>
        ///     Checks whether string is a strict valid RSZ number.
        /// </summary>
        /// <param name="rsz">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool StrictValidRsz(string rsz)
        {
            bool result = false;
            if (rsz.Length == LengthRsz)
            {
                long rest = 96 - ((long.Parse(rsz.Substring(0, 8)) * 100) % 97);

                if (rest == 0)
                {
                    rest = 97;
                }

                result = rest == long.Parse(rsz.Substring(8, 2));
            }

            return result;
        }

        /// <summary>
        ///     Adds zeros to left of string so that the length of string is the required length for RSZ number.
        /// </summary>
        /// <param name="rsz">The given RSZ number as string.</param>
        /// <returns>A string padded with zeros.</returns>
        [Pure]
        public static string PadRsz(string rsz)
        {
            return string.IsNullOrEmpty(rsz) ? rsz : rsz.PadLeft(LengthRsz, '0');
        }

        /// <summary>
        ///     Checks whether given string is a valid KBO number.
        /// </summary>
        /// <param name="kbo">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool ValidKbo(string kbo)
        {
            string digitStream = PadKbo(GetDigitStream(kbo));
            return StrictValidKbo(digitStream);
        }

        /// <summary>
        ///     Checks whether given string is a strict valid KBO number.
        /// </summary>
        /// <param name="kbo">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool StrictValidKbo(string kbo)
        {
            bool result = false;
            if (kbo.Length == LengthKbo)
            {
                long rest = 97 - (long.Parse(kbo.Substring(0, 8)) % 97);
                result = rest == long.Parse(kbo.Substring(8, 2));
            }

            return result;
        }

        /// <summary>
        ///     Adds zeros to left of string so that the length of string is the required length for KBO number.
        /// </summary>
        /// <param name="kbo">The given string.</param>
        /// <returns>A string padded with zeros.</returns>
        [Pure]
        public static string PadKbo(string kbo)
        {
            return string.IsNullOrEmpty(kbo) ? kbo : kbo.PadLeft(LengthKbo, '0');
        }

        /// <summary>
        ///     Checks whether string is a valid VAT number.
        /// </summary>
        /// <param name="vat">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool ValidVat(string vat)
        {
            string digitStream = GetDigitStream(vat);
            if (digitStream.Length < LengthVat)
            {
                digitStream = digitStream.PadLeft(LengthVat, '0');
            }

            return StrictValidVat(digitStream);
        }

        /// <summary>
        ///     Checks whether string is a strict valid VAT number.
        /// </summary>
        /// <param name="vat">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool StrictValidVat(string vat)
        {
            bool result = false;
            if (vat.Length == LengthVat)
            {
                long rest = 97 - (long.Parse(vat.Substring(0, 7)) % 97);
                result = rest == long.Parse(vat.Substring(7, 2));
            }

            return result;
        }

        /// <summary>
        ///     Checks whether string is a valid IBAN.
        /// </summary>
        /// <param name="iban">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool StrictValidIban(string iban)
        {
            // Step 1. Size should be at least 2
            if (string.IsNullOrEmpty(iban) || iban.Length < 2)
            {
                return false;
            }

            // Step 2. Check country code
            string countryCode = iban.Substring(0, 2).ToUpper();
            IbanCountry ibanCountry;
            if (!s_IBANCountries.TryGetValue(countryCode, out ibanCountry))
            {
                return false;
            }

            // Fase 3. delete all non-alphanumeric characters
            StringBuilder sb = new StringBuilder(50);
            IEnumerable<char> letterAndDigits = iban
                .Select(t => char.ToUpper(t))
                .Where(ch => char.IsLetterOrDigit(ch));
            foreach (char ch in letterAndDigits)
            {
                sb.Append(ch);
            }

            // Fase 4: Check the length of the IBAN nr according the country code
            if (ibanCountry.IBANLength != sb.Length)
            {
                return false;
            }

            // Fase 4b: Check regular expression if known
            if (ibanCountry.Pattern != string.Empty)
            {
                Regex regex = new Regex(string.Concat(countryCode, ibanCountry.Pattern));
                if (!regex.IsMatch(sb.ToString()))
                {
                    return false;
                }
            }

            // Fase 5: Move the first four characters of the IBAN to the right of the number.
            sb.Append(sb.ToString().Substring(0, 4));
            sb.Remove(0, 4);

            // Fase 6: Convert the letters into numerics in accordance with the conversion table.
            StringBuilder sb2 = new StringBuilder(50);
            for (int i = 0; i < sb.ToString().Length; i++)
            {
                char ch = sb.ToString()[i];
                if (char.IsLetter(ch))
                {
                    int num;
                    if (s_IBANConversions.TryGetValue(ch, out num))
                    {
                        sb2.Append(num.ToString());
                    }
                }
                else
                {
                    sb2.Append(ch);
                }
            }

            // Fase 7: Check MOD 97-10 (see ISO 7064).  
            // For the check digits to be correct, 
            // the remainder after calculating the modulus 97 must be 1.
            // We will use integers instead of floating point numnbers for precision.
            // BUT if the number is too long for the software implementation of
            // integers (a signed 32/64 bits represents 9/18 digits), then the 
            // calculation can be split up into consecutive remainder calculations
            // on integers with a maximum of 9 or 18 digits.
            // I wil choose 32 bit integers.
            int mod97 = 0, n = 9;
            string s9 = sb2.ToString().Substring(0, n);
            while (s9.Length > 0)
            {
                sb2.Remove(0, n);
                mod97 = int.Parse(s9) % 97;
                if (sb2.Length > 0)
                {
                    n = (mod97 < 10) ? 8 : 7;
                    n = sb2.Length < n ? sb2.Length : n;
                    s9 = string.Concat(mod97.ToString(), sb2.ToString().Substring(0, n));
                }
                else
                {
                    s9 = string.Empty;
                }
            }

            return mod97 == 1;
        }

        private static readonly IDictionary<string, IbanCountry> s_IBANCountries = new Dictionary<string, IbanCountry>
        {
            { "AD", new IbanCountry(24, "[0-9]{10}[0-9A-Z]{12}") }, // 1. Andorra
            { "AT", new IbanCountry(20, "[0-9]{18}") }, // 2. Austria
            { "BE", new IbanCountry(16, "[0-9]{14}") }, // 3. Belgium 
            { "BA", new IbanCountry(20, "[0-9]{18}") }, // 4. Bosnia and Herzegovina
            { "BG", new IbanCountry(22, "[0-9]{2}[A-Z]{4}[0-9]{6}[0-9A-Z]{8}") }, // 5. Bulgaria
            { "HR", new IbanCountry(21, "[0-9]{19}") }, // 6. Croatia
            { "CY", new IbanCountry(28, "[0-9]{10}[0-9A-Z]{16}") }, // 7. Cyprus
            { "CZ", new IbanCountry(24, "[0-9]{22}") }, // 8. Czech Republic
            { "DK", new IbanCountry(18, "[0-9]{16}") }, // 9. Denmark
            { "EE", new IbanCountry(20, "[0-9]{18}") }, // 10. Estonia
            { "FI", new IbanCountry(18, "[0-9]{16}") }, // 11. Finland
            { "FR", new IbanCountry(27, "[0-9]{12}[0-9A-Z]{11}[0-9]{2}") }, // 12. France
            { "DE", new IbanCountry(22, "[0-9]{20}") }, // 13. Germany
            { "GI", new IbanCountry(23, "[0-9]{2}[A-Z]{4}[0-9A-Z]{15}") }, // 14. Gibraltar
            { "GR", new IbanCountry(27, "[0-9]{9}[0-9A-Z]{16}") }, // 15. Greece
            { "HU", new IbanCountry(28, "[0-9]{26}") }, // 16. Hungary
            { "IS", new IbanCountry(26, "[0-9]{24}") }, // 17. Iceland
            { "IE", new IbanCountry(22, "[0-9]{2}[A-Z]{4}[0-9]{14}") }, // 18. Ireland
            { "IL", new IbanCountry(23, "[0-9]{21}") }, // 19. Israel
            { "IT", new IbanCountry(27, "[0-9]{2}[A-Z][0-9]{10}[0-9A-Z]{12}") }, // 20. Italy
            { "LV", new IbanCountry(21, "[0-9]{2}[A-Z]{4}[0-9A-Z]{13}") }, // 21. Latvia
            { "LI", new IbanCountry(21, "[0-9]{7}[0-9A-Z]{12}") }, // 22. Principality of Liechtenstein
            { "LT", new IbanCountry(20, "[0-9]{18}") }, // 23. Lithuania
            { "LU", new IbanCountry(20, "[0-9]{5}[0-9A-Z]{13}") }, // 24. Luxembourg
            { "MK", new IbanCountry(19, "[0-9]{5}[0-9A-Z]{10}[0-9]{2}") }, // 25. Macedonia, former Yugoslav Republic of
            { "MT", new IbanCountry(31, "[0-9]{2}[A-Z]{4}[0-9]{5}[0-9A-Z]{18}") }, // 26. Malta
            { "MU", new IbanCountry(30, "[0-9]{2}[A-Z]{4}[0-9]{19}[A-Z]{3}") }, // 27. Mauritius
            { "MC", new IbanCountry(27, "[0-9]{12}[0-9A-Z]{11}[0-9]{2}") }, // 28. Monaco
            { "ME", new IbanCountry(22, "[0-9]{20}") }, // 29. Montenegro
            { "NL", new IbanCountry(18, "[0-9]{2}[A-Z]{4}[0-9]{10}") }, // 30. The Netherlands
            { "NO", new IbanCountry(15, "[0-9]{13}") }, // 31. Norway
            { "PL", new IbanCountry(28, "[0-9]{26}") }, // 32. Poland
            { "PT", new IbanCountry(25, "[0-9]{23}") }, // 33. Portugal
            { "RO", new IbanCountry(24, "[0-9]{2}[A-Z]{4}[0-9A-Z]{16}") }, // 34. Romania
            { "SM", new IbanCountry(27, "[0-9]{2}[A-Z][0-9]{10}[0-9A-Z]{12}") }, // 35. San Marino
            { "RS", new IbanCountry(22, "[0-9]{20}") }, // 36. Serbia
            { "SK", new IbanCountry(24, "[0-9]{22}") }, // 37. Slovak Republic
            { "SI", new IbanCountry(19, "[0-9]{17}") }, // 38. Slovenia
            { "ES", new IbanCountry(24, "[0-9]{22}") }, // 39. Spain
            { "SE", new IbanCountry(24, "[0-9]{22}") }, // 40. Sweden
            { "CH", new IbanCountry(21, "[0-9]{7}[0-9A-Z]{12}") }, // 41. Switzerland
            { "TN", new IbanCountry(24, "59[0-9]{20}") }, // 42. Tunisia
            { "TR", new IbanCountry(26, "[0-9]{7}[0-9A-Z]{17}") }, // 43. Turkey
            { "GB", new IbanCountry(22, "[0-9]{2}[A-Z]{4}[0-9]{14}") } // 44. United Kingdom
        };

        private static readonly IDictionary<char, int> s_IBANConversions = new Dictionary<char, int>
        {
            { 'A', 10 },
            { 'B', 11 },
            { 'C', 12 },
            { 'D', 13 },
            { 'E', 14 },
            { 'F', 15 },
            { 'G', 16 },
            { 'H', 17 },
            { 'I', 18 },
            { 'J', 19 },
            { 'K', 20 },
            { 'L', 21 },
            { 'M', 22 },
            { 'N', 23 },
            { 'O', 24 },
            { 'P', 25 },
            { 'Q', 26 },
            { 'R', 27 },
            { 'S', 28 },
            { 'T', 29 },
            { 'U', 30 },
            { 'V', 31 },
            { 'W', 32 },
            { 'X', 33 },
            { 'Y', 34 },
            { 'Z', 35 }
        };

        private struct IbanCountry
        {
            public int IBANLength { get; private set; }

            public string Pattern { get; private set; }

            public IbanCountry(int ibanLength, string pattern)
                : this()
            {
                IBANLength = ibanLength;
                Pattern = pattern;
            }
        }

        /// <summary>
        ///     Checks whether string is a valid BIC.
        /// </summary>
        /// <param name="bic">The given string.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool StrictValidBic(string bic)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(bic))
            {
                Regex regex = new Regex(@"^[A-Z]{6}[A-Z0-9]{2}([A-Z0-9]{3})?$");
                result = regex.IsMatch(bic);
            }

            return result;
        }
    }
}