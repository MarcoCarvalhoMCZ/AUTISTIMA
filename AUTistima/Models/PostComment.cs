using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Comentário em um Post - mensagens de suporte e acolhimento
/// </summary>
public class PostComment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int PostId { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Comentário")]
    [DataType(DataType.MultilineText)]
    [StringLength(1000)]
    public string Conteudo { get; set; } = string.Empty;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    public bool Ativo { get; set; } = true;
    
    [ForeignKey("PostId")]
    public virtual Post? Post { get; set; }
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Autor { get; set; }
}
