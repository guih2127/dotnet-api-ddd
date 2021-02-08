namespace Api.Domain.Security
{
    public class TokenConfiguration
    {
        public string Audidence { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}