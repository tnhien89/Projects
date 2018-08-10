using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DotNetLibrary.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNonAsciiCharacter(this string str)
        {
            string stFormD = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

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
            string rs = str.RemoveNonAsciiCharacter();
            rs = rs.Replace(" ", "-");

            return rs.ToLower();
        }

        public static string MD5Encryption(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            MD5 md5 = MD5.Create();

            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(str));

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                result.Append(hashData[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
