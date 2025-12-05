using AUTistima.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Manejos - Estratégias e dicas práticas compartilhadas pelas mães
/// "Saberes não cientificizados" - conhecimento de vivência
/// </summary>
public class Manejo
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Categoria")]
    public CategoriaManejo Categoria { get; set; }
    
    [Required]
    [Display(Name = "Descrição")]
    [DataType(DataType.MultilineText)]
    public string Descricao { get; set; } = string.Empty;
    
    [Display(Name = "Dica Prática")]
    [DataType(DataType.MultilineText)]
    public string? DicaPratica { get; set; }
    
    [Display(Name = "Faixa Etária Indicada")]
    [StringLength(50)]
    public string? FaixaEtariaIndicada { get; set; }
    
    [Display(Name = "Nível de Suporte Indicado")]
    public NivelSuporte? NivelSuporteIndicado { get; set; }
    
    [Display(Name = "Validado por Especialista")]
    public bool ValidadoPorEspecialista { get; set; } = false;
    
    [Display(Name = "Especialista que Validou")]
    public string? EspecialistaValidadorId { get; set; }
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    public DateTime? DataAtualizacao { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    // Autor do manejo
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Autor { get; set; }
    
    [ForeignKey("EspecialistaValidadorId")]
    public virtual ApplicationUser? EspecialistaValidador { get; set; }
}
