using Proiect.Models;
using Microsoft.EntityFrameworkCore;

namespace Proiect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Rentings> Rentings { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
                       // One to Many

            builder.Entity<User>()
                    .HasMany(m1 => m1.Orders)
                    .WithOne(m2 => m2.User);

            builder.Entity<Orders>()
                .HasOne(m2 => m2.User)
                .WithMany(m1 => m1.Orders)
                .HasForeignKey(m2=>m2.UserId);


            // One to One

            builder.Entity<User>()
                .HasOne(m5 => m5.UserDetails)
                .WithOne(m6 => m6.User)
                .HasForeignKey<UserDetails>(m6 => m6.UserId);

            // Many to Many

            builder.Entity<Rentings>().HasKey(mr => new { mr.UserId, mr.ProductsId });

            builder.Entity<Rentings>()
                   .HasOne<User>(mr => mr.User)
                   .WithMany(m3 => m3.Rentings)
                   .HasForeignKey(mr => mr.UserId);


            builder.Entity<Rentings>()
                   .HasOne<Products>(mr => mr.Products)
                   .WithMany(m4 => m4.Rentings)
                   .HasForeignKey(mr => mr.ProductsId);

            base.OnModelCreating(builder);
        }
    }
}
