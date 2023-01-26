﻿// <auto-generated />
using Durian.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(DurianContext))]
    [Migration("20230126035551_initialdatabase")]
    partial class initialdatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Belanja.Models.KeranjangDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DurianDTO_Id")
                        .HasColumnType("integer");

                    b.Property<int>("Jumlahdurian")
                        .HasColumnType("integer");

                    b.Property<int>("UserDTO_Id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DurianDTO_Id");

                    b.HasIndex("UserDTO_Id");

                    b.ToTable("Keranjangs");
                });

            modelBuilder.Entity("Durian.Models.DurianDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Gambardurian")
                        .HasColumnType("text");

                    b.Property<decimal>("Hargadurian")
                        .HasColumnType("numeric");

                    b.Property<string>("Keadaandurian")
                        .HasColumnType("text");

                    b.Property<string>("Namadurian")
                        .HasColumnType("text");

                    b.Property<int>("Stokdurian")
                        .HasColumnType("integer");

                    b.Property<string>("Tentangdurian")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Durians");
                });

            modelBuilder.Entity("User.Models.UserDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nama")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Telepon")
                        .HasColumnType("text");

                    b.Property<string>("Tipe")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Belanja.Models.KeranjangDTO", b =>
                {
                    b.HasOne("Durian.Models.DurianDTO", "DurianDTO")
                        .WithMany("KeranjangDTOs")
                        .HasForeignKey("DurianDTO_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Keranjang_Durian");

                    b.HasOne("User.Models.UserDTO", "UserDTO")
                        .WithMany("KeranjangDTOs")
                        .HasForeignKey("UserDTO_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Keranjang_User");

                    b.Navigation("DurianDTO");

                    b.Navigation("UserDTO");
                });

            modelBuilder.Entity("Durian.Models.DurianDTO", b =>
                {
                    b.Navigation("KeranjangDTOs");
                });

            modelBuilder.Entity("User.Models.UserDTO", b =>
                {
                    b.Navigation("KeranjangDTOs");
                });
#pragma warning restore 612, 618
        }
    }
}
