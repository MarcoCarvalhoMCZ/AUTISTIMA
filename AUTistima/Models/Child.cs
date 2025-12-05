using AUTistima.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Filho(a) - criança/adolescente/adulto autista vinculado a uma mãe
/// </summary>
public class Child
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;
    
    [Display(Name = "Data de Nascimento")]
    [DataType(DataType.Date)]
    public DateTime? DataNascimento { get; set; }
    
    [Display(Name = "Nível de Suporte")]
    public NivelSuporte? NivelSuporte { get; set; }
    
    [Display(Name = "Possui diagnóstico formal?")]
    public bool PossuiDiagnostico { get; set; } = false;
    
    [Display(Name = "Data do Diagnóstico")]
    [DataType(DataType.Date)]
    public DateTime? DataDiagnostico { get; set; }
    
    [StringLength(200)]
    [Display(Name = "Escola")]
    public string? EscolaNome { get; set; }
    
    [Display(Name = "ID da Escola")]
    public int? EscolaId { get; set; }
    
    [Display(Name = "Possui PAE (Plano de Atendimento Educacional)?")]
    public bool PossuiPAE { get; set; } = false;
    
    [Display(Name = "Estratégias para Crises")]
    [DataType(DataType.MultilineText)]
    public string? EstrategiasCrise { get; set; }
    
    [Display(Name = "Observações")]
    [DataType(DataType.MultilineText)]
    public string? Observacoes { get; set; }
    
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    
    // Chave estrangeira para a mãe
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Usuario { get; set; }
    
    // Escola (relacionamento opcional)
    [ForeignKey("EscolaId")]
    public virtual School? Escola { get; set; }
}
