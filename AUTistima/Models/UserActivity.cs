using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AUTistima.Models.Enums;

namespace AUTistima.Models;

/// <summary>
/// Registro de atividades dos usuários para métricas de engajamento
/// </summary>
public class UserActivity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Usuario { get; set; }
    
    [Required]
    [Display(Name = "Tipo de Atividade")]
    public TipoAtividade TipoAtividade { get; set; }
    
    [StringLength(100)]
    [Display(Name = "Entidade Relacionada")]
    public string? EntidadeRelacionada { get; set; }
    
    public int? EntidadeId { get; set; }
    
    [StringLength(500)]
    [Display(Name = "Detalhes")]
    public string? Detalhes { get; set; }
    
    [StringLength(45)]
    [Display(Name = "Endereço IP")]
    public string? EnderecoIP { get; set; }
    
    [StringLength(500)]
    [Display(Name = "User Agent")]
    public string? UserAgent { get; set; }
    
    [Display(Name = "Dispositivo")]
    public TipoDispositivo Dispositivo { get; set; } = TipoDispositivo.Web;
    
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "Sessão")]
    [StringLength(100)]
    public string? SessaoId { get; set; }
    
    [Display(Name = "Tempo na Página (segundos)")]
    public int? TempoNaPagina { get; set; }
}

/// <summary>
/// Snapshot diário de estatísticas para gráficos temporais
/// </summary>
public class StatisticSnapshot
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Data")]
    public DateOnly Data { get; set; }
    
    // Usuários
    [Display(Name = "Total Usuários")]
    public int TotalUsuarios { get; set; }
    
    [Display(Name = "Novos Usuários (dia)")]
    public int NovosUsuarios { get; set; }
    
    [Display(Name = "Usuários Ativos (dia)")]
    public int UsuariosAtivos { get; set; }
    
    // Por perfil
    [Display(Name = "Total Mães")]
    public int TotalMaes { get; set; }
    
    [Display(Name = "Total Prof. Saúde")]
    public int TotalProfissionaisSaude { get; set; }
    
    [Display(Name = "Total Prof. Educação")]
    public int TotalProfissionaisEducacao { get; set; }
    
    [Display(Name = "Total Empresas")]
    public int TotalEmpresas { get; set; }
    
    [Display(Name = "Total Governo")]
    public int TotalGoverno { get; set; }
    
    // Engajamento
    [Display(Name = "Posts Criados")]
    public int PostsCriados { get; set; }
    
    [Display(Name = "Comentários")]
    public int Comentarios { get; set; }
    
    [Display(Name = "Acolhimentos")]
    public int Acolhimentos { get; set; }
    
    [Display(Name = "Manejos Compartilhados")]
    public int ManejosCompartilhados { get; set; }
    
    [Display(Name = "Manejos Validados")]
    public int ManejosValidados { get; set; }
    
    // Triagens
    [Display(Name = "Triagens Solicitadas")]
    public int TriagensSolicitadas { get; set; }
    
    [Display(Name = "Triagens Concluídas")]
    public int TriagensConcluidas { get; set; }
    
    // Oportunidades
    [Display(Name = "Oportunidades Publicadas")]
    public int OportunidadesPublicadas { get; set; }
    
    // Interações
    [Display(Name = "Total Logins")]
    public int TotalLogins { get; set; }
    
    [Display(Name = "Visualizações de Página")]
    public int VisualizacoesPagina { get; set; }
    
    [Display(Name = "Buscas Realizadas")]
    public int BuscasRealizadas { get; set; }
    
    [Display(Name = "Mensagens Chat")]
    public int MensagensChat { get; set; }
    
    // Métricas calculadas
    [Display(Name = "Taxa Engajamento (%)")]
    [Column(TypeName = "decimal(5,2)")]
    public decimal TaxaEngajamento { get; set; }
    
    [Display(Name = "Média Acolhimentos/Post")]
    [Column(TypeName = "decimal(5,2)")]
    public decimal MediaAcolhimentosPorPost { get; set; }
    
    public DateTime DataGeracao { get; set; } = DateTime.UtcNow;
}
