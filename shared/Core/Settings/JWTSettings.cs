namespace Core.Settings
{
    public record JWTSettings
    {
        public string? Key { get; set; }
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public decimal DurationInMinutes { get; set; }
    }
}
