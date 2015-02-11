using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace NerdBudget.Core
{
    static class Utilities
    {
        #region Members

        private const NumberStyles FlexibleNumberStyle = NumberStyles.AllowDecimalPoint |
            NumberStyles.AllowLeadingWhite |
            NumberStyles.AllowLeadingSign |
            NumberStyles.AllowThousands |
            NumberStyles.AllowTrailingWhite |
            NumberStyles.AllowParentheses
            ;

        #endregion

        #region String Extensions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static string ToCrc32(this string input)
        {
            return Crc32.Hash(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static double ToDouble(this string value)
        {
            if (value == "")
            {
                value = "0";
            }

            value = value.Replace("$", "");

            return double.Parse(value, FlexibleNumberStyle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DateTime ToDate(this string value)
        {
            return DateTime.Parse(value).Date;
        }

        #endregion

        #region ID Generator

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateId(int length)
        {
            string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];

                crypto.GetNonZeroBytes(data);

                StringBuilder result = new StringBuilder(length);

                foreach (byte b in data)
                {
                    result.Append(a[b % (a.Length - 1)]);
                }

                return result.ToString();
            }
        }

        #endregion
    }
}
