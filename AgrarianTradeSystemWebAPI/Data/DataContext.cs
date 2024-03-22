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
			optionsBuilder.UseSqlServer("Data Source=SQL5106.site4now.net;Initial Catalog=db_aa6c3c_syntecproject;User Id=db_aa6c3c_syntecproject_admin;Password=syntec@123");
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Farmer> Farmers { get; set; }
		public DbSet<Courier> Couriers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Cart> Cart { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Orders> Orders { get; set; }

	}
}
