using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Utilities._585.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gelen metni Base64Url formatına çevirir. Bu format, URL'lerde güvenli bir şekilde kullanılabilir.
        /// </summary>
        /// <param name="text">Dönüştürülecek metin. Bu metin, UTF-8 formatında kodlanacaktır.</param>
        /// <returns>Dönüştürülmüş Base64Url formatındaki metin. Bu metin, URL'lerde güvenli bir şekilde kullanılabilir.</returns>
        public static string Base64UrlEncode(this string text)
        {
            var encodedBytes = Encoding.UTF8.GetBytes(text);
            var validString = WebEncoders.Base64UrlEncode(encodedBytes);
            return validString;
        }

        /// <summary>
        /// Gelen Base64Url formatındaki metni çözerek orijinal metni elde eder. Bu işlem, Base64Url formatının tersidir ve UTF-8 formatında kodlanmış metni geri döndürür.
        /// </summary>
        /// <param name="text">Geri dönüştürülecek Base64Url formatındaki metin. Bu metin, URL'lerde güvenli bir şekilde kullanılabilir ve çözülerek orijinal metin elde edilecektir.</param>
        /// <returns>Orijinal metin. Bu metin, Base64Url formatından çözülmüş ve UTF-8 formatında kodlanmış metindir.</returns>
        public static string Base64UrlDecode(this string text)
        {
            var decodedBytes = WebEncoders.Base64UrlDecode(text);
            var validString = Encoding.UTF8.GetString(decodedBytes);
            return validString;
        }
    }
}
