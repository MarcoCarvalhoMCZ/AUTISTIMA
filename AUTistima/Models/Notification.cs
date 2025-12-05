using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Notificações do sistema para os usuários
/// </summary>
public class Notification
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    [Display(Name = "Mensagem")]
    public string Mensagem { get; set; } = string.Empty;
    
    [Display(Name = "Tipo")]
    public TipoNotificacao Tipo { get; set; } = TipoNotificacao.Sistema;
    
    [StringLength(255)]
    [Display(Name = "Link")]
    public string? Link { get; set; }
    
    [Display(Name = "Lida")]
    public bool Lida { get; set; } = false;
    
    [Display(Name = "Data de Leitura")]
    public DateTime? DataLeitura { get; set; }
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    // Navegação
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Usuario { get; set; }
}

/// <summary>
/// Tipos de notificação do sistema
/// </summary>
public enum TipoNotificacao
{
    [Display(Name = "Sistema")]
    Sistema = 0,
    
    [Display(Name = "Acolhimento")]
    Acolhimento = 1,
    
    [Display(Name = "Comentário")]
    Comentario = 2,
    
    [Display(Name = "Mensagem")]
    Mensagem = 3,
    
    [Display(Name = "Manejo Validado")]
    ManejoValidado = 4,
    
    [Display(Name = "Nova Oportunidade")]
    NovaOportunidade = 5,
    
    [Display(Name = "Lembrete")]
    Lembrete = 6,
    
    [Display(Name = "Boas-vindas")]
    BoasVindas = 7
}
