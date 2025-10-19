namespace API.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}