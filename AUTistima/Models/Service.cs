using AUTistima.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Serviço de Saúde - profissionais e clínicas disponíveis
/// </summary>
public class Service
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    [Display(Name = "Nome do Profissional/Clínica")]
    public string NomeProfissional { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Especialidade")]
    public Especialidade Especialidade { get; set; }
    
    [Required]
    [Display(Name = "Tipo de Atendimento")]
    public TipoAtendimento TipoAtendimento { get; set; }
    
    [Display(Name = "Descrição do Serviço")]
    [DataType(DataType.MultilineText)]
    public string? Descricao { get; set; }
    
    [StringLength(50)]
    [Display(Name = "Registro Profissional")]
    public string? RegistroProfissional { get; set; }
    
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
    [Display(Name = "Telefone")]
    public string? Telefone { get; set; }
    
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }
    
    [Display(Name = "Site/Instagram")]
    [Url]
    public string? Website { get; set; }
    
    [Display(Name = "Atende Online")]
    public bool AtendeOnline { get; set; } = false;
    
    [Display(Name = "Valor da Consulta")]
    [StringLength(100)]
    public string? ValorConsulta { get; set; }
    
    [Display(Name = "Observações")]
    [DataType(DataType.MultilineText)]
    public string? Observacoes { get; set; }
    
    [Display(Name = "Verificado pela Plataforma")]
    public bool Verificado { get; set; } = false;
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    
    // Profissional vinculado (opcional)
    public string? UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser? Profissional { get; set; }
}
