namespace AUTistima.Models.Enums;

/// <summary>
/// Status da solicitação de triagem educacional
/// </summary>
public enum StatusTriagem
{
    /// <summary>
    /// Aguardando avaliação
    /// </summary>
    Pendente = 1,
    
    /// <summary>
    /// Em processo de avaliação por profissional
    /// </summary>
    EmAvaliacao = 2,
    
    /// <summary>
    /// Encaminhado para diagnóstico formal
    /// </summary>
    Encaminhado = 3,
    
    /// <summary>
    /// Avaliação concluída - sem sinais de TEA
    /// </summary>
    Concluido = 4,
    
    /// <summary>
    /// Solicitação cancelada
    /// </summary>
    Cancelado = 5
}
