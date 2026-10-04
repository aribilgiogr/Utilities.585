using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Utilities._585.Extensions
{
    public static class StringExtensions
    {
        public static string Base64UrlEncode(this string text)
        {
            var encodedBytes = Encoding.UTF8.GetBytes(text);
            var validString = WebEncoders.Base64UrlEncode(encodedBytes);
            return validString;
        }
    }
}
