using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AUTistima.Models;

namespace AUTistima.Data;

/// <summary>
/// Contexto do banco de dados AUTistima
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    // DbSets para as entidades
    public DbSet<Child> Children { get; set; } = null!;
    public DbSet<School> Schools { get; set; } = null!;
    public DbSet<Manejo> Manejos { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<PostAcolhimento> PostAcolhimentos { get; set; } = null!;
    public DbSet<PostComment> PostComments { get; set; } = null!;
    public DbSet<GlossaryTerm> GlossaryTerms { get; set; } = null!;
    public DbSet<Opportunity> Opportunities { get; set; } = null!;
    public DbSet<ScreeningRequest> ScreeningRequests { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configuração do schema
        builder.HasDefaultSchema("autistima_sa_sql");
        
        // ApplicationUser - Configurações adicionais
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(e => e.NomeCompleto).HasMaxLength(150);
            entity.Property(e => e.CNPJ).HasMaxLength(18);
            entity.Property(e => e.NomeEmpresa).HasMaxLength(200);
            entity.Property(e => e.RegistroProfissional).HasMaxLength(50);
        });
        
        // Child - Filho
        builder.Entity<Child>(entity =>
        {
            entity.ToTable("Children");
            entity.HasIndex(e => e.UserId);
            
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Filhos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Escola)
                .WithMany(s => s.Alunos)
                .HasForeignKey(e => e.EscolaId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        // School - Escola
        builder.Entity<School>(entity =>
        {
            entity.ToTable("Schools");
            entity.HasIndex(e => e.CNPJ).IsUnique();
        });
        
        // Manejo
        builder.Entity<Manejo>(entity =>
        {
            entity.ToTable("Manejos");
            entity.HasIndex(e => e.Categoria);
            entity.HasIndex(e => e.UserId);
            
            entity.HasOne(e => e.Autor)
                .WithMany(u => u.Manejos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.EspecialistaValidador)
                .WithMany()
                .HasForeignKey(e => e.EspecialistaValidadorId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        // Post
        builder.Entity<Post>(entity =>
        {
            entity.ToTable("Posts");
            entity.HasIndex(e => e.DataCriacao);
            entity.HasIndex(e => e.UserId);
            
            entity.HasOne(e => e.Autor)
                .WithMany(u => u.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // PostAcolhimento
        builder.Entity<PostAcolhimento>(entity =>
        {
            entity.ToTable("PostAcolhimentos");
            entity.HasIndex(e => new { e.PostId, e.UserId }).IsUnique();
            
            entity.HasOne(e => e.Post)
                .WithMany(p => p.Acolhimentos)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Acolhimentos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // PostComment
        builder.Entity<PostComment>(entity =>
        {
            entity.ToTable("PostComments");
            entity.HasIndex(e => e.PostId);
            
            entity.HasOne(e => e.Post)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.Autor)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // GlossaryTerm
        builder.Entity<GlossaryTerm>(entity =>
        {
            entity.ToTable("GlossaryTerms");
            entity.HasIndex(e => e.TermoTecnico);
            entity.HasIndex(e => e.Categoria);
        });
        
        // Opportunity
        builder.Entity<Opportunity>(entity =>
        {
            entity.ToTable("Opportunities");
            entity.HasIndex(e => e.Tipo);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.DataCriacao);
            
            entity.HasOne(e => e.Criador)
                .WithMany(u => u.OportunidadesCriadas)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // ScreeningRequest
        builder.Entity<ScreeningRequest>(entity =>
        {
            entity.ToTable("ScreeningRequests");
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.EscolaId);
            entity.HasIndex(e => e.ProfessorSolicitanteId);
            
            entity.HasOne(e => e.Escola)
                .WithMany(s => s.Triagens)
                .HasForeignKey(e => e.EscolaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.ProfessorSolicitante)
                .WithMany()
                .HasForeignKey(e => e.ProfessorSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.ProfissionalResponsavel)
                .WithMany()
                .HasForeignKey(e => e.ProfissionalResponsavelId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        // Service
        builder.Entity<Service>(entity =>
        {
            entity.ToTable("Services");
            entity.HasIndex(e => e.Especialidade);
            entity.HasIndex(e => e.TipoAtendimento);
            entity.HasIndex(e => e.Cidade);
            entity.HasIndex(e => e.Bairro);
            
            entity.HasOne(e => e.Profissional)
                .WithMany(u => u.Servicos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        // Seed de dados iniciais para o Glossário
        SeedGlossaryTerms(builder);
    }
    
    private void SeedGlossaryTerms(ModelBuilder builder)
    {
        builder.Entity<GlossaryTerm>().HasData(
            new GlossaryTerm
            {
                Id = 1,
                TermoTecnico = "TEA",
                ExplicacaoSimples = "Transtorno do Espectro Autista - é uma condição do neurodesenvolvimento que afeta a comunicação, interação social e comportamento. Cada pessoa autista é única.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 2,
                TermoTecnico = "Estereotipia",
                ExplicacaoSimples = "Movimentos repetitivos que a pessoa autista faz, como balançar o corpo, bater as mãos ou girar objetos. São formas de autorregulação e não devem ser reprimidas.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 3,
                TermoTecnico = "Hiperfoco",
                ExplicacaoSimples = "Quando a pessoa autista tem um interesse muito intenso por um assunto específico. Pode ser uma força quando bem direcionado.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 4,
                TermoTecnico = "Meltdown",
                ExplicacaoSimples = "Uma crise intensa causada por sobrecarga sensorial ou emocional. Não é birra - é o corpo reagindo a algo insuportável. Requer paciência e ambiente calmo.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 5,
                TermoTecnico = "Shutdown",
                ExplicacaoSimples = "Quando a pessoa 'desliga' por estar sobrecarregada. Pode ficar quieta, não responder, parecer distante. É uma forma de proteção do cérebro.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 6,
                TermoTecnico = "Sensibilidade Sensorial",
                ExplicacaoSimples = "Quando os sentidos (audição, visão, tato, olfato, paladar) são mais intensos. Um som normal pode doer, uma luz pode incomodar muito, algumas texturas são insuportáveis.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 7,
                TermoTecnico = "Ecolalia",
                ExplicacaoSimples = "Repetir palavras ou frases ouvidas. Pode ser imediata ou depois de um tempo. É uma forma de comunicação e processamento de linguagem.",
                Categoria = "Comunicação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 8,
                TermoTecnico = "PAE",
                ExplicacaoSimples = "Plano de Atendimento Educacional - documento que a escola deve fazer para adaptar o ensino às necessidades do aluno autista. É um direito garantido por lei.",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 9,
                TermoTecnico = "ABA",
                ExplicacaoSimples = "Análise do Comportamento Aplicada - uma terapia comportamental usada para desenvolver habilidades. Deve ser aplicada de forma ética e respeitosa.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 10,
                TermoTecnico = "Neurodivergente",
                ExplicacaoSimples = "Pessoa cujo cérebro funciona de forma diferente do padrão. Inclui autistas, pessoas com TDAH, dislexia e outras condições. Não é doença, é diversidade.",
                Categoria = "Geral",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 11,
                TermoTecnico = "Comorbidade",
                ExplicacaoSimples = "Quando a pessoa tem mais de uma condição ao mesmo tempo. Por exemplo, autismo junto com TDAH, ansiedade ou epilepsia.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 12,
                TermoTecnico = "Seletividade Alimentar",
                ExplicacaoSimples = "Quando a pessoa aceita poucos alimentos. Está relacionada à sensibilidade sensorial (textura, cor, cheiro). Não é frescura ou falta de educação.",
                Categoria = "Alimentação",
                DataCriacao = DateTime.UtcNow
            }
        );
    }
}
