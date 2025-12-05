using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTistima.Migrations
{
    /// <inheritdoc />
    public partial class AddPushSubscriptionsPWA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PushSubscriptions",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    P256dh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Auth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimoEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushSubscriptions_Users_UserId",
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
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1140));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 13,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 14,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 15,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 16,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 17,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 18,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 19,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 20,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 21,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 22,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 23,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 24,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 25,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 26,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 27,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 28,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 29,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 30,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 31,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 32,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 33,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 34,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 35,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 36,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 37,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 38,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 39,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 40,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 41,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 42,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 43,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 44,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 45,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 46,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 47,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 48,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 49,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 50,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 51,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 52,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 53,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 54,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(1710));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 55,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 56,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 57,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 58,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 59,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 60,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 61,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 62,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 63,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 64,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 65,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 66,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 67,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 68,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 69,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 70,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 71,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 72,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 73,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 74,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 75,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 76,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 77,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 78,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 854, DateTimeKind.Utc).AddTicks(2670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(670));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1090));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1090));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1500));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1890));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 5, 23, 3, 57, 857, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.CreateIndex(
                name: "IX_PushSubscriptions_Endpoint",
                schema: "autistima_sa_sql",
                table: "PushSubscriptions",
                column: "Endpoint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PushSubscriptions_UserId",
                schema: "autistima_sa_sql",
                table: "PushSubscriptions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushSubscriptions",
                schema: "autistima_sa_sql");

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
        }
    }
}
