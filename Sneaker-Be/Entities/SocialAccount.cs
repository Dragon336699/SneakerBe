namespace Sneaker_Be.Entities
{
    public class SocialAccount
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
