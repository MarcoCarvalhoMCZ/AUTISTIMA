namespace AUTistima.Models.Enums;

/// <summary>
/// Tipos de perfil de usuário no sistema AUTistima
/// </summary>
public enum TipoPerfil
{
    /// <summary>
    /// Mãe atípica - mãe de pessoa autista
    /// </summary>
    Mae = 1,
    
    /// <summary>
    /// Profissional de saúde (Psicólogo, Fonoaudiólogo, Terapeuta Ocupacional, etc.)
    /// </summary>
    ProfissionalSaude = 2,
    
    /// <summary>
    /// Profissional de educação (Professor, Pedagogo, Coordenador, etc.)
    /// </summary>
    ProfissionalEducacao = 3,
    
    /// <summary>
    /// Empresa parceira (oferece vagas, serviços, etc.)
    /// </summary>
    Empresa = 4,
    
    /// <summary>
    /// Governo/Administração pública
    /// </summary>
    Governo = 5
}
