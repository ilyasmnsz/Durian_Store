using Microsoft.EntityFrameworkCore;
using Durian.Models;

namespace Durian.Data;

public class DurianContext : DbContext
{
    public DurianContext(DbContextOptions<DurianContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
        public DbSet<DurianItemDTO> DurianItems { get; set; }
        public DbSet<PenjualDTO> Penjuals { get; set; }
        public DbSet<PembeliDTO> Pembelis { get; set; }
}