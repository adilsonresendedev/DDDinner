namespace DDDinner.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SectionName { get; set; }
    }
}
