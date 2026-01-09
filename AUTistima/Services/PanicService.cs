using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;

namespace AUTistima.Services;

/// <summary>
/// Interface para o serviço de Alerta de Pânico
/// </summary>
public interface IPanicService
{
    /// <summary>
    /// Cria um novo alerta de pânico
    /// </summary>
    Task<PanicAlert> CriarAlertaAsync(string userId, string descricao, NivelUrgenciaPanico nivelUrgencia = NivelUrgenciaPanico.Critico);
    
    /// <summary>
    /// Confirma um alerta e gera link do WhatsApp
    /// </summary>
    Task<string> ConfirmarAlertaAsync(int panicAlertId);
    
    /// <summary>
    /// Obtém o número do WhatsApp configurado no sistema
    /// </summary>
    Task<string?> ObterNumeroWhatsAppAsync();
    
    /// <summary>
    /// Gera URL de conversa do WhatsApp com dados da mãe e filhos
    /// </summary>
    string GerarLinkWhatsApp(string numeroProfissional, string descricaoDemanda, string userId);
    
    /// <summary>
    /// Marca um alerta como atendido
    /// </summary>
    Task<bool> MarcarComoAtendidoAsync(int panicAlertId, string? notaAtendimento = null);
    
    /// <summary>
    /// Obtém alertas ativos de um usuário
    /// </summary>
    Task<List<PanicAlert>> ObterAlertasAtivosPorUsuarioAsync(string userId);
    
    /// <summary>
    /// Obtém histórico de alertas de um usuário
    /// </summary>
    Task<List<PanicAlert>> ObterHistoricoAlertasAsync(string userId, int limit = 10);
    
    /// <summary>
    /// Obtém todos os alertas para profissionais (admin view)
    /// </summary>
    Task<List<PanicAlert>> ObterTodosAlertasAsync(StatusAlertaPanico? status = null);
}

