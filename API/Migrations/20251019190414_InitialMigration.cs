using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Permissoes = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    PerfilId = table.Column<int>(type: "INTEGER", maxLength: 50, nullable: false),
                    Idade = table.Column<int>(type: "INTEGER", nullable: false),
                    Sexo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nacionalidade = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EstadoCivil = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Perfil",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Descricao", "Nome", "Permissoes" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Perfil com acesso total ao sistema", "Administrador", "Criar, Ler, Atualizar, Deletar" },
                    { 2, new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Perfil padrão para usuários comuns", "Usuário", "Ler" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Email", "EstadoCivil", "Idade", "Nacionalidade", "Nome", "PerfilId", "Senha", "Sexo" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "admin@admin.com", "Solteiro", 30, "Brasileiro", "Admin", 1, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", "Masculino" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilId",
                table: "Usuario",
                column: "PerfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
