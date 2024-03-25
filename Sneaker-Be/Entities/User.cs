using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Be.Entities
{
    [Table("users")]
    public class User : IdentityUser
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
        public int FacebookAccountId { get; set; }
        public int GoogleAccountId { get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
}
