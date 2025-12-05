using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AUTistima.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "autistima_sa_sql");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlossaryTerms",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermoTecnico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExplicacaoSimples = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExemploUso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fonte = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscolaPublica = table.Column<bool>(type: "bit", nullable: false),
                    PossuiSalaRecursos = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TipoPerfil = table.Column<int>(type: "int", nullable: false),
                    SobreMim = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FotoPerfilUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EmpresaAmiga = table.Column<bool>(type: "bit", nullable: false),
                    RegistroProfissional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Especialidade = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NivelSuporte = table.Column<int>(type: "int", nullable: true),
                    PossuiDiagnostico = table.Column<bool>(type: "bit", nullable: false),
                    DataDiagnostico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EscolaNome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EscolaId = table.Column<int>(type: "int", nullable: true),
                    PossuiPAE = table.Column<bool>(type: "bit", nullable: false),
                    EstrategiasCrise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Schools_EscolaId",
                        column: x => x.EscolaId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Children_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manejos",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DicaPratica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaixaEtariaIndicada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NivelSuporteIndicado = table.Column<int>(type: "int", nullable: true),
                    ValidadoPorEspecialista = table.Column<bool>(type: "bit", nullable: false),
                    EspecialistaValidadorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manejos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manejos_Users_EspecialistaValidadorId",
                        column: x => x.EspecialistaValidadorId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Manejos_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requisitos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beneficios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorSalario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PermiteHomeOffice = table.Column<bool>(type: "bit", nullable: false),
                    HorarioFlexivel = table.Column<bool>(type: "bit", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Contato = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LinkExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunities_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    PermitirComentarios = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningRequests",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAluno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodigoIdentificador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdadeAluno = table.Column<int>(type: "int", nullable: true),
                    SerieAno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SinaisObservados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContextoObservacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ParecerProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recomendacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Encaminhamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAvaliacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    ProfessorSolicitanteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfissionalResponsavelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningRequests_Schools_EscolaId",
                        column: x => x.EscolaId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreeningRequests_Users_ProfessorSolicitanteId",
                        column: x => x.ProfessorSolicitanteId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreeningRequests_Users_ProfissionalResponsavelId",
                        column: x => x.ProfissionalResponsavelId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProfissional = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Especialidade = table.Column<int>(type: "int", nullable: false),
                    TipoAtendimento = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistroProfissional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtendeOnline = table.Column<bool>(type: "bit", nullable: false),
                    ValorConsulta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verificado = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PostAcolhimentos",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataAcolhimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAcolhimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAcolhimentos_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostAcolhimentos_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                schema: "autistima_sa_sql",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "autistima_sa_sql",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                columns: new[] { "Id", "Ativo", "Categoria", "DataCriacao", "ExemploUso", "ExplicacaoSimples", "Fonte", "TermoTecnico" },
                values: new object[,]
                {
                    { 1, true, "Diagnóstico", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(7860), null, "Transtorno do Espectro Autista - é uma condição do neurodesenvolvimento que afeta a comunicação, interação social e comportamento. Cada pessoa autista é única.", null, "TEA" },
                    { 2, true, "Comportamento", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8210), null, "Movimentos repetitivos que a pessoa autista faz, como balançar o corpo, bater as mãos ou girar objetos. São formas de autorregulação e não devem ser reprimidas.", null, "Estereotipia" },
                    { 3, true, "Comportamento", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8210), null, "Quando a pessoa autista tem um interesse muito intenso por um assunto específico. Pode ser uma força quando bem direcionado.", null, "Hiperfoco" },
                    { 4, true, "Comportamento", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8210), null, "Uma crise intensa causada por sobrecarga sensorial ou emocional. Não é birra - é o corpo reagindo a algo insuportável. Requer paciência e ambiente calmo.", null, "Meltdown" },
                    { 5, true, "Comportamento", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8210), null, "Quando a pessoa 'desliga' por estar sobrecarregada. Pode ficar quieta, não responder, parecer distante. É uma forma de proteção do cérebro.", null, "Shutdown" },
                    { 6, true, "Sensorial", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8210), null, "Quando os sentidos (audição, visão, tato, olfato, paladar) são mais intensos. Um som normal pode doer, uma luz pode incomodar muito, algumas texturas são insuportáveis.", null, "Sensibilidade Sensorial" },
                    { 7, true, "Comunicação", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8220), null, "Repetir palavras ou frases ouvidas. Pode ser imediata ou depois de um tempo. É uma forma de comunicação e processamento de linguagem.", null, "Ecolalia" },
                    { 8, true, "Educação", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8220), null, "Plano de Atendimento Educacional - documento que a escola deve fazer para adaptar o ensino às necessidades do aluno autista. É um direito garantido por lei.", null, "PAE" },
                    { 9, true, "Terapia", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8220), null, "Análise do Comportamento Aplicada - uma terapia comportamental usada para desenvolver habilidades. Deve ser aplicada de forma ética e respeitosa.", null, "ABA" },
                    { 10, true, "Geral", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8220), null, "Pessoa cujo cérebro funciona de forma diferente do padrão. Inclui autistas, pessoas com TDAH, dislexia e outras condições. Não é doença, é diversidade.", null, "Neurodivergente" },
                    { 11, true, "Diagnóstico", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8220), null, "Quando a pessoa tem mais de uma condição ao mesmo tempo. Por exemplo, autismo junto com TDAH, ansiedade ou epilepsia.", null, "Comorbidade" },
                    { 12, true, "Alimentação", new DateTime(2025, 12, 5, 15, 20, 5, 919, DateTimeKind.Utc).AddTicks(8220), null, "Quando a pessoa aceita poucos alimentos. Está relacionada à sensibilidade sensorial (textura, cor, cheiro). Não é frescura ou falta de educação.", null, "Seletividade Alimentar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "autistima_sa_sql",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "autistima_sa_sql",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "autistima_sa_sql",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "autistima_sa_sql",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "autistima_sa_sql",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Children_EscolaId",
                schema: "autistima_sa_sql",
                table: "Children",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Children_UserId",
                schema: "autistima_sa_sql",
                table: "Children",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryTerms_Categoria",
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                column: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryTerms_TermoTecnico",
                schema: "autistima_sa_sql",
                table: "GlossaryTerms",
                column: "TermoTecnico");

            migrationBuilder.CreateIndex(
                name: "IX_Manejos_Categoria",
                schema: "autistima_sa_sql",
                table: "Manejos",
                column: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Manejos_EspecialistaValidadorId",
                schema: "autistima_sa_sql",
                table: "Manejos",
                column: "EspecialistaValidadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Manejos_UserId",
                schema: "autistima_sa_sql",
                table: "Manejos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_DataCriacao",
                schema: "autistima_sa_sql",
                table: "Opportunities",
                column: "DataCriacao");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_Tipo",
                schema: "autistima_sa_sql",
                table: "Opportunities",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_UserId",
                schema: "autistima_sa_sql",
                table: "Opportunities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAcolhimentos_PostId_UserId",
                schema: "autistima_sa_sql",
                table: "PostAcolhimentos",
                columns: new[] { "PostId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostAcolhimentos_UserId",
                schema: "autistima_sa_sql",
                table: "PostAcolhimentos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                schema: "autistima_sa_sql",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserId",
                schema: "autistima_sa_sql",
                table: "PostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_DataCriacao",
                schema: "autistima_sa_sql",
                table: "Posts",
                column: "DataCriacao");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                schema: "autistima_sa_sql",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_CNPJ",
                schema: "autistima_sa_sql",
                table: "Schools",
                column: "CNPJ",
                unique: true,
                filter: "[CNPJ] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningRequests_EscolaId",
                schema: "autistima_sa_sql",
                table: "ScreeningRequests",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningRequests_ProfessorSolicitanteId",
                schema: "autistima_sa_sql",
                table: "ScreeningRequests",
                column: "ProfessorSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningRequests_ProfissionalResponsavelId",
                schema: "autistima_sa_sql",
                table: "ScreeningRequests",
                column: "ProfissionalResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningRequests_Status",
                schema: "autistima_sa_sql",
                table: "ScreeningRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Bairro",
                schema: "autistima_sa_sql",
                table: "Services",
                column: "Bairro");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Cidade",
                schema: "autistima_sa_sql",
                table: "Services",
                column: "Cidade");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Especialidade",
                schema: "autistima_sa_sql",
                table: "Services",
                column: "Especialidade");

            migrationBuilder.CreateIndex(
                name: "IX_Services_TipoAtendimento",
                schema: "autistima_sa_sql",
                table: "Services",
                column: "TipoAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                schema: "autistima_sa_sql",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "autistima_sa_sql",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "autistima_sa_sql",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Children",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "GlossaryTerms",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Manejos",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Opportunities",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "PostAcolhimentos",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "PostComments",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "ScreeningRequests",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Schools",
                schema: "autistima_sa_sql");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "autistima_sa_sql");
        }
    }
}
