using System;
using System.Linq;

namespace Shadow.Client.Extensions
{
    public static class HexExtensions
    {
        public static string HexEncode(this byte[] byteArr, string sep = "")
            => BitConverter.ToString(byteArr).Replace("-", sep);

        public static byte[] HexDecode(this string hex)
            => Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
    }
}