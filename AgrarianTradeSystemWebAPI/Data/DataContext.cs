using AgrarianTradeSystemWebAPI.Models;
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
			optionsBuilder.UseSqlServer("Data Source=.; initial Catalog=atsdb ; User Id=sa; password=1234; TrustServerCertificate= True");
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Cart> Cart { get; set; }

		public DbSet<Buyer> Buyers { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

	}
}
