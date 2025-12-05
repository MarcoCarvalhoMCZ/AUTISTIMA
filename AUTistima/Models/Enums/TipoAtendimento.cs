namespace AUTistima.Models.Enums;

/// <summary>
/// Tipos de atendimento de profissionais de saúde
/// </summary>
public enum TipoAtendimento
{
    /// <summary>
    /// Atendimento totalmente gratuito
    /// </summary>
    Gratuito = 1,
    
    /// <summary>
    /// Valor social (preço reduzido)
    /// </summary>
    ValorSocial = 2,
    
    /// <summary>
    /// Convênio com universidade/clínica escola
    /// </summary>
    ConvenioUniversitario = 3,
    
    /// <summary>
    /// Atendimento particular
    /// </summary>
    Particular = 4
}
