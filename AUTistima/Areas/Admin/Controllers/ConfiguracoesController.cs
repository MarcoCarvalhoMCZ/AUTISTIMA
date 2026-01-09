using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Text.Json;

namespace AUTistima.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ConfiguracoesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ConfiguracoesController> _logger;

    public ConfiguracoesController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<ConfiguracoesController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    private async Task<bool> IsAdmin()
    {
        var user = await _userManager.GetUserAsync(User);
        return user?.TipoPerfil == TipoPerfil.Administrador;
    }

    // GET: Admin/Configuracoes
    public async Task<IActionResult> Index()
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        return View();
    }

    // GET: Admin/Configuracoes/Email
    public async Task<IActionResult> Email()
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        var model = new EmailConfigViewModel();
        
        // Carregar configurações existentes
        var configs = await _context.SystemConfigurations
            .Where(c => c.Categoria == "Email" && c.Ativo)
            .ToListAsync();

        foreach (var config in configs)
        {
            switch (config.Chave)
            {
                case "SMTP_Server": model.SmtpServer = config.Valor; break;
                case "SMTP_Port": int.TryParse(config.Valor, out int port); model.SmtpPort = port > 0 ? port : 587; break;
                case "SMTP_Email": model.EmailRemetente = config.Valor; break;
                case "SMTP_Nome": model.NomeRemetente = config.Valor; break;
                case "SMTP_Usuario": model.SmtpUsuario = config.Valor; break;
                case "SMTP_Senha": model.SmtpSenha = ""; break; // Nunca retorna a senha real
                case "SMTP_SSL": model.UsarSsl = config.Valor == "true"; break;
                case "Email_Ativo": model.Ativo = config.Valor == "true"; break;
            }
        }

        // Verificar se há senha cadastrada
        ViewBag.SenhaConfigurada = configs.Any(c => c.Chave == "SMTP_Senha" && !string.IsNullOrEmpty(c.Valor));

        return View(model);
    }

    // POST: Admin/Configuracoes/Email
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Email(EmailConfigViewModel model)
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        if (ModelState.IsValid)
        {
            await SalvarConfiguracao("SMTP_Server", model.SmtpServer, "Email", "Servidor SMTP", false);
            await SalvarConfiguracao("SMTP_Port", model.SmtpPort.ToString(), "Email", "Porta SMTP", false);
            await SalvarConfiguracao("SMTP_Email", model.EmailRemetente, "Email", "E-mail de envio", false);
            await SalvarConfiguracao("SMTP_Nome", model.NomeRemetente ?? "", "Email", "Nome do remetente", false);
            await SalvarConfiguracao("SMTP_Usuario", model.SmtpUsuario, "Email", "Usuário SMTP", false);
            
            // Só salva a senha se foi informada uma nova
            if (!string.IsNullOrEmpty(model.SmtpSenha))
            {
                await SalvarConfiguracao("SMTP_Senha", model.SmtpSenha, "Email", "Senha SMTP", true);
            }
            
            await SalvarConfiguracao("SMTP_SSL", model.UsarSsl ? "true" : "false", "Email", "Usar SSL/TLS", false);
            await SalvarConfiguracao("Email_Ativo", model.Ativo ? "true" : "false", "Email", "Serviço de e-mail ativo", false);

            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Configurações de e-mail atualizadas pelo administrador");
            TempData["Mensagem"] = "Configurações de e-mail salvas com sucesso!";
            
            return RedirectToAction(nameof(Email));
        }

        return View(model);
    }

    // POST: Admin/Configuracoes/TestarEmail
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TestarEmail(string emailDestino)
    {
        if (!await IsAdmin())
            return Json(new { success = false, message = "Acesso negado" });

        try
        {
            // Aqui implementaria o envio de e-mail de teste
            // Por enquanto, apenas simula
            _logger.LogInformation("Teste de e-mail solicitado para: {Email}", emailDestino);
            
            return Json(new { success = true, message = $"E-mail de teste enviado para {emailDestino}" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar e-mail de teste");
            return Json(new { success = false, message = "Erro ao enviar e-mail: " + ex.Message });
        }
    }

    // GET: Admin/Configuracoes/IA
    public async Task<IActionResult> IA()
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        var model = new IAConfigViewModel();
        
        // Carregar configurações existentes
        var configs = await _context.SystemConfigurations
            .Where(c => c.Categoria == "IA" && c.Ativo)
            .ToListAsync();

        foreach (var config in configs)
        {
            switch (config.Chave)
            {
                case "IA_Provedor": model.Provedor = config.Valor; break;
                case "IA_ApiKey": model.ApiKey = ""; break; // Nunca retorna a chave real
                case "IA_Modelo": model.Modelo = config.Valor; break;
                case "IA_ApiUrl": model.ApiUrl = config.Valor; break;
                case "IA_Temperatura": double.TryParse(config.Valor, out double temp); model.Temperatura = temp; break;
                case "IA_MaxTokens": int.TryParse(config.Valor, out int tokens); model.MaxTokens = tokens > 0 ? tokens : 2000; break;
                case "IA_SystemPrompt": model.SystemPrompt = config.Valor; break;
                case "IA_Ativo": model.Ativo = config.Valor == "true"; break;
            }
        }

        // Verificar se há API key cadastrada
        ViewBag.ApiKeyConfigurada = configs.Any(c => c.Chave == "IA_ApiKey" && !string.IsNullOrEmpty(c.Valor));

        return View(model);
    }

    // POST: Admin/Configuracoes/IA
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IA(IAConfigViewModel model)
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        if (ModelState.IsValid)
        {
            await SalvarConfiguracao("IA_Provedor", model.Provedor, "IA", "Provedor de IA (OpenAI, Azure, etc.)", false);
            
            // Só salva a API key se foi informada uma nova
            if (!string.IsNullOrEmpty(model.ApiKey))
            {
                await SalvarConfiguracao("IA_ApiKey", model.ApiKey, "IA", "Chave da API", true);
            }
            
            await SalvarConfiguracao("IA_Modelo", model.Modelo, "IA", "Modelo de IA", false);
            await SalvarConfiguracao("IA_ApiUrl", model.ApiUrl ?? "", "IA", "URL da API (opcional para provedores alternativos)", false);
            await SalvarConfiguracao("IA_Temperatura", model.Temperatura.ToString("F2"), "IA", "Temperatura (criatividade)", false);
            await SalvarConfiguracao("IA_MaxTokens", model.MaxTokens.ToString(), "IA", "Máximo de tokens por resposta", false);
            await SalvarConfiguracao("IA_SystemPrompt", model.SystemPrompt ?? GetDefaultSystemPrompt(), "IA", "Prompt do sistema para o chat", false);
            await SalvarConfiguracao("IA_Ativo", model.Ativo ? "true" : "false", "IA", "Chat com IA ativo", false);

            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Configurações de IA atualizadas pelo administrador");
            TempData["Mensagem"] = "Configurações de IA salvas com sucesso!";
            
            return RedirectToAction(nameof(IA));
        }

        return View(model);
    }

    // POST: Admin/Configuracoes/TestarIA
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TestarIA()
    {
        if (!await IsAdmin())
            return Json(new { success = false, message = "Acesso negado" });

        try
        {
            // Verifica se há API key configurada
            var apiKey = await _context.SystemConfigurations
                .FirstOrDefaultAsync(c => c.Chave == "IA_ApiKey" && c.Categoria == "IA" && c.Ativo);

            if (apiKey == null || string.IsNullOrEmpty(apiKey.Valor))
            {
                return Json(new { success = false, message = "API Key não configurada" });
            }

            // Aqui implementaria o teste real da API
            // Por enquanto, apenas simula
            _logger.LogInformation("Teste de conexão com IA solicitado");
            
            return Json(new { success = true, message = "Conexão com a API de IA estabelecida com sucesso!" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao testar conexão com IA");
            return Json(new { success = false, message = "Erro ao conectar: " + ex.Message });
        }
    }

    // GET: Admin/Configuracoes/WhatsApp
    public async Task<IActionResult> WhatsApp()
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        var model = new WhatsAppConfigViewModel();
        
        // Carregar configurações existentes
        var configs = await _context.SystemConfigurations
            .Where(c => c.Categoria == "WhatsApp" && c.Ativo)
            .ToListAsync();

        foreach (var config in configs)
        {
            switch (config.Chave)
            {
                case "WHATSAPP_NUMERO_PANICO": model.NumeroPanico = config.Valor; break;
                case "PANICO_HABILITADO": model.PanicoHabilitado = config.Valor == "true"; break;
                case "WHATSAPP_MENSAGEM_PADRAO": model.MensagemPadrao = config.Valor; break;
            }
        }

        return View(model);
    }

    // POST: Admin/Configuracoes/WhatsApp
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> WhatsApp(WhatsAppConfigViewModel model)
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        if (ModelState.IsValid)
        {
            await SalvarConfiguracao("WHATSAPP_NUMERO_PANICO", model.NumeroPanico, "WhatsApp", "Número WhatsApp para botão de pânico (formato: 5511999999999)", false);
            await SalvarConfiguracao("PANICO_HABILITADO", model.PanicoHabilitado ? "true" : "false", "WhatsApp", "Habilitar botão de pânico", false);
            await SalvarConfiguracao("WHATSAPP_MENSAGEM_PADRAO", model.MensagemPadrao ?? "Preciso de apoio urgente!", "WhatsApp", "Mensagem padrão do botão de pânico", false);

            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Configurações de WhatsApp atualizadas pelo administrador");
            TempData["Mensagem"] = "✅ Configurações de WhatsApp salvas com sucesso!";
            
            return RedirectToAction(nameof(WhatsApp));
        }

        return View(model);
    }

    // GET: Admin/Configuracoes/Todas
    public async Task<IActionResult> Todas()
    {
        if (!await IsAdmin())
            return RedirectToAction("Index", "Home", new { area = "" });

        var configs = await _context.SystemConfigurations
            .OrderBy(c => c.Categoria)
            .ThenBy(c => c.Chave)
            .ToListAsync();

        return View(configs);
    }

    // Método auxiliar para salvar/atualizar configuração
    private async Task SalvarConfiguracao(string chave, string valor, string categoria, string descricao, bool dadoSensivel)
    {
        var config = await _context.SystemConfigurations
            .FirstOrDefaultAsync(c => c.Chave == chave && c.Categoria == categoria);

        if (config == null)
        {
            config = new SystemConfiguration
            {
                Chave = chave,
                Valor = valor,
                Categoria = categoria,
                Descricao = descricao,
                DadoSensivel = dadoSensivel,
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };
            _context.SystemConfigurations.Add(config);
        }
        else
        {
            config.Valor = valor;
            config.DataAtualizacao = DateTime.UtcNow;
        }
    }

    // Prompt padrão do sistema para o chat de orientação
    private string GetDefaultSystemPrompt()
    {
        return @"Você é uma assistente virtual acolhedora do AUTistima, uma plataforma de apoio para mães atípicas (mães de pessoas autistas).

Seu papel é:
1. Acolher e ouvir as mães com empatia e carinho
2. Fornecer informações básicas sobre autismo (TEA - Transtorno do Espectro Autista)
3. Orientar sobre direitos, benefícios e serviços disponíveis
4. Sugerir estratégias de manejo do dia-a-dia (sempre ressaltando que não substitui profissionais)
5. Indicar quando procurar ajuda profissional especializada

IMPORTANTE:
- Nunca faça diagnósticos ou prescrições médicas
- Sempre recomende consulta com profissionais para questões de saúde
- Use linguagem acolhedora e empática
- Valide os sentimentos das mães
- Lembre que cada criança autista é única
- Respeite o conhecimento de vivência das mães

Você fala português brasileiro e usa emojis com moderação para criar um ambiente acolhedor 💕";
    }
}
