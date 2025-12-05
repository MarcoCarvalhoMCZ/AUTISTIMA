using System.ComponentModel.DataAnnotations;

namespace AUTistima.Models;

/// <summary>
/// Termo do Mini Dicionário - glossário de termos técnicos explicados de forma simples
/// </summary>
public class GlossaryTerm
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Termo Técnico")]
    public string TermoTecnico { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Explicação Simples")]
    [DataType(DataType.MultilineText)]
    public string ExplicacaoSimples { get; set; } = string.Empty;
    
    [StringLength(100)]
    [Display(Name = "Categoria")]
    public string? Categoria { get; set; }
    
    [Display(Name = "Exemplo de Uso")]
    [DataType(DataType.MultilineText)]
    public string? ExemploUso { get; set; }
    
    [StringLength(500)]
    [Display(Name = "Fonte/Referência")]
    public string? Fonte { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
