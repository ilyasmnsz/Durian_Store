using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Durian.Models;
using Belanja.Models;
using User.Models;

namespace Durian.Data;

public class DurianContext : DbContext
{
    public DurianContext(DbContextOptions<DurianContext> options) : base(options)
    {}
        public DbSet<DurianDTO> Durians { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<KeranjangDTO> Keranjangs  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeranjangDTO>()
                .HasOne(p => p.UserDTO)
                .WithMany(b => b.KeranjangDTOs)
                .HasForeignKey(t => t.UserDTO_Id)
                .HasConstraintName("FK_Keranjang_User")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<KeranjangDTO>()
                .HasOne(q => q.DurianDTO)
                .WithMany(c => c.KeranjangDTOs)
                .HasForeignKey(u => u.DurianDTO_Id)
                .HasConstraintName("FK_Keranjang_Durian")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
}
        