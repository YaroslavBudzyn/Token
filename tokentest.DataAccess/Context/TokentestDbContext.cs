using Microsoft.EntityFrameworkCore;
using tokentest.DataAccess.UserManagement.Entities;

namespace tokentest.DataAccess.Context
{
    public class TokentestDbContext:DbContext
    {
        public TokentestDbContext(DbContextOptions<TokentestDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
