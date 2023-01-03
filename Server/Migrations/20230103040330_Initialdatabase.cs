using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Initialdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Durians",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nama = table.Column<string>(type: "text", nullable: true),
                    Ukuran = table.Column<string>(type: "text", nullable: true),
                    Keadaan = table.Column<string>(type: "text", nullable: true),
                    Harga = table.Column<decimal>(type: "numeric", nullable: false),
                    Diskon = table.Column<decimal>(type: "numeric", nullable: false),
                    Stok = table.Column<int>(type: "integer", nullable: false),
                    PembelianPada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gambar = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durians", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Keranjangs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Harga = table.Column<decimal>(type: "numeric", nullable: false),
                    Diskon = table.Column<decimal>(type: "numeric", nullable: false),
                    Stok = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keranjangs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Penggunas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Namapengguna = table.Column<string>(type: "text", nullable: true),
                    Katasandi = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telepon = table.Column<string>(type: "text", nullable: true),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    Dana = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DibuatPada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penggunas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PesananDurians",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IDPesanan = table.Column<int>(type: "integer", nullable: false),
                    IDDurian = table.Column<int>(type: "integer", nullable: false),
                    Harga = table.Column<decimal>(type: "numeric", nullable: false),
                    Diskon = table.Column<decimal>(type: "numeric", nullable: false),
                    Stok = table.Column<int>(type: "integer", nullable: false),
                    TotalHarga = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PesananDurians", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pesanans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    NomorPesanan = table.Column<string>(type: "text", nullable: true),
                    TotalPesanan = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusPesanan = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesanans", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Durians");

            migrationBuilder.DropTable(
                name: "Keranjangs");

            migrationBuilder.DropTable(
                name: "Penggunas");

            migrationBuilder.DropTable(
                name: "PesananDurians");

            migrationBuilder.DropTable(
                name: "Pesanans");
        }
    }
}
