using System.ComponentModel.DataAnnotations;

namespace AUTistima.Models.Enums;

/// <summary>
/// Tipos de oportunidade no marketplace
/// </summary>
public enum TipoOportunidade
{
    /// <summary>
    /// Vaga de emprego para mãe atípica
    /// </summary>
    [Display(Name = "Emprego para Mãe")]
    EmpregoParaMae = 1,
    
    /// <summary>
    /// Vaga de emprego para pessoa autista
    /// </summary>
    [Display(Name = "Emprego para Autista")]
    EmpregoParaFilho = 2,
    
    /// <summary>
    /// Serviço oferecido por mãe (ex: bolos, design, etc.)
    /// </summary>
    [Display(Name = "Serviço de Mãe")]
    ServicoMae = 3,
    
    /// <summary>
    /// Curso ou capacitação
    /// </summary>
    [Display(Name = "Capacitação")]
    Capacitacao = 4
}
