using AgrarianTradeSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

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

	   public DbSet<Order> Orders { get; set; }

        public DbSet<Assigns> Assigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //put our configurations
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(a => a.OrderId);
                entity.Property(a => a.Status).IsRequired();
                entity.Property(a => a.OrderedDate).IsRequired();
                entity.Property(a => a.TotalQuantity).IsRequired();
                entity.Property(a => a.TotalPrice).IsRequired();
                entity.Property(a => a.DeliveryFee).IsRequired();
                //entity.Property(a => a.DeliveryAddress).IsRequired();


            });

            modelBuilder.Entity<Assigns>(entity =>
            {
                entity.HasKey(a => a.OrderId);
                entity.Property(a => a.PickupDate).IsRequired();
                entity.Property(a => a.DeliveryDate).IsRequired();
                entity.Property(a => a.Status).IsRequired();

                entity.HasOne(a => a.Order)
                    .WithMany(o => o.Assigns)
                    .HasForeignKey(a => a.OrderId);
            });


        }


    }
}
