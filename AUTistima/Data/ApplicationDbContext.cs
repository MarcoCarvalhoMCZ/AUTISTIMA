using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AUTistima.Models;
using AUTistima.Models.Enums;

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
    public DbSet<SystemConfiguration> SystemConfigurations { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
    public DbSet<Conversation> Conversations { get; set; } = null!;
    public DbSet<PushSubscription> PushSubscriptions { get; set; } = null!;
    
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
        
        // Notification
        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Lida);
            entity.HasIndex(e => e.DataCriacao);
            
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Notificacoes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // ChatMessage
        builder.Entity<ChatMessage>(entity =>
        {
            entity.ToTable("ChatMessages");
            entity.HasIndex(e => e.RemetenteId);
            entity.HasIndex(e => e.DestinatarioId);
            entity.HasIndex(e => e.DataEnvio);
            
            entity.HasOne(e => e.Remetente)
                .WithMany(u => u.MensagensEnviadas)
                .HasForeignKey(e => e.RemetenteId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Destinatario)
                .WithMany(u => u.MensagensRecebidas)
                .HasForeignKey(e => e.DestinatarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Conversation
        builder.Entity<Conversation>(entity =>
        {
            entity.ToTable("Conversations");
            entity.HasIndex(e => new { e.Usuario1Id, e.Usuario2Id }).IsUnique();
            entity.HasIndex(e => e.UltimaMensagem);
            
            entity.HasOne(e => e.Usuario1)
                .WithMany()
                .HasForeignKey(e => e.Usuario1Id)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Usuario2)
                .WithMany()
                .HasForeignKey(e => e.Usuario2Id)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // PushSubscription - Para notificações push do PWA
        builder.Entity<PushSubscription>(entity =>
        {
            entity.ToTable("PushSubscriptions");
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Endpoint).IsUnique();
            
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Seed de dados iniciais para o Glossário
        SeedGlossaryTerms(builder);
        
        // Seed de CAPS de Maceió
        SeedServicesCapsMaceio(builder);
        
        // Seed de Manejos Iniciais
        SeedManejosIniciais(builder);
    }
    
    private void SeedGlossaryTerms(ModelBuilder builder)
    {
        builder.Entity<GlossaryTerm>().HasData(
            // === DIAGNÓSTICO ===
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
                Id = 11,
                TermoTecnico = "Comorbidade",
                ExplicacaoSimples = "Quando a pessoa tem mais de uma condição ao mesmo tempo. Por exemplo, autismo junto com TDAH, ansiedade ou epilepsia.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 13,
                TermoTecnico = "DSM-5",
                ExplicacaoSimples = "Manual de Diagnóstico e Estatística dos Transtornos Mentais, 5ª edição. É o livro que os profissionais usam para diagnosticar autismo e outras condições.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 14,
                TermoTecnico = "CID-11",
                ExplicacaoSimples = "Classificação Internacional de Doenças da OMS. Usada para registro médico oficial. O código do autismo é 6A02.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 15,
                TermoTecnico = "Nível de Suporte",
                ExplicacaoSimples = "Classificação de quanto apoio a pessoa autista precisa: Nível 1 (precisa de apoio), Nível 2 (precisa de apoio substancial), Nível 3 (precisa de apoio muito substancial).",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 16,
                TermoTecnico = "Diagnóstico Tardio",
                ExplicacaoSimples = "Quando a pessoa descobre que é autista na adolescência ou idade adulta. Muitas mulheres recebem diagnóstico tardio porque os sinais são diferentes dos meninos.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 17,
                TermoTecnico = "Masking/Camuflagem",
                ExplicacaoSimples = "Quando a pessoa autista 'esconde' seus traços para parecer neurotípica. É muito cansativo e pode causar burnout. Comum em mulheres autistas.",
                Categoria = "Diagnóstico",
                DataCriacao = DateTime.UtcNow
            },
            
            // === COMPORTAMENTO ===
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
                Id = 18,
                TermoTecnico = "Stimming",
                ExplicacaoSimples = "Comportamentos de autoestimulação como balançar, girar, apertar objetos. Ajuda a pessoa autista a se regular. Não deve ser proibido, apenas redirecionado se necessário.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 19,
                TermoTecnico = "Rotina Rígida",
                ExplicacaoSimples = "Necessidade de manter as coisas sempre iguais. Mudanças inesperadas podem causar muita ansiedade. Ajuda ter um calendário visual e preparar antecipadamente.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 20,
                TermoTecnico = "Comportamento Restritivo",
                ExplicacaoSimples = "Padrões de comportamento repetitivos e interesses muito específicos. Faz parte do autismo e pode ser uma grande força quando direcionado.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 21,
                TermoTecnico = "Burnout Autista",
                ExplicacaoSimples = "Esgotamento extremo causado pelo esforço de se adaptar ao mundo neurotípico. Sintomas: cansaço extremo, perda de habilidades, aumento de crises.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 22,
                TermoTecnico = "Autolesão",
                ExplicacaoSimples = "Quando a pessoa se machuca durante uma crise ou sobrecarga. Não é 'querer atenção' - é uma resposta à dor interna. Precisa de acompanhamento profissional.",
                Categoria = "Comportamento",
                DataCriacao = DateTime.UtcNow
            },
            
            // === SENSORIAL ===
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
                Id = 23,
                TermoTecnico = "Hipersensibilidade",
                ExplicacaoSimples = "Quando os sentidos são MUITO aguçados. Luzes parecem mais fortes, sons mais altos, toques mais intensos. Pode ser doloroso.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 24,
                TermoTecnico = "Hipossensibilidade",
                ExplicacaoSimples = "Quando os sentidos são menos sensíveis. A pessoa pode não sentir dor, frio, ou buscar sensações intensas como pular, girar, apertar forte.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 25,
                TermoTecnico = "Sobrecarga Sensorial",
                ExplicacaoSimples = "Quando há estímulos demais ao mesmo tempo (barulho, luz, cheiros, pessoas). Causa muito desconforto e pode levar a meltdown ou shutdown.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 26,
                TermoTecnico = "Dieta Sensorial",
                ExplicacaoSimples = "Programa de atividades sensoriais personalizado para ajudar a pessoa autista a se regular. Inclui coisas como massagens, balanços, música.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 27,
                TermoTecnico = "Propriocepção",
                ExplicacaoSimples = "Sentido que nos diz onde nosso corpo está no espaço. Pessoas autistas podem ter dificuldade com isso, parecendo 'desajeitadas' ou buscando abraços apertados.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 28,
                TermoTecnico = "Sistema Vestibular",
                ExplicacaoSimples = "Sentido do equilíbrio e movimento. Algumas pessoas autistas adoram girar e balançar, outras têm muito medo de movimento.",
                Categoria = "Sensorial",
                DataCriacao = DateTime.UtcNow
            },
            
            // === COMUNICAÇÃO ===
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
                Id = 29,
                TermoTecnico = "CAA",
                ExplicacaoSimples = "Comunicação Aumentativa e Alternativa - formas de comunicação além da fala, como pranchas de imagens, aplicativos, linguagem de sinais. Não impede a fala!",
                Categoria = "Comunicação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 30,
                TermoTecnico = "PECS",
                ExplicacaoSimples = "Sistema de Comunicação por Troca de Figuras. A pessoa usa cartões com imagens para se comunicar. Muito usado com crianças que ainda não falam.",
                Categoria = "Comunicação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 31,
                TermoTecnico = "Não-Verbal",
                ExplicacaoSimples = "Pessoa que não usa a fala para se comunicar. Não significa que não entende ou não tem o que dizer - usa outras formas de comunicação.",
                Categoria = "Comunicação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 32,
                TermoTecnico = "Literalidade",
                ExplicacaoSimples = "Entender as coisas ao pé da letra. Expressões como 'estou morrendo de fome' ou 'chovendo canivetes' podem confundir. Seja claro e direto!",
                Categoria = "Comunicação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 33,
                TermoTecnico = "Dificuldade Pragmática",
                ExplicacaoSimples = "Dificuldade com as 'regras sociais' da comunicação: quando falar, como manter uma conversa, entender sarcasmo, expressões faciais.",
                Categoria = "Comunicação",
                DataCriacao = DateTime.UtcNow
            },
            
            // === EDUCAÇÃO ===
            new GlossaryTerm
            {
                Id = 8,
                TermoTecnico = "PAE/PEI",
                ExplicacaoSimples = "Plano de Atendimento Educacional ou Plano Educacional Individualizado - documento que a escola deve fazer para adaptar o ensino às necessidades do aluno. É um direito!",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 34,
                TermoTecnico = "AEE",
                ExplicacaoSimples = "Atendimento Educacional Especializado - apoio extra que a escola oferece no contraturno. Inclui sala de recursos e profissionais especializados.",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 35,
                TermoTecnico = "Sala de Recursos",
                ExplicacaoSimples = "Espaço na escola com materiais e profissionais especializados para apoiar alunos com deficiência. O aluno frequenta no contraturno.",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 36,
                TermoTecnico = "Profissional de Apoio",
                ExplicacaoSimples = "Pessoa que acompanha o aluno autista na escola para ajudar nas atividades e adaptações. Também chamado de mediador ou acompanhante.",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 37,
                TermoTecnico = "Adaptação Curricular",
                ExplicacaoSimples = "Mudanças no conteúdo, metodologia ou avaliação para que o aluno autista possa aprender. Não é facilitar - é ensinar de forma diferente.",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 38,
                TermoTecnico = "Inclusão Escolar",
                ExplicacaoSimples = "Direito de estudar em escola regular com os devidos apoios. A escola deve se adaptar ao aluno, não o contrário. Garantido pela Lei Berenice Piana.",
                Categoria = "Educação",
                DataCriacao = DateTime.UtcNow
            },
            
            // === TERAPIA ===
            new GlossaryTerm
            {
                Id = 9,
                TermoTecnico = "ABA",
                ExplicacaoSimples = "Análise do Comportamento Aplicada - terapia comportamental para desenvolver habilidades. Deve ser aplicada de forma ética, respeitosa e naturalista.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 39,
                TermoTecnico = "Terapia Ocupacional",
                ExplicacaoSimples = "Trabalha habilidades do dia-a-dia, coordenação motora e integração sensorial. Ajuda a criança a ser mais independente nas atividades.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 40,
                TermoTecnico = "Fonoaudiologia",
                ExplicacaoSimples = "Trabalha fala, linguagem, comunicação e alimentação. Fundamental para desenvolver a comunicação, seja verbal ou alternativa.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 41,
                TermoTecnico = "Integração Sensorial",
                ExplicacaoSimples = "Terapia que ajuda o cérebro a processar melhor os sentidos. Usa atividades como balanços, texturas, massagens para regular os sentidos.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 42,
                TermoTecnico = "TEACCH",
                ExplicacaoSimples = "Método de ensino estruturado para pessoas autistas. Usa apoios visuais, rotinas previsíveis e ambiente organizado.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 43,
                TermoTecnico = "Floortime/DIR",
                ExplicacaoSimples = "Abordagem que segue os interesses da criança brincando no chão. Foca no desenvolvimento emocional e na relação, não em comportamentos.",
                Categoria = "Terapia",
                DataCriacao = DateTime.UtcNow
            },
            
            // === GERAL ===
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
                Id = 44,
                TermoTecnico = "Neurotípico",
                ExplicacaoSimples = "Pessoa cujo cérebro funciona de forma 'típica' ou 'padrão'. Não é melhor nem pior, apenas diferente do neurodivergente.",
                Categoria = "Geral",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 45,
                TermoTecnico = "Neurodiversidade",
                ExplicacaoSimples = "Ideia de que diferenças neurológicas (autismo, TDAH, etc.) são variações naturais do cérebro humano, não doenças a serem curadas.",
                Categoria = "Geral",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 46,
                TermoTecnico = "Capacitismo",
                ExplicacaoSimples = "Preconceito contra pessoas com deficiência. Inclui baixas expectativas, exclusão, falar 'você nem parece autista' como elogio.",
                Categoria = "Geral",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 47,
                TermoTecnico = "Pessoa com Autismo vs Pessoa Autista",
                ExplicacaoSimples = "Algumas pessoas preferem 'pessoa com autismo' (pessoa em primeiro lugar), outras preferem 'pessoa autista' (identidade). Pergunte como a pessoa prefere!",
                Categoria = "Geral",
                DataCriacao = DateTime.UtcNow
            },
            
            // === ALIMENTAÇÃO ===
            new GlossaryTerm
            {
                Id = 12,
                TermoTecnico = "Seletividade Alimentar",
                ExplicacaoSimples = "Quando a pessoa aceita poucos alimentos. Está relacionada à sensibilidade sensorial (textura, cor, cheiro). Não é frescura ou falta de educação.",
                Categoria = "Alimentação",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 48,
                TermoTecnico = "ARFID",
                ExplicacaoSimples = "Transtorno Alimentar Restritivo Evitativo - quando a seletividade alimentar é tão intensa que afeta a saúde ou crescimento. Precisa de acompanhamento.",
                Categoria = "Alimentação",
                DataCriacao = DateTime.UtcNow
            },
            
            // === DIREITOS ===
            new GlossaryTerm
            {
                Id = 49,
                TermoTecnico = "Lei Berenice Piana",
                ExplicacaoSimples = "Lei 12.764/2012 que garante direitos às pessoas autistas no Brasil: diagnóstico precoce, tratamento, educação inclusiva, trabalho.",
                Categoria = "Direitos",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 50,
                TermoTecnico = "Lei Romeo Mion",
                ExplicacaoSimples = "Lei 13.977/2020 que criou a CIPTEA - carteira de identificação para pessoa autista - facilitando atendimento prioritário.",
                Categoria = "Direitos",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 51,
                TermoTecnico = "CIPTEA",
                ExplicacaoSimples = "Carteira de Identificação da Pessoa com Transtorno do Espectro Autista. Documento que facilita acesso a direitos e atendimento prioritário.",
                Categoria = "Direitos",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 52,
                TermoTecnico = "BPC/LOAS",
                ExplicacaoSimples = "Benefício de Prestação Continuada - um salário mínimo mensal para pessoas com deficiência de baixa renda. Não precisa ter contribuído ao INSS.",
                Categoria = "Direitos",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 53,
                TermoTecnico = "Curatela",
                ExplicacaoSimples = "Quando um juiz determina que outra pessoa (curador) tome decisões pela pessoa autista. Só em casos extremos de incapacidade civil.",
                Categoria = "Direitos",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 54,
                TermoTecnico = "Tomada de Decisão Apoiada",
                ExplicacaoSimples = "Alternativa à curatela onde a pessoa autista escolhe apoiadores para ajudar em decisões específicas, mantendo sua autonomia.",
                Categoria = "Direitos",
                DataCriacao = DateTime.UtcNow
            },
            
            // === NOVOS TERMOS DO MINI DICIONÁRIO AUTISTIMA ===
            // Aprendizagem
            new GlossaryTerm
            {
                Id = 55,
                TermoTecnico = "Disgrafia",
                ExplicacaoSimples = "Dificuldade na escrita manual, com traçado irregular, cansaço e lentidão. Não é letra feia - é disgrafia.",
                ExemploUso = "Não é letra feia, é disgrafia.",
                Categoria = "Aprendizagem",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 56,
                TermoTecnico = "Discalculia",
                ExplicacaoSimples = "Transtorno específico da aprendizagem matemática, afetando a compreensão de números e operações. Não é burrice - é discalculia.",
                ExemploUso = "Não é burrice, é discalculia.",
                Categoria = "Aprendizagem",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 57,
                TermoTecnico = "Dislexia",
                ExplicacaoSimples = "Dificuldade na leitura, decodificação de palavras e compreensão escrita. Não é falta de atenção - pode ser dislexia.",
                ExemploUso = "Não é falta de atenção, pode ser dislexia.",
                Categoria = "Aprendizagem",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 58,
                TermoTecnico = "Perfil de Aprendizagem Atípico",
                ExplicacaoSimples = "Desempenho irregular entre diferentes áreas de conhecimento. A criança pode ser excelente em uma matéria e ter muita dificuldade em outra.",
                ExemploUso = "Não é atraso escolar, é perfil de aprendizagem atípico.",
                Categoria = "Aprendizagem",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Processamento
            new GlossaryTerm
            {
                Id = 59,
                TermoTecnico = "Déficit Visoespacial",
                ExplicacaoSimples = "Dificuldade em perceber relações espaciais e organizar informações visuais. Não é desorganização - é déficit visoespacial.",
                ExemploUso = "Não é desorganização, é déficit visoespacial.",
                Categoria = "Processamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 60,
                TermoTecnico = "Déficit em Funções Executivas",
                ExplicacaoSimples = "Dificuldade de planejamento, organização, foco e controle emocional. Afeta a capacidade de iniciar e completar tarefas.",
                ExemploUso = "Não é dificuldade isolada, é déficit em funções executivas.",
                Categoria = "Processamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Comportamento (novos)
            new GlossaryTerm
            {
                Id = 61,
                TermoTecnico = "Desregulação Emocional",
                ExplicacaoSimples = "Dificuldade de regular emoções diante de frustrações e estímulos. Não é birra - é desregulação emocional.",
                ExemploUso = "Não é birra, é desregulação emocional.",
                Categoria = "Comportamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 62,
                TermoTecnico = "Rigidez Cognitivo-Comportamental",
                ExplicacaoSimples = "Dificuldade em lidar com mudanças de rotina ou padrões estabelecidos. Não é teimosia - é rigidez cognitivo-comportamental.",
                ExemploUso = "Não é teimosia, é rigidez cognitivo-comportamental.",
                Categoria = "Comportamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 63,
                TermoTecnico = "Interesse Restrito",
                ExplicacaoSimples = "Interesses muito intensos e específicos por determinados temas. Pode ser uma grande força quando bem direcionado.",
                ExemploUso = "Não é fixação, é interesse restrito.",
                Categoria = "Comportamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 64,
                TermoTecnico = "Necessidade de Previsibilidade",
                ExplicacaoSimples = "Apego intenso a rotinas e necessidade de saber o que vai acontecer. Mudanças inesperadas causam grande ansiedade.",
                ExemploUso = "Não é frescura, é necessidade de previsibilidade.",
                Categoria = "Comportamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Socialização (novos)
            new GlossaryTerm
            {
                Id = 65,
                TermoTecnico = "Interação Social Atípica",
                ExplicacaoSimples = "Preferência por interação previsível ou por brincar sozinho. Não é isolamento - é uma forma diferente de interagir.",
                ExemploUso = "Não é isolamento, é interação social atípica.",
                Categoria = "Socialização",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 66,
                TermoTecnico = "Dificuldade de Habilidades Sociais",
                ExplicacaoSimples = "Desafio em compreender regras sociais implícitas e expressões emocionais. Não é falta de educação - é dificuldade de habilidades sociais.",
                ExemploUso = "Não é falta de educação, é dificuldade de habilidades sociais.",
                Categoria = "Socialização",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 67,
                TermoTecnico = "Dificuldade de Contato Visual",
                ExplicacaoSimples = "Padrão atípico de atenção visual. Olhar nos olhos pode ser desconfortável ou difícil para pessoas autistas.",
                ExemploUso = "Não é indiferença, é dificuldade de contato visual.",
                Categoria = "Socialização",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 68,
                TermoTecnico = "Expressão Afetiva Atípica",
                ExplicacaoSimples = "Formas diferentes de demonstrar carinho e afeto. A pessoa pode amar muito, mas expressar de maneira diferente.",
                ExemploUso = "Não é falta de afeto, é expressão afetiva atípica.",
                Categoria = "Socialização",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Sensorial (novos)
            new GlossaryTerm
            {
                Id = 69,
                TermoTecnico = "Defesa Sensorial Alimentar",
                ExplicacaoSimples = "Restrição alimentar causada por sensibilidade a textura, cheiro, cor ou consistência dos alimentos.",
                ExemploUso = "Não é frescura, é defesa sensorial alimentar.",
                Categoria = "Sensorial",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 70,
                TermoTecnico = "Busca Sensorial",
                ExplicacaoSimples = "Necessidade intensa de estímulos físicos como pular, girar, apertar. O corpo precisa dessa entrada sensorial.",
                ExemploUso = "Não é descontrole, é busca sensorial.",
                Categoria = "Sensorial",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Comunicação (novos)
            new GlossaryTerm
            {
                Id = 71,
                TermoTecnico = "Apraxia da Fala",
                ExplicacaoSimples = "Dificuldade na programação motora da fala - o cérebro tem dificuldade em coordenar os movimentos para produzir sons.",
                ExemploUso = "Não é timidez, é apraxia da fala.",
                Categoria = "Comunicação",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 72,
                TermoTecnico = "Transtorno do Desenvolvimento da Linguagem",
                ExplicacaoSimples = "Dificuldades persistentes na compreensão e expressão verbal que não são explicadas por outras condições.",
                ExemploUso = "Não é atraso simples, pode ser transtorno do desenvolvimento da linguagem.",
                Categoria = "Comunicação",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Motor
            new GlossaryTerm
            {
                Id = 73,
                TermoTecnico = "Dispraxia",
                ExplicacaoSimples = "Dificuldades de planejamento e execução de movimentos coordenados. Afeta tarefas como escrever, amarrar sapato, andar de bicicleta.",
                ExemploUso = "Não é preguiça, pode ser dispraxia.",
                Categoria = "Motor",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 74,
                TermoTecnico = "Fadiga Motora Fina",
                ExplicacaoSimples = "Cansaço rápido em tarefas que exigem movimentos precisos das mãos, como escrever ou recortar.",
                ExemploUso = "Não é preguiça, é fadiga motora fina.",
                Categoria = "Motor",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            new GlossaryTerm
            {
                Id = 75,
                TermoTecnico = "Alteração Motora Global",
                ExplicacaoSimples = "Dificuldades em movimentos amplos como correr, pular, subir escadas. Pode parecer desajeitado ou descoordenado.",
                ExemploUso = "Não é falta de jeito, pode ser alteração motora global.",
                Categoria = "Motor",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Diagnóstico (novos)
            new GlossaryTerm
            {
                Id = 76,
                TermoTecnico = "Dupla Excepcionalidade",
                ExplicacaoSimples = "Quando coexistem superdotação/altas habilidades e um transtorno como autismo. A pessoa pode ter talentos extraordinários e desafios significativos.",
                ExemploUso = "Não é contradição, é dupla excepcionalidade.",
                Categoria = "Diagnóstico",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Neurodivergência
            new GlossaryTerm
            {
                Id = 77,
                TermoTecnico = "TDAH",
                ExplicacaoSimples = "Transtorno do Déficit de Atenção e Hiperatividade - condição que afeta atenção, controle de impulsos e nível de atividade. Frequentemente coexiste com autismo.",
                ExemploUso = "Não é distração, pode ser TDAH.",
                Categoria = "Neurodivergência",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            },
            
            // Tratamento
            new GlossaryTerm
            {
                Id = 78,
                TermoTecnico = "Intervenção Precoce",
                ExplicacaoSimples = "Estimulação e terapias iniciadas nos primeiros anos de vida. Quanto mais cedo começar, melhores os resultados do desenvolvimento.",
                ExemploUso = "Não é esperar crescer - intervenção precoce é fundamental.",
                Categoria = "Tratamento",
                Fonte = "Mini Dicionário AUTistima",
                DataCriacao = DateTime.UtcNow
            }
        );
    }
    
    private void SeedServicesCapsMaceio(ModelBuilder builder)
    {
        builder.Entity<Service>().HasData(
            // CAPS II - Centro
            new Service
            {
                Id = 1,
                NomeProfissional = "CAPS II - Centro (Dr. Everaldo Moreira)",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "Centro de Atenção Psicossocial para atendimento de adultos com transtornos mentais graves e persistentes. Oferece acolhimento, atendimento individual e em grupo, oficinas terapêuticas e acompanhamento familiar.",
                Endereco = "Rua Comendador Palmeira, 270",
                Bairro = "Centro",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57020-090",
                Telefone = "(82) 3315-5590",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Atendimento de segunda a sexta, das 8h às 17h. Funcionamento em regime de porta aberta.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // CAPS II - Jacintinho
            new Service
            {
                Id = 2,
                NomeProfissional = "CAPS II - Jacintinho",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "Centro de Atenção Psicossocial para atendimento de adultos com transtornos mentais graves. Equipe multidisciplinar com psicólogos, psiquiatras, assistentes sociais e terapeutas ocupacionais.",
                Endereco = "Rua Conselheiro Lourenço de Albuquerque, s/n",
                Bairro = "Jacintinho",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57041-400",
                Telefone = "(82) 3315-5591",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Atendimento de segunda a sexta, das 8h às 17h.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // CAPS II - Benedito Bentes
            new Service
            {
                Id = 3,
                NomeProfissional = "CAPS II - Benedito Bentes",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "Centro de Atenção Psicossocial para atendimento de adultos com transtornos mentais. Oferece atendimento individual, em grupo, oficinas terapêuticas e visitas domiciliares.",
                Endereco = "Conjunto Denisson Menezes, s/n",
                Bairro = "Benedito Bentes",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57084-000",
                Telefone = "(82) 3315-5592",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Atendimento de segunda a sexta, das 8h às 17h.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // CAPSi - Infanto-Juvenil
            new Service
            {
                Id = 4,
                NomeProfissional = "CAPSi - Centro de Atenção Psicossocial Infanto-Juvenil",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "CAPS especializado no atendimento de crianças e adolescentes com transtornos mentais graves, incluindo autismo (TEA). Equipe especializada em saúde mental infantojuvenil com psicólogos, fonoaudiólogos, terapeutas ocupacionais e psiquiatras.",
                Endereco = "Av. Fernandes Lima, 1681",
                Bairro = "Farol",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57055-000",
                Telefone = "(82) 3315-5593",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Atendimento especializado para crianças e adolescentes de 0 a 18 anos. Referência em TEA. Segunda a sexta, das 8h às 17h.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // CAPS AD III - Álcool e Drogas
            new Service
            {
                Id = 5,
                NomeProfissional = "CAPS AD III - Centro de Atenção Psicossocial Álcool e Drogas",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "CAPS especializado em tratamento de pessoas com transtornos relacionados ao uso de álcool e outras drogas. Funciona 24 horas com leitos de acolhimento noturno.",
                Endereco = "Av. Siqueira Campos, 1655",
                Bairro = "Trapiche da Barra",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57010-005",
                Telefone = "(82) 3315-5594",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Funcionamento 24 horas, incluindo finais de semana e feriados. Possui leitos de acolhimento.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // CAPS III - Tabuleiro do Martins
            new Service
            {
                Id = 6,
                NomeProfissional = "CAPS III - Tabuleiro do Martins",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "Centro de Atenção Psicossocial com funcionamento 24 horas para adultos com transtornos mentais graves. Possui leitos de acolhimento noturno e atendimento de urgência psiquiátrica.",
                Endereco = "Rua México, s/n",
                Bairro = "Tabuleiro do Martins",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57081-000",
                Telefone = "(82) 3315-5595",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Funcionamento 24 horas. Porta de entrada para urgências psiquiátricas.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // Ambulatório de Saúde Mental - Especialidades em TEA
            new Service
            {
                Id = 7,
                NomeProfissional = "Ambulatório de Saúde Mental - PAM Salgadinho",
                Especialidade = Especialidade.Neurologia,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "Ambulatório de especialidades com atendimento em neurologia, psiquiatria e psicologia. Realiza avaliação diagnóstica para TEA e acompanhamento de pessoas autistas.",
                Endereco = "Av. Major Cícero de Góes Monteiro, 1655",
                Bairro = "Poço",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57025-000",
                Telefone = "(82) 3315-5500",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (SUS)",
                Observacoes = "Necessário encaminhamento da UBS. Atendimento mediante agendamento.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // APAE Maceió
            new Service
            {
                Id = 8,
                NomeProfissional = "APAE Maceió - Associação de Pais e Amigos dos Excepcionais",
                Especialidade = Especialidade.TerapiaOcupacional,
                TipoAtendimento = TipoAtendimento.Gratuito,
                Descricao = "Instituição filantrópica que oferece atendimento multidisciplinar gratuito para pessoas com deficiência intelectual e autismo. Serviços incluem: psicologia, fonoaudiologia, terapia ocupacional, fisioterapia, pedagogia e serviço social.",
                Endereco = "Rua José de Alencar, 340",
                Bairro = "Farol",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57055-170",
                Telefone = "(82) 3221-3344",
                Email = "apae.maceio@gmail.com",
                AtendeOnline = false,
                ValorConsulta = "Gratuito",
                Observacoes = "Atendimento gratuito pelo SUS. Necessário cadastro e avaliação inicial. Segunda a sexta, das 7h às 17h.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // AMA - Associação dos Amigos do Autista
            new Service
            {
                Id = 9,
                NomeProfissional = "AMA Alagoas - Associação dos Amigos do Autista",
                Especialidade = Especialidade.ABA,
                TipoAtendimento = TipoAtendimento.ValorSocial,
                Descricao = "Associação especializada no atendimento de pessoas autistas e suas famílias. Oferece terapia ABA, fonoaudiologia, terapia ocupacional, psicopedagogia e grupos de apoio para famílias.",
                Endereco = "Rua Melo Moraes, 99",
                Bairro = "Pitanguinha",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57052-280",
                Telefone = "(82) 3223-4567",
                Email = "ama.alagoas@gmail.com",
                AtendeOnline = false,
                ValorConsulta = "Valor social (consultar)",
                Observacoes = "Associação sem fins lucrativos. Oferece atendimento por valor social. Grupos de apoio para mães.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // Clínica Escola UNCISAL
            new Service
            {
                Id = 10,
                NomeProfissional = "Clínica Escola UNCISAL - Fonoaudiologia",
                Especialidade = Especialidade.Fonoaudiologia,
                TipoAtendimento = TipoAtendimento.ConvenioUniversitario,
                Descricao = "Clínica escola da Universidade Estadual de Ciências da Saúde de Alagoas. Oferece atendimento fonoaudiológico gratuito para avaliação e terapia de linguagem, fala e comunicação alternativa.",
                Endereco = "Rua Dr. Jorge de Lima, 113",
                Bairro = "Trapiche da Barra",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57010-300",
                Telefone = "(82) 3315-6700",
                Website = "https://www.uncisal.edu.br",
                AtendeOnline = false,
                ValorConsulta = "Gratuito (clínica escola)",
                Observacoes = "Atendimento gratuito por estudantes supervisionados. Necessário agendamento. Fila de espera pode ser longa.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // Centro Universitário CESMAC - Psicologia
            new Service
            {
                Id = 11,
                NomeProfissional = "Clínica Escola CESMAC - Psicologia",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.ConvenioUniversitario,
                Descricao = "Clínica escola do CESMAC com atendimento psicológico por estudantes supervisionados. Oferece avaliação psicológica, psicoterapia individual e familiar, e grupos terapêuticos.",
                Endereco = "Rua Cônego Machado, 918",
                Bairro = "Farol",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57051-160",
                Telefone = "(82) 3215-5000",
                Website = "https://www.cesmac.edu.br",
                AtendeOnline = false,
                ValorConsulta = "Gratuito ou valor social",
                Observacoes = "Atendimento por estudantes de psicologia supervisionados. Valor social para comunidade.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            },
            // UFAL - Clínica de Psicologia
            new Service
            {
                Id = 12,
                NomeProfissional = "Clínica de Psicologia UFAL",
                Especialidade = Especialidade.Psicologia,
                TipoAtendimento = TipoAtendimento.ConvenioUniversitario,
                Descricao = "Serviço de Psicologia Aplicada da Universidade Federal de Alagoas. Oferece atendimento psicológico gratuito à comunidade, incluindo avaliação e acompanhamento de crianças autistas.",
                Endereco = "Campus A.C. Simões, Av. Lourival Melo Mota, s/n",
                Bairro = "Cidade Universitária",
                Cidade = "Maceió",
                Estado = "AL",
                CEP = "57072-970",
                Telefone = "(82) 3214-1100",
                Website = "https://www.ufal.edu.br",
                AtendeOnline = false,
                ValorConsulta = "Gratuito",
                Observacoes = "Serviço gratuito da UFAL. Necessário inscrição e triagem. Atendimento por estudantes supervisionados por professores.",
                Verificado = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            }
        );
    }
    
    private void SeedManejosIniciais(ModelBuilder builder)
    {
        // ID do usuário admin padrão - será criado automaticamente no Program.cs
        // Os manejos serão associados à Lorena (admin) como autora inicial
        // UserId será definido após a criação do banco via script SQL ou manualmente
        
        // NOTA: Como Manejo requer UserId (que é gerado dinamicamente),
        // este seed será aplicado via script SQL após a criação do admin.
        // Aqui deixamos a estrutura preparada para referência.
        
        /*
        Os 29 manejos do documento "Manejos Iniciais.docx" são:
        
        CRISE SENSORIAL (5):
        1. Cantinho do Silêncio - criar espaço com almofadas, luz baixa, abafadores
        2. Kit de Emergência Sensorial - bolsa com objetos sensoriais favoritos
        3. Técnica da Pressão Profunda - usar cobertor pesado ou abraço firme
        4. Rotina de Descompressão - 15min de transição após atividades intensas
        5. Mapa Sensorial do Ambiente - identificar gatilhos em cada cômodo
        
        ANSIEDADE (3):
        6. Antecipação Visual - mostrar fotos/vídeos de lugares novos
        7. Objeto de Transição - item favorito como âncora emocional
        8. Respiração com Bolhas - soprar bolhas de sabão para acalmar
        
        BANHO (3):
        9. Banho Gradual - começar pelos pés, termômetro visual de temperatura
        10. Hora do Banho Musical - playlist que indica duração
        11. Produtos Sensoriais - experimentar texturas e cheiros diferentes
        
        ALIMENTAÇÃO (3):
        12. Prato da Descoberta - pequenas porções separadas, sem pressão
        13. Participação na Cozinha - ajudar a preparar aumenta aceitação
        14. Mapa de Texturas - registro visual de aceitação de texturas
        
        SONO (3):
        15. Ritual do Sono - sequência fixa de atividades antes de dormir
        16. Ambiente Sensorial Noturno - luz, temperatura, som adequados
        17. História Personalizada - roteiro previsível com personagem favorito
        
        COMUNICAÇÃO (4):
        18. Prancha de Emoções - imagens de sentimentos para apontar
        19. Timer Visual - ampulheta ou app para espera e transições
        20. Roteiro Social - script para situações específicas
        21. Comunicação Alternativa - PECS, pranchas, aplicativos
        
        ROTINA (2):
        22. Quadro de Rotina Visual - agenda com imagens do dia
        23. Aviso de Mudança - preparar com antecedência alterações
        
        ESCOLA (3):
        24. Pasta de Comunicação - caderno família-escola
        25. Combinados Visuais - regras em imagens
        26. Cantinho da Calma na Escola - espaço de autorregulação
        
        SOCIALIZAÇÃO (2):
        27. Play Date Estruturado - brincadeiras com roteiro
        28. Modelagem Social - ensaiar situações com bonecos
        
        AUTOCUIDADO (3):
        29. Escovação Sensorial - escovas macias, sabores preferidos
        */
    }
}
