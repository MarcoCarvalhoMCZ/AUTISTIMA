using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTistima.Migrations
{
    /// <inheritdoc />
    public partial class AddPanicAlertSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PanicAlerts",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NivelUrgencia = table.Column<int>(type: "int", nullable: false),
                    LinkWhatsApp = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    DataConfirmacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotaAtendimento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataAtendimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanicAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanicAlerts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(7900));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 13,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 14,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 15,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 16,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 17,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 18,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 19,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 20,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 21,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 22,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8310));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 23,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 24,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 25,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 26,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 27,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 28,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 29,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 30,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 31,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 32,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 33,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 34,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 35,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 36,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 37,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 38,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 39,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 40,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 41,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 42,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 43,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 44,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 45,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 46,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 47,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 48,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 49,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 50,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 51,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 52,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 53,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 54,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(8440));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 55,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 56,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 57,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 58,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 59,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 60,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 61,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 62,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 63,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 64,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 65,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 66,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 67,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 68,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 69,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 70,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 71,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 72,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 73,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 74,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 75,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 76,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 77,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 78,
                column: "DataCriacao",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 788, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3430));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(3800));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(4470));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(4480));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCadastro",
                value: new DateTime(2026, 1, 9, 12, 7, 1, 791, DateTimeKind.Utc).AddTicks(4480));

            migrationBuilder.CreateIndex(
                name: "IX_PanicAlerts_DataCriacao",
                schema: "autistima_sa_sql",
                table: "PanicAlerts",
                column: "DataCriacao");

            migrationBuilder.CreateIndex(
                name: "IX_PanicAlerts_NivelUrgencia",
                schema: "autistima_sa_sql",
                table: "PanicAlerts",
                column: "NivelUrgencia");

            migrationBuilder.CreateIndex(
                name: "IX_PanicAlerts_Status",
                schema: "autistima_sa_sql",
                table: "PanicAlerts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PanicAlerts_UserId",
                schema: "autistima_sa_sql",
                table: "PanicAlerts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PanicAlerts_UserId_Status",
                schema: "autistima_sa_sql",
                table: "PanicAlerts",
                columns: new[] { "UserId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PanicAlerts",
                schema: "autistima_sa_sql");

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 13,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 14,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 15,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 16,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 17,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 18,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 19,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 20,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 21,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 22,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 23,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 24,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 25,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 26,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 27,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 28,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 29,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 30,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 31,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 32,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 33,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 34,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 35,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 36,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 37,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 38,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 39,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 40,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 41,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 42,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 43,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 44,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 45,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 46,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 47,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 48,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 49,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 50,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 51,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 52,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 53,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 54,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(640));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 55,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 56,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 57,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1310));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 58,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1310));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 59,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1310));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 60,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 61,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 62,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 63,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 64,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 65,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 66,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 67,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 68,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 69,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 70,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 71,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 72,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 73,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 74,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 75,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 76,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 77,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 78,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 459, DateTimeKind.Utc).AddTicks(1420));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4490));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4510));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4830));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(4840));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(5160));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(5170));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 15, 20, 9, 11, 461, DateTimeKind.Utc).AddTicks(5170));
        }
    }
}
