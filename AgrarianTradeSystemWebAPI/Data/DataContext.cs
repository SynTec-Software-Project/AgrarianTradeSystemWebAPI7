using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Data
{
	public class DataContext : DbContext

	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=atsdb;Trusted_connection=true;TrustServerCertificate=true;");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<User>().ToTable("Users");
        //    modelBuilder.Entity<Farmer>().ToTable("Farmers");
        //    modelBuilder.Entity<Courier>().ToTable("Couriers");
        //}

        public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Farmer> Farmers { get; set; }
		public DbSet<Courier> Couriers { get; set; }
		
    }
}
