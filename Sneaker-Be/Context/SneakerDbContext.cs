using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Context
{
    public class SneakerDbContext : IdentityDbContext
    {
        public SneakerDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> SneakerUsers { get; set; }
    }
}
