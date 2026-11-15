using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudEF.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Uf = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Local = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Gratuito = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CidadeId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RegistradoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interesses_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interesses_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Música" },
                    { 2, "Teatro" },
                    { 3, "Exposições de arte" },
                    { 4, "Feiras culturais" },
                    { 5, "Dança" },
                    { 6, "Cinema" },
                    { 7, "Oficinas" },
                    { 8, "Outros" }
                });

            migrationBuilder.InsertData(
                table: "Cidades",
                columns: new[] { "Id", "Nome", "Uf" },
                values: new object[,]
                {
                    { 1, "São José", "SC" },
                    { 2, "Florianópolis", "SC" },
                    { 3, "Palhoça", "SC" },
                    { 4, "Biguaçu", "SC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nome",
                table: "Categorias",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CategoriaId",
                table: "Eventos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CidadeId",
                table: "Eventos",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_DataHora",
                table: "Eventos",
                column: "DataHora");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_UsuarioId",
                table: "Eventos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Interesses_EventoId",
                table: "Interesses",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Interesses_UsuarioId_EventoId",
                table: "Interesses",
                columns: new[] { "UsuarioId", "EventoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CidadeId",
                table: "Usuarios",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interesses");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cidades");
        }
    }
}
