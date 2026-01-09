using System.ComponentModel.DataAnnotations;

namespace AUTistima.Models;

/// <summary>
/// Configurações do Sistema - armazena configurações globais como API keys, SMTP, etc.
/// Usa padrão chave-valor com criptografia para dados sensíveis
/// </summary>
public class SystemConfiguration
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Chave")]
    public string Chave { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Valor")]
    public string Valor { get; set; } = string.Empty;
    
    [StringLength(500)]
    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }
    
    [StringLength(50)]
    [Display(Name = "Categoria")]
    public string Categoria { get; set; } = "Geral";
    
    /// <summary>
    /// Indica se o valor é sensível (senha, API key) e deve ser mascarado na exibição
    /// </summary>
    [Display(Name = "Dado Sensível")]
    public bool DadoSensivel { get; set; } = false;
    
    /// <summary>
    /// Indica se a configuração está ativa
    /// </summary>
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    public DateTime? DataAtualizacao { get; set; }
}

/// <summary>
/// ViewModel para edição de configurações de e-mail
/// </summary>
public class EmailConfigViewModel
{
    [Required(ErrorMessage = "O servidor SMTP é obrigatório")]
    [Display(Name = "Servidor SMTP")]
    public string SmtpServer { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A porta é obrigatória")]
    [Display(Name = "Porta")]
    [Range(1, 65535, ErrorMessage = "Porta inválida")]
    public int SmtpPort { get; set; } = 587;
    
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [Display(Name = "E-mail de Envio")]
    public string EmailRemetente { get; set; } = string.Empty;
    
    [Display(Name = "Nome do Remetente")]
    public string? NomeRemetente { get; set; }
    
    [Required(ErrorMessage = "O usuário é obrigatório")]
    [Display(Name = "Usuário")]
    public string SmtpUsuario { get; set; } = string.Empty;
    
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string? SmtpSenha { get; set; }
    
    [Display(Name = "Usar SSL/TLS")]
    public bool UsarSsl { get; set; } = true;
    
    [Display(Name = "Ativo")]
    public bool Ativo { get; set; } = true;
}

/// <summary>
/// ViewModel para edição de configurações da IA
/// </summary>
public class IAConfigViewModel
{
    [Display(Name = "Provedor de IA")]
    public string Provedor { get; set; } = "OpenAI";
    
    [Display(Name = "Chave da API")]
    [DataType(DataType.Password)]
    public string? ApiKey { get; set; }
    
    [Display(Name = "Modelo")]
    public string Modelo { get; set; } = "gpt-4o-mini";
    
    [Display(Name = "URL da API (opcional)")]
    [Url(ErrorMessage = "URL inválida")]
    public string? ApiUrl { get; set; }
    
    [Display(Name = "Temperatura (0-2)")]
    [Range(0, 2, ErrorMessage = "Temperatura deve estar entre 0 e 2")]
    public double Temperatura { get; set; } = 0.7;
    
    [Display(Name = "Máximo de Tokens")]
    [Range(100, 128000, ErrorMessage = "Máximo de tokens deve estar entre 100 e 128000")]
    public int MaxTokens { get; set; } = 2000;
    
    [Display(Name = "Prompt do Sistema")]
    [DataType(DataType.MultilineText)]
    public string? SystemPrompt { get; set; }
    
    [Display(Name = "Ativo")]
    public bool Ativo { get; set; } = false;
}

/// <summary>
/// ViewModel para configurações de WhatsApp
/// </summary>
public class WhatsAppConfigViewModel
{
    [Required(ErrorMessage = "O número do WhatsApp é obrigatório")]
    [Display(Name = "Número do WhatsApp")]
    [Phone(ErrorMessage = "Número inválido")]
    [StringLength(20, MinimumLength = 10, ErrorMessage = "Digite o número no formato completo: 5511999999999")]
    public string NumeroPanico { get; set; } = string.Empty;
    
    [Display(Name = "Mensagem Padrão")]
    [StringLength(500)]
    public string? MensagemPadrao { get; set; } = "Preciso de apoio urgente!";
    
    [Display(Name = "Botão de Pânico Habilitado")]
    public bool PanicoHabilitado { get; set; } = true;
}
