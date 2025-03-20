using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data 
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : IdentityDbContext<User>(dbContextOptions) {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Garante o unique together, caso tivesse mais campos funcionaria também setando apenas alguns como unique together
            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.UserId, p.StockId }));

            builder.Entity<Portfolio>()
                .HasOne(u => u.User)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.UserId);

            builder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = "Common",
                    NormalizedName = "COMMON",
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}