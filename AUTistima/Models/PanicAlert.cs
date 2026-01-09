using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Alerta de Pânico - Registra demandas críticas de mães em situação de risco
/// Usado para conectar rapidamente com profissionais via WhatsApp
/// </summary>
public class PanicAlert
{
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Usuário (mãe) que acionou o alerta
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Usuario { get; set; }
    
    /// <summary>
    /// Motivo/descrição do pânico
    /// </summary>
    [Required]
    [StringLength(500)]
    [Display(Name = "Descrição da Situação")]
    [DataType(DataType.MultilineText)]
    public string Descricao { get; set; } = string.Empty;
    
    /// <summary>
    /// Nível de urgência
    /// </summary>
    [Display(Name = "Nível de Urgência")]
    public NivelUrgenciaPanico NivelUrgencia { get; set; } = NivelUrgenciaPanico.Critico;
    
    /// <summary>
    /// URL da conversa WhatsApp gerada
    /// </summary>
    [StringLength(500)]
    [Url]
    [Display(Name = "Link WhatsApp")]
    public string? LinkWhatsApp { get; set; }
    
    /// <summary>
    /// Status do alerta
    /// </summary>
    [Display(Name = "Status")]
    public StatusAlertaPanico Status { get; set; } = StatusAlertaPanico.Ativo;
    
    /// <summary>
    /// Indica se o usuário confirmou e foi redirecionado
    /// </summary>
    [Display(Name = "Confirmado")]
    public bool Confirmado { get; set; } = false;
    
    /// <summary>
    /// Data/hora da confirmação
    /// </summary>
    [Display(Name = "Data da Confirmação")]
    public DateTime? DataConfirmacao { get; set; }
    
    /// <summary>
    /// Data/hora de criação do alerta
    /// </summary>
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Nota do profissional que atendeu
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Nota de Atendimento")]
    public string? NotaAtendimento { get; set; }
    
    /// <summary>
    /// Data/hora do atendimento
    /// </summary>
    [Display(Name = "Data do Atendimento")]
    public DateTime? DataAtendimento { get; set; }
    
    /// <summary>
    /// Ativo ou arquivado
    /// </summary>
    [Display(Name = "Ativo")]
    public bool Ativo { get; set; } = true;
}

/// <summary>
/// Nível de urgência do alerta de pânico
/// </summary>
public enum NivelUrgenciaPanico
{
    [Display(Name = "Nível 1 - Normal")]
    Normal = 1,
    
    [Display(Name = "Nível 2 - Moderado")]
    Moderado = 2,
    
    [Display(Name = "Nível 3 - Crítico")]
    Critico = 3,
    
    [Display(Name = "Nível 4 - Emergência")]
    Emergencia = 4
}

/// <summary>
/// Status do alerta de pânico
/// </summary>
public enum StatusAlertaPanico
{
    [Display(Name = "Ativo")]
    Ativo = 0,
    
    [Display(Name = "Atendido")]
    Atendido = 1,
    
    [Display(Name = "Resolvido")]
    Resolvido = 2,
    
    [Display(Name = "Escalado")]
    Escalado = 3,
    
    [Display(Name = "Arquivado")]
    Arquivado = 4
}
