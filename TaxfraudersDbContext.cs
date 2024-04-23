using Microsoft.EntityFrameworkCore;
using taxfrauders.Models;

public class TaxfraudersDbContext : DbContext
{
    public TaxfraudersDbContext(
        DbContextOptions<TaxfraudersDbContext> options) : base(options)
    {

    }

public DbSet<taxfrauders.Models.Workplace> Workplace { get; set; } = default!;
}