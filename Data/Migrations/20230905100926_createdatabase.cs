using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektDaniel.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rola",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "użytkownik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imię = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Id_przełożonego = table.Column<int>(type: "int", nullable: true),
                    Id_rola = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_użytkownik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_użytkownik_rola_Id_rola",
                        column: x => x.Id_rola,
                        principalTable: "rola",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_użytkownik_użytkownik_Id_przełożonego",
                        column: x => x.Id_przełożonego,
                        principalTable: "użytkownik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "wniosek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plik = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_osoby_zgłaszającej = table.Column<int>(type: "int", nullable: true),
                    Id_osoby_zaakceptował = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wniosek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wniosek_użytkownik_Id_osoby_zaakceptował",
                        column: x => x.Id_osoby_zaakceptował,
                        principalTable: "użytkownik",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_wniosek_użytkownik_Id_osoby_zgłaszającej",
                        column: x => x.Id_osoby_zgłaszającej,
                        principalTable: "użytkownik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_użytkownik_Id_przełożonego",
                table: "użytkownik",
                column: "Id_przełożonego");

            migrationBuilder.CreateIndex(
                name: "IX_użytkownik_Id_rola",
                table: "użytkownik",
                column: "Id_rola");

            migrationBuilder.CreateIndex(
                name: "UQ__użytkown__A9D10534C7C7CBD8",
                table: "użytkownik",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_wniosek_Id_osoby_zaakceptował",
                table: "wniosek",
                column: "Id_osoby_zaakceptował");

            migrationBuilder.CreateIndex(
                name: "IX_wniosek_Id_osoby_zgłaszającej",
                table: "wniosek",
                column: "Id_osoby_zgłaszającej");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wniosek");

            migrationBuilder.DropTable(
                name: "użytkownik");

            migrationBuilder.DropTable(
                name: "rola");
        }
    }
}
