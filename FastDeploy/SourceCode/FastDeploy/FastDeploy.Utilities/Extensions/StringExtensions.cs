using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastDeploy.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNonAsciiCharacter(this string str)
        {
            var stFormD = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string CreateItemKey(this string str)
        {
            var rs = str.RemoveNonAsciiCharacter();
            rs = rs.Replace(" ", "-");

            return rs.ToLower();
        }

        public static string MD5Encryption(this string str, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            MD5 md5 = MD5.Create();

            byte[] hashData = md5.ComputeHash(encoding == null ? Encoding.Default.GetBytes(str) : encoding.GetBytes(str));

            StringBuilder result = new();

            for (int i = 0; i < hashData.Length; i++)
            {
                result.Append(hashData[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
