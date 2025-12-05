using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTistima.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationsAndChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemetenteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DestinatarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lida = table.Column<bool>(type: "bit", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_RemetenteId",
                        column: x => x.RemetenteId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario1Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Usuario2Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UltimaMensagem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_Usuario1Id",
                        column: x => x.Usuario1Id,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_Usuario2Id",
                        column: x => x.Usuario2Id,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Lida = table.Column<bool>(type: "bit", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5780));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5810));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5730));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 13,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5730));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 14,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 15,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 16,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 17,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 18,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 19,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 20,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 21,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 22,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 23,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5780));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 24,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 25,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 26,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 27,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 28,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 29,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 30,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 31,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 32,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 33,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 34,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 35,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 36,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5810));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 37,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5810));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 38,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5810));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 39,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 40,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 41,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 42,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 43,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 44,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 45,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 46,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 47,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 48,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 49,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 50,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 51,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5850));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 52,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5850));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 53,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5850));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 54,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(5850));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 55,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7070));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 56,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 57,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 58,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 59,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 60,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 61,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 62,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 63,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 64,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 65,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 66,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 67,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 68,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 69,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 70,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 71,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 72,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 73,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 74,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 75,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 76,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7180));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 77,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7180));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 78,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 227, DateTimeKind.Utc).AddTicks(7180));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(4950));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(5680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(6000));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(6000));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 43, 34, 230, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_DataEnvio",
                schema: "autistima_sa_sql",
                table: "ChatMessages",
                column: "DataEnvio");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_DestinatarioId",
                schema: "autistima_sa_sql",
                table: "ChatMessages",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RemetenteId",
                schema: "autistima_sa_sql",
                table: "ChatMessages",
                column: "RemetenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UltimaMensagem",
                schema: "autistima_sa_sql",
                table: "Conversations",
                column: "UltimaMensagem");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_Usuario1Id_Usuario2Id",
                schema: "autistima_sa_sql",
                table: "Conversations",
                columns: new[] { "Usuario1Id", "Usuario2Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_Usuario2Id",
                schema: "autistima_sa_sql",
                table: "Conversations",
                column: "Usuario2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DataCriacao",
                schema: "autistima_sa_sql",
                table: "Notifications",
                column: "DataCriacao");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Lida",
                schema: "autistima_sa_sql",
                table: "Notifications",
                column: "Lida");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                schema: "autistima_sa_sql",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Conversations",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "autistima_sa_sql");

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1060));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 13,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 14,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 15,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 16,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 17,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 18,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 19,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 20,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 21,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 22,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 23,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1060));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 24,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1060));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 25,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1090));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 26,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 27,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 28,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 29,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 30,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 31,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 32,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 33,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 34,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 35,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 36,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 37,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 38,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 39,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 40,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1130));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 41,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 42,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 43,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 44,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 45,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 46,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 47,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 48,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 49,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 50,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 51,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 52,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 53,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 54,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 55,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 56,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1990));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 57,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(1990));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 58,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 59,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 60,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 61,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 62,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 63,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 64,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 65,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 66,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 67,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 68,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 69,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 70,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 71,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 72,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 73,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 74,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 75,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 76,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2050));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 77,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2060));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 78,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 974, DateTimeKind.Utc).AddTicks(2060));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1020));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(1850));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(2230));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(2240));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 22, 8, 46, 977, DateTimeKind.Utc).AddTicks(2240));
        }
    }
}
