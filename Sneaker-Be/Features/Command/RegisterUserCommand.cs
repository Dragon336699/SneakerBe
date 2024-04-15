using MediatR;

namespace Sneaker_Be.Features.Command
{
    public class RegisterUserCommand : IRequest<string>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string phone_number { get; set; }
        public int role_id { get; set; } 
        public DateTime date_of_birth { get; set; }
        public RegisterUserCommand(string fullname, string address, string password, string phoneNumber, DateTime dateOfBirth)
        {
            this.FullName = fullname;
            this.Address = address;
            this.Password = password;
            this.phone_number = phoneNumber;
            this.date_of_birth = dateOfBirth;
        }
    }
}
