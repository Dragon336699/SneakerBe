using MediatR;

namespace Sneaker_Be.Features.Command.UserCommand
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
            FullName = fullname;
            Address = address;
            Password = password;
            phone_number = phoneNumber;
            date_of_birth = dateOfBirth;
        }
    }
}
