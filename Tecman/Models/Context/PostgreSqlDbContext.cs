/**
 * Created:
 * Date: 
 * Modified: Roberto Alvarus
 * Date: January, 03, 2022
 *
 * Data base - Front
 * 
 */

using Tecman.Models;
using Microsoft.EntityFrameworkCore;

namespace Tecman.Models.Context
{
    public class PostgreSqlDbContext : DbContext
    {
        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
        {
        }

        public DbSet<UserToken> Token { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TokenType> TokenType { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatus { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientAddress> ClientAddress { get; set; }
        public DbSet<OrderService> OrderService { get; set; }
        public DbSet<OrderServiceStatus> OrderServiceStatus { get; set; }
        public DbSet<TechnicalVisit> TechnicalVisit { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Piece> Piece { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            //modelBuilder.Entity<User>()
            //    .HasOne(a => a.token)
            //    .WithOne(a => a.user)
            //    .HasForeignKey<UserToken>(a => a.user_token_id);
                
        }
    }
}
