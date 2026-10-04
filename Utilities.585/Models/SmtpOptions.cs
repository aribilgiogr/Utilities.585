namespace Utilities._585.Models
{
    public class SmtpOptions
    {
        public const string SectionName = "SmtpSetting";

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587; // Standart güvenli port
        public bool EnableSsl { get; set; } = true; // Güvenli bağlantı
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
    }
}