/// <summary>
/// Implementação do serviço de Alerta de Pânico
/// </summary>
public class PanicService : IPanicService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PanicService> _logger;

    public PanicService(
        ApplicationDbContext context,
        ILogger<PanicService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Cria um novo alerta de pânico no banco de dados
    /// </summary>
    public async Task<PanicAlert> CriarAlertaAsync(string userId, string descricao, NivelUrgenciaPanico nivelUrgencia = NivelUrgenciaPanico.Critico)
    {
        var alerta = new PanicAlert
        {
            UserId = userId,
            Descricao = descricao,
            NivelUrgencia = nivelUrgencia,
            DataCriacao = DateTime.UtcNow,
            Status = StatusAlertaPanico.Ativo,
            Ativo = true
        };

        _context.PanicAlerts.Add(alerta);
        await _context.SaveChangesAsync();

        _logger.LogWarning($"⚠️ ALERTA DE PÂNICO criado para usuário {userId}. Nível: {nivelUrgencia}. ID: {alerta.Id}");

        return alerta;
    }

    /// <summary>
    /// Confirma um alerta e gera link do WhatsApp
    /// </summary>
    public async Task<string> ConfirmarAlertaAsync(int panicAlertId)
    {
        var alerta = await _context.PanicAlerts.FindAsync(panicAlertId);
        if (alerta == null)
            throw new Exception($"Alerta de pânico com ID {panicAlertId} não encontrado.");

        // Obter número do WhatsApp
        var numeroWhatsApp = await ObterNumeroWhatsAppAsync();
        if (string.IsNullOrEmpty(numeroWhatsApp))
            throw new Exception("Número de WhatsApp não configurado no sistema. Contate o administrador.");

        // Gerar link com informações da mãe
        var link = await GerarLinkWhatsAppComDadosAsync(numeroWhatsApp, alerta.Descricao, alerta.UserId);

        // Atualizar alerta
        alerta.Confirmado = true;
        alerta.DataConfirmacao = DateTime.UtcNow;
        alerta.LinkWhatsApp = link;

        _context.PanicAlerts.Update(alerta);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"✅ Alerta de pânico ID {panicAlertId} confirmado. Link WhatsApp gerado.");

        return link;
    }

    /// <summary>
    /// Obtém o número do WhatsApp configurado
    /// </summary>
    public async Task<string?> ObterNumeroWhatsAppAsync()
    {
        var config = await _context.SystemConfigurations
            .FirstOrDefaultAsync(c => c.Chave == "WHATSAPP_NUMERO_PANICO" && c.Ativo);

        return config?.Valor;
    }

    /// <summary>
    /// Gera URL de conversa do WhatsApp com dados da mãe e filhos
    /// Formato: https://wa.me/55XXXXXXXXXXX?text=Mensagem
    /// </summary>
    public string GerarLinkWhatsApp(string numeroProfissional, string descricaoDemanda, string userId)
    {
        // Esta é a versão síncrona que será chamada de forma assíncrona no controller
        // A versão real com dados é a GerarLinkWhatsAppComDadosAsync
        throw new NotImplementedException("Use ConfirmarAlertaAsync para gerar links com dados da mãe");
    }

    /// <summary>
    /// Gera link com dados da mãe e filhos (versão assíncrona)
    /// </summary>
    private async Task<string> GerarLinkWhatsAppComDadosAsync(string numeroProfissional, string descricaoDemanda, string userId)
    {
        _logger.LogInformation($"Iniciando geração de link WhatsApp para usuário {userId}");
        
        // Buscar dados da mãe
        var mae = await _context.Users.FindAsync(userId);
        if (mae == null)
        {
            _logger.LogError($"Usuária com ID {userId} não encontrada - usando link simples");
            return GerarLinkWhatsAppSimples(numeroProfissional, descricaoDemanda);
        }

        _logger.LogInformation($"Mãe encontrada: {mae.NomeCompleto}");

        // Buscar filhos da mãe
        var filhos = await _context.Children
            .Where(f => f.UserId == userId)
            .OrderBy(f => f.Nome)
            .ToListAsync();

        _logger.LogInformation($"Encontrados {filhos.Count} filhos");

        // Montar lista de filhos
        var filhosList = filhos.Any() 
            ? string.Join(", ", filhos.Select(f => f.Nome))
            : "Nenhum filho cadastrado";
        
        // Montar mensagem com dados da mãe (usar Uri.EscapeDataString para tudo)
        var nomeMae = mae.NomeCompleto ?? "Sem nome";
        
        var mensagemCompleta = "🆘 ALERTA DE PÂNICO - AUTISTIMA%0A%0A" +
            $"👩 Mãe: {Uri.EscapeDataString(nomeMae)}%0A" +
            $"👧👦 Filhos: {Uri.EscapeDataString(filhosList)}%0A%0A" +
            $"📝 Situação: {Uri.EscapeDataString(descricaoDemanda)}%0A%0A" +
            "Preciso de apoio urgente. Pode me ajudar?";

        // Limpar número
        var numeroLimpo = System.Text.RegularExpressions.Regex.Replace(numeroProfissional ?? "", @"[^0-9]", "");
        if (!numeroLimpo.StartsWith("55"))
            numeroLimpo = "55" + numeroLimpo;

        var link = $"https://wa.me/{numeroLimpo}?text={mensagemCompleta}";

        _logger.LogInformation($"Link WhatsApp gerado com sucesso para {mae.NomeCompleto} ({filhos.Count} filhos)");

        return link;
    }

    /// <summary>
    /// Gera link simples sem dados da mãe (fallback)
    /// </summary>
    private string GerarLinkWhatsAppSimples(string numeroProfissional, string descricaoDemanda)
    {
        _logger.LogWarning("Usando link WhatsApp SIMPLES (sem dados da mãe)");
        
        // Remover caracteres especiais do número
        var numeroLimpo = System.Text.RegularExpressions.Regex.Replace(numeroProfissional ?? "", @"[^0-9]", "");

        // Garantir que comece com 55 (Brasil)
        if (!numeroLimpo.StartsWith("55"))
            numeroLimpo = "55" + numeroLimpo;

        // Preparar mensagem simples (sem emojis Unicode problemáticos)
        var mensagem = "ALERTA DE PÂNICO%0A%0A" +
            $"Descrição: {Uri.EscapeDataString(descricaoDemanda)}%0A%0A" +
            "Estou precisando de apoio urgente. Pode me ajudar?";

        var link = $"https://wa.me/{numeroLimpo}?text={mensagem}";

        _logger.LogInformation($"Link WhatsApp simples gerado: https://wa.me/{numeroLimpo}?text=...");

        return link;
    }

    /// <summary>
    /// Marca um alerta como atendido
    /// </summary>
    public async Task<bool> MarcarComoAtendidoAsync(int panicAlertId, string? notaAtendimento = null)
    {
        var alerta = await _context.PanicAlerts.FindAsync(panicAlertId);
        if (alerta == null)
            return false;

        alerta.Status = StatusAlertaPanico.Atendido;
        alerta.DataAtendimento = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(notaAtendimento))
            alerta.NotaAtendimento = notaAtendimento;

        _context.PanicAlerts.Update(alerta);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Alerta de pânico ID {panicAlertId} marcado como atendido.");

        return true;
    }

    /// <summary>
    /// Obtém alertas ativos de um usuário
    /// </summary>
    public async Task<List<PanicAlert>> ObterAlertasAtivosPorUsuarioAsync(string userId)
    {
        return await _context.PanicAlerts
            .Include(p => p.Usuario)
            .Where(p => p.UserId == userId && p.Status == StatusAlertaPanico.Ativo && p.Ativo)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();
    }

    /// <summary>
    /// Obtém histórico de alertas de um usuário
    /// </summary>
    public async Task<List<PanicAlert>> ObterHistoricoAlertasAsync(string userId, int limit = 10)
    {
        return await _context.PanicAlerts
            .Include(p => p.Usuario)
            .Where(p => p.UserId == userId && p.Ativo)
            .OrderByDescending(p => p.DataCriacao)
            .Take(limit)
            .ToListAsync();
    }

    /// <summary>
    /// Obtém todos os alertas para profissionais/admin
    /// </summary>
    public async Task<List<PanicAlert>> ObterTodosAlertasAsync(StatusAlertaPanico? status = null)
    {
        var query = _context.PanicAlerts
            .Include(p => p.Usuario)
            .Where(p => p.Ativo);

        if (status.HasValue)
            query = query.Where(p => p.Status == status);

        return await query
            .OrderByDescending(p => p.NivelUrgencia)
            .ThenByDescending(p => p.DataCriacao)
            .ToListAsync();
    }
}
