using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class initialdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Durians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Namadurian = table.Column<string>(type: "text", nullable: true),
                    Tentangdurian = table.Column<string>(type: "text", nullable: true),
                    Keadaandurian = table.Column<string>(type: "text", nullable: true),
                    Hargadurian = table.Column<decimal>(type: "numeric", nullable: false),
                    Stokdurian = table.Column<int>(type: "integer", nullable: false),
                    Gambardurian = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telepon = table.Column<string>(type: "text", nullable: true),
                    Tipe = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keranjangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Jumlahdurian = table.Column<int>(type: "integer", nullable: false),
                    UserDTOId = table.Column<int>(name: "UserDTO_Id", type: "integer", nullable: false),
                    DurianDTOId = table.Column<int>(name: "DurianDTO_Id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keranjangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keranjang_Durian",
                        column: x => x.DurianDTOId,
                        principalTable: "Durians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keranjang_User",
                        column: x => x.UserDTOId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keranjangs_DurianDTO_Id",
                table: "Keranjangs",
                column: "DurianDTO_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Keranjangs_UserDTO_Id",
                table: "Keranjangs",
                column: "UserDTO_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keranjangs");

            migrationBuilder.DropTable(
                name: "Durians");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
