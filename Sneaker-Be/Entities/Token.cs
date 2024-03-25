namespace Sneaker_Be.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string TokenString {  get; set; }
        public string TokenType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int revoked {  get; set; }
        public int Expried { get; set; }
        public int UserId { get; set; }
    }
}
