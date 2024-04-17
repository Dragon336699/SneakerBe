namespace Sneaker_Be.Dtos
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string phone_number { get; set; }
        public int role_id { get; set; }
        public DateTime date_of_birth { get; set; }
    }
}
