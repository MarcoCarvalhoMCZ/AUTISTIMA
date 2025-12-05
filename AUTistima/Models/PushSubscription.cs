using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Armazena as subscriptions de Push Notification dos usuários
/// Permite enviar notificações push mesmo quando o app está fechado
/// </summary>
public class PushSubscription
{
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Usuário dono desta subscription
    /// </summary>
    [Required]
    [StringLength(450)]
    public string UserId { get; set; } = string.Empty;
    
    /// <summary>
    /// Endpoint do Push Service (URL único para este dispositivo)
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Endpoint { get; set; } = string.Empty;
    
    /// <summary>
    /// Chave P256DH para criptografia
    /// </summary>
    [Required]
    [StringLength(100)]
    public string P256dh { get; set; } = string.Empty;
    
    /// <summary>
    /// Chave de autenticação
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Auth { get; set; } = string.Empty;
    
    /// <summary>
    /// User Agent do dispositivo (para identificação)
    /// </summary>
    [StringLength(500)]
    public string? UserAgent { get; set; }
    
    /// <summary>
    /// Data de criação da subscription
    /// </summary>
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Última vez que uma notificação foi enviada com sucesso
    /// </summary>
    public DateTime? UltimoEnvio { get; set; }
    
    /// <summary>
    /// Se a subscription está ativa (false se expirou ou foi removida)
    /// </summary>
    public bool Ativo { get; set; } = true;
    
    // Navegação
    [ForeignKey("UserId")]
    public virtual ApplicationUser? User { get; set; }
}
