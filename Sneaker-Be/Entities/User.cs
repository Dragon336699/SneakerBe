using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Be.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string phone_number { get; set; }
        public int is_active { get; set; }
        public int role_id { get; set; }
        public int facebook_account_id { get; set; }
        public int google_account_id { get; set; }
        public DateTime created_at {  get; set; }
        public DateTime date_of_birth { get; set; }
        public DateTime updated_at { get; set;}
    }
}
