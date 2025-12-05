using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Acolhimento em um Post - substitui o "Like" por um abraço/coração
/// </summary>
public class PostAcolhimento
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int PostId { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    public DateTime DataAcolhimento { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("PostId")]
    public virtual Post? Post { get; set; }
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Usuario { get; set; }
}
