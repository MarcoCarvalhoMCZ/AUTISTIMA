using AUTistima.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Oportunidade - Vagas de emprego e serviços oferecidos
/// </summary>
public class Opportunity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Tipo")]
    public TipoOportunidade Tipo { get; set; }
    
    [Required]
    [Display(Name = "Descrição")]
    [DataType(DataType.MultilineText)]
    public string Descricao { get; set; } = string.Empty;
    
    [Display(Name = "Requisitos")]
    [DataType(DataType.MultilineText)]
    public string? Requisitos { get; set; }
    
    [Display(Name = "Benefícios")]
    [DataType(DataType.MultilineText)]
    public string? Beneficios { get; set; }
    
    [Display(Name = "Valor/Salário")]
    [StringLength(100)]
    public string? ValorSalario { get; set; }
    
    [Display(Name = "Permite Home Office")]
    public bool PermiteHomeOffice { get; set; } = false;
    
    [Display(Name = "Horário Flexível")]
    public bool HorarioFlexivel { get; set; } = false;
    
    [StringLength(100)]
    public string? Cidade { get; set; }
    
    [StringLength(2)]
    public string? Estado { get; set; }
    
    [Display(Name = "Contato")]
    [StringLength(200)]
    public string? Contato { get; set; }
    
    [Display(Name = "Link Externo")]
    [Url]
    public string? LinkExterno { get; set; }
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "Data de Expiração")]
    public DateTime? DataExpiracao { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    // Empresa ou mãe que criou a oportunidade
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Criador { get; set; }
}
