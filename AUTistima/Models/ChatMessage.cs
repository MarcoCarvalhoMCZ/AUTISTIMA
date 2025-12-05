using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Mensagens de chat entre usuários
/// </summary>
public class ChatMessage
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string RemetenteId { get; set; } = string.Empty;
    
    [Required]
    public string DestinatarioId { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Mensagem")]
    [DataType(DataType.MultilineText)]
    public string Conteudo { get; set; } = string.Empty;
    
    [Display(Name = "Lida")]
    public bool Lida { get; set; } = false;
    
    [Display(Name = "Data de Leitura")]
    public DateTime? DataLeitura { get; set; }
    
    public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
    
    public bool Ativo { get; set; } = true;
    
    // Navegações
    [ForeignKey("RemetenteId")]
    public virtual ApplicationUser? Remetente { get; set; }
    
    [ForeignKey("DestinatarioId")]
    public virtual ApplicationUser? Destinatario { get; set; }
}

/// <summary>
/// Conversa entre dois usuários (agrupa mensagens)
/// </summary>
public class Conversation
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Usuario1Id { get; set; } = string.Empty;
    
    [Required]
    public string Usuario2Id { get; set; } = string.Empty;
    
    public DateTime UltimaMensagem { get; set; } = DateTime.UtcNow;
    
    public bool Ativo { get; set; } = true;
    
    // Navegações
    [ForeignKey("Usuario1Id")]
    public virtual ApplicationUser? Usuario1 { get; set; }
    
    [ForeignKey("Usuario2Id")]
    public virtual ApplicationUser? Usuario2 { get; set; }
}
