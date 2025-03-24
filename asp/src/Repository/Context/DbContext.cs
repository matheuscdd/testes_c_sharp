using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context;

public class ApplicationDbContext: DbContext
{
    public DbSet<User> User { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
}