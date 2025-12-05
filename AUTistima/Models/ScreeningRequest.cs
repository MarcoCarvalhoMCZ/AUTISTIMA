using AUTistima.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTistima.Models;

/// <summary>
/// Solicitação de Triagem - fluxo de avaliação de alunos com sinais de TEA
/// </summary>
public class ScreeningRequest
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Nome do Aluno (pode ser anonimizado)")]
    [StringLength(100)]
    public string? NomeAluno { get; set; }
    
    [Display(Name = "Código Identificador")]
    [StringLength(20)]
    public string? CodigoIdentificador { get; set; }
    
    [Display(Name = "Idade do Aluno")]
    public int? IdadeAluno { get; set; }
    
    [Display(Name = "Série/Ano")]
    [StringLength(50)]
    public string? SerieAno { get; set; }
    
    [Required]
    [Display(Name = "Sinais Observados")]
    [DataType(DataType.MultilineText)]
    public string SinaisObservados { get; set; } = string.Empty;
    
    [Display(Name = "Contexto da Observação")]
    [DataType(DataType.MultilineText)]
    public string? ContextoObservacao { get; set; }
    
    [Required]
    [Display(Name = "Status")]
    public StatusTriagem Status { get; set; } = StatusTriagem.Pendente;
    
    [Display(Name = "Parecer do Profissional")]
    [DataType(DataType.MultilineText)]
    public string? ParecerProfissional { get; set; }
    
    [Display(Name = "Recomendações")]
    [DataType(DataType.MultilineText)]
    public string? Recomendacoes { get; set; }
    
    [Display(Name = "Encaminhamento")]
    [DataType(DataType.MultilineText)]
    public string? Encaminhamento { get; set; }
    
    public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
    
    public DateTime? DataAvaliacao { get; set; }
    
    public DateTime? DataConclusao { get; set; }
    
    // Escola
    [Required]
    public int EscolaId { get; set; }
    
    [ForeignKey("EscolaId")]
    public virtual School? Escola { get; set; }
    
    // Professor/Educador que solicitou
    [Required]
    public string ProfessorSolicitanteId { get; set; } = string.Empty;
    
    [ForeignKey("ProfessorSolicitanteId")]
    public virtual ApplicationUser? ProfessorSolicitante { get; set; }
    
    // Profissional de saúde responsável pela avaliação
    public string? ProfissionalResponsavelId { get; set; }
    
    [ForeignKey("ProfissionalResponsavelId")]
    public virtual ApplicationUser? ProfissionalResponsavel { get; set; }
}
