using System.ComponentModel.DataAnnotations;

namespace AUTistima.Models.Enums;

/// <summary>
/// Tipos de atividade rastreados no sistema
/// </summary>
public enum TipoAtividade
{
    [Display(Name = "Login")]
    Login = 0,
    
    [Display(Name = "Logout")]
    Logout = 1,
    
    [Display(Name = "Cadastro")]
    Cadastro = 2,
    
    [Display(Name = "Atualização de Perfil")]
    AtualizacaoPerfil = 3,
    
    // Posts e Interações
    [Display(Name = "Criação de Post")]
    CriacaoPost = 10,
    
    [Display(Name = "Visualização de Post")]
    VisualizacaoPost = 11,
    
    [Display(Name = "Acolhimento (Curtida)")]
    Acolhimento = 12,
    
    [Display(Name = "Comentário")]
    Comentario = 13,
    
    // Manejos
    [Display(Name = "Criação de Manejo")]
    CriacaoManejo = 20,
    
    [Display(Name = "Visualização de Manejo")]
    VisualizacaoManejo = 21,
    
    [Display(Name = "Validação de Manejo")]
    ValidacaoManejo = 22,
    
    // Triagens
    [Display(Name = "Solicitação de Triagem")]
    SolicitacaoTriagem = 30,
    
    [Display(Name = "Avaliação de Triagem")]
    AvaliacaoTriagem = 31,
    
    [Display(Name = "Conclusão de Triagem")]
    ConclusaoTriagem = 32,
    
    // Oportunidades
    [Display(Name = "Criação de Oportunidade")]
    CriacaoOportunidade = 40,
    
    [Display(Name = "Visualização de Oportunidade")]
    VisualizacaoOportunidade = 41,
    
    [Display(Name = "Candidatura")]
    Candidatura = 42,
    
    // Busca e Navegação
    [Display(Name = "Busca")]
    Busca = 50,
    
    [Display(Name = "Visualização de Página")]
    VisualizacaoPagina = 51,
    
    [Display(Name = "Consulta ao Glossário")]
    ConsultaGlossario = 52,
    
    // Chat e Comunicação
    [Display(Name = "Envio de Mensagem")]
    EnvioMensagem = 60,
    
    [Display(Name = "Leitura de Mensagem")]
    LeituraMensagem = 61,
    
    // Filhos
    [Display(Name = "Cadastro de Filho")]
    CadastroFilho = 70,
    
    [Display(Name = "Atualização de Filho")]
    AtualizacaoFilho = 71,
    
    // Downloads e Relatórios
    [Display(Name = "Download de Relatório")]
    DownloadRelatorio = 80,
    
    [Display(Name = "Exportação de Dados")]
    ExportacaoDados = 81,
    
    // Admin
    [Display(Name = "Ação Administrativa")]
    AcaoAdministrativa = 90,
    
    // Pânico
    [Display(Name = "Acionamento de Alerta de Pânico")]
    AcionamentoPanico = 100,
    
    [Display(Name = "Confirmação de Alerta de Pânico")]
    ConfirmacaoPanico = 101,
    
    [Display(Name = "Redirecionamento WhatsApp")]
    RedirecionamentoWhatsApp = 102
}

/// <summary>
/// Tipo de dispositivo usado para acessar a plataforma
/// </summary>
public enum TipoDispositivo
{
    [Display(Name = "Navegador Web")]
    Web = 0,
    
    [Display(Name = "PWA Mobile")]
    PWAMobile = 1,
    
    [Display(Name = "PWA Desktop")]
    PWADesktop = 2,
    
    [Display(Name = "App Android")]
    Android = 3,
    
    [Display(Name = "App iOS")]
    iOS = 4,
    
    [Display(Name = "API")]
    API = 5
}
