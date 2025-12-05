using System.ComponentModel.DataAnnotations;

namespace AUTistima.Models;

/// <summary>
/// Escola cadastrada no sistema
/// </summary>
public class School
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    [Display(Name = "Nome da Escola")]
    public string Nome { get; set; } = string.Empty;
    
    [StringLength(14)]
    [Display(Name = "CNPJ")]
    public string? CNPJ { get; set; }
    
    [StringLength(200)]
    [Display(Name = "Endereço")]
    public string? Endereco { get; set; }
    
    [StringLength(100)]
    public string? Bairro { get; set; }
    
    [StringLength(100)]
    public string? Cidade { get; set; }
    
    [StringLength(2)]
    public string? Estado { get; set; }
    
    [StringLength(9)]
    public string? CEP { get; set; }
    
    [StringLength(20)]
    public string? Telefone { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Display(Name = "Escola Pública?")]
    public bool EscolaPublica { get; set; } = true;
    
    [Display(Name = "Possui Sala de Recursos?")]
    public bool PossuiSalaRecursos { get; set; } = false;
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    
    // Navegações
    public virtual ICollection<Child> Alunos { get; set; } = new List<Child>();
    public virtual ICollection<ScreeningRequest> Triagens { get; set; } = new List<ScreeningRequest>();
}
