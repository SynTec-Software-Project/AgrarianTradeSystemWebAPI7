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
			//optionsBuilder.UseSqlServer("Data Source=.; initial Catalog=atsdb ; User Id=sa; password=1234; TrustServerCertificate= True");
			optionsBuilder.UseSqlServer("Data Source=SQL5106.site4now.net;Initial Catalog=db_aa6c3c_atsdb;User Id=db_aa6c3c_atsdb_admin;Password=syntec@123");
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Cart> Cart { get; set; }

		public DbSet<Buyer> Buyers { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

	}
}
