namespace Restaurante.Domain.Users.Common.Models
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public int ValidTime { get; set; }
        public TokenConfiguration()
        {
        }
        public TokenConfiguration(string secret, int validTime)
        {
            Secret = secret;
            ValidTime = validTime;
        }
    }
}
