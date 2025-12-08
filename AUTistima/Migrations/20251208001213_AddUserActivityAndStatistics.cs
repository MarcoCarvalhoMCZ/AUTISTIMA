using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTistima.Migrations
{
    /// <inheritdoc />
    public partial class AddUserActivityAndStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticSnapshots",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalUsuarios = table.Column<int>(type: "int", nullable: false),
                    NovosUsuarios = table.Column<int>(type: "int", nullable: false),
                    UsuariosAtivos = table.Column<int>(type: "int", nullable: false),
                    TotalMaes = table.Column<int>(type: "int", nullable: false),
                    TotalProfissionaisSaude = table.Column<int>(type: "int", nullable: false),
                    TotalProfissionaisEducacao = table.Column<int>(type: "int", nullable: false),
                    TotalEmpresas = table.Column<int>(type: "int", nullable: false),
                    TotalGoverno = table.Column<int>(type: "int", nullable: false),
                    PostsCriados = table.Column<int>(type: "int", nullable: false),
                    Comentarios = table.Column<int>(type: "int", nullable: false),
                    Acolhimentos = table.Column<int>(type: "int", nullable: false),
                    ManejosCompartilhados = table.Column<int>(type: "int", nullable: false),
                    ManejosValidados = table.Column<int>(type: "int", nullable: false),
                    TriagensSolicitadas = table.Column<int>(type: "int", nullable: false),
                    TriagensConcluidas = table.Column<int>(type: "int", nullable: false),
                    OportunidadesPublicadas = table.Column<int>(type: "int", nullable: false),
                    TotalLogins = table.Column<int>(type: "int", nullable: false),
                    VisualizacoesPagina = table.Column<int>(type: "int", nullable: false),
                    BuscasRealizadas = table.Column<int>(type: "int", nullable: false),
                    MensagensChat = table.Column<int>(type: "int", nullable: false),
                    TaxaEngajamento = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MediaAcolhimentosPorPost = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticSnapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoAtividade = table.Column<int>(type: "int", nullable: false),
                    EntidadeRelacionada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntidadeId = table.Column<int>(type: "int", nullable: true),
                    Detalhes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EnderecoIP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Dispositivo = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessaoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TempoNaPagina = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivities_Users_UserId",
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
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(8920));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9270));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 13,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 14,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 15,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 16,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 17,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 18,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 19,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 20,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 21,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 22,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 23,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 24,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 25,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 26,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 27,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 28,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 29,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 30,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 31,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 32,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 33,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 34,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 35,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 36,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 37,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 38,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 39,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 40,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 41,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 42,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 43,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 44,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 45,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 46,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 47,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 48,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 49,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 50,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 51,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9390));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 52,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9390));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 53,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9390));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 54,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 883, DateTimeKind.Utc).AddTicks(9390));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 55,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(70));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 56,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(70));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 57,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(70));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 58,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 59,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 60,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 61,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 62,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 63,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 64,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(110));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 65,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(180));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 66,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(180));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 67,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(180));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 68,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 69,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 70,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 71,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 72,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 73,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 74,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 75,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 76,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 77,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: 78,
                column: "DataCriacao",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 884, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3010));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 9,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(3750));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 10,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                schema: "autistima_sa_sql",
                table: "Services",
                keyColumn: "Id",
                keyValue: 12,
                column: "DataCadastro",
                value: new DateTime(2025, 12, 8, 0, 12, 12, 886, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSnapshots_Data",
                schema: "autistima_sa_sql",
                table: "StatisticSnapshots",
                column: "Data",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_DataHora",
                schema: "autistima_sa_sql",
                table: "UserActivities",
                column: "DataHora");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_TipoAtividade",
                schema: "autistima_sa_sql",
                table: "UserActivities",
                column: "TipoAtividade");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_TipoAtividade_DataHora",
                schema: "autistima_sa_sql",
                table: "UserActivities",
                columns: new[] { "TipoAtividade", "DataHora" });

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                schema: "autistima_sa_sql",
                table: "UserActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId_DataHora",
                schema: "autistima_sa_sql",
                table: "UserActivities",
                columns: new[] { "UserId", "DataHora" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticSnapshots",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "UserActivities",
                schema: "autistima_sa_sql");

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
        }
    }
}
