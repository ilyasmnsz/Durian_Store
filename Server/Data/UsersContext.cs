using Microsoft.EntityFrameworkCore;
using EDurianstore.Models;

namespace EDurianstore.Data;

public class UsersContext : DbContext
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
        public DbSet<Penggunas> Penggunas { get; set; }
        public DbSet<Durians> Durians { get; set; }
        public DbSet<Keranjangs> Keranjangs { get; set; }
        public DbSet<Pesanans> Pesanans { get; set; }
        public DbSet<PesananDurians> PesananDurians { get; set; }
}