namespace Tecman.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int MinutesRefreshToken { get; set; }

        public int MinutesAccessToken { get; set; }

        public int DaysToExpiry { get; set; }
    }
}
