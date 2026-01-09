using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using AUTistima.Services;

namespace AUTistima.Controllers;

[Authorize]
public class PanicoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPanicService _panicService;
    private readonly ILogger<PanicoController> _logger;

    public PanicoController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IPanicService panicService,
        ILogger<PanicoController> logger)
    {
        _context = context;
        _userManager = userManager;
        _panicService = panicService;
        _logger = logger;
    }

    /// <summary>
    /// Verifica se o usuário é mãe (único perfil que pode usar pânico)
    /// </summary>
    private async Task<bool> IsMae()
    {
        var user = await _userManager.GetUserAsync(User);
        return user?.TipoPerfil == TipoPerfil.Mae;
    }

    /// <summary>
    /// GET: Panico/Index - Página do alerta de pânico
    /// </summary>
    public async Task<IActionResult> Index()
    {
        if (!await IsMae())
        {
            TempData["Mensagem"] = "⛔ Apenas mães podem acessar esta funcionalidade.";
            return RedirectToAction("Index", "Home");
        }

        var user = await _userManager.GetUserAsync(User);
        var alertasAtivos = await _panicService.ObterAlertasAtivosPorUsuarioAsync(user!.Id);

        ViewBag.AlertasAtivos = alertasAtivos;

        return View();
    }

    /// <summary>
    /// POST: Panico/AcionarAlerta - Aciona um novo alerta de pânico
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AcionarAlerta([FromBody] AlertaPanicoRequest request)
    {
        if (!await IsMae())
        {
            return Json(new { sucesso = false, mensagem = "Acesso negado." });
        }

        try
        {
            // Validações
            if (string.IsNullOrWhiteSpace(request.Descricao))
                return Json(new { sucesso = false, mensagem = "Descrição é obrigatória." });

            if (request.Descricao.Length > 500)
                return Json(new { sucesso = false, mensagem = "Descrição não pode exceder 500 caracteres." });

            var user = await _userManager.GetUserAsync(User);

            // Criar alerta
            var alerta = await _panicService.CriarAlertaAsync(
                user!.Id,
                request.Descricao,
                request.NivelUrgencia);

            // Registrar atividade
            var atividade = new UserActivity
            {
                UserId = user.Id,
                TipoAtividade = TipoAtividade.AcionamentoPanico,
                Detalhes = $"Alerta de pânico acionado. Nível: {request.NivelUrgencia}. ID Alerta: {alerta.Id}",
                DataHora = DateTime.UtcNow,
                EnderecoIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconhecido",
                UserAgent = Request.Headers["User-Agent"].ToString()
            };

            _context.UserActivities.Add(atividade);
            await _context.SaveChangesAsync();

            _logger.LogWarning($"⚠️ ALERTA DE PÂNICO acionado pelo usuário {user.NomeCompleto} ({user.Email})");

            return Json(new
            {
                sucesso = true,
                mensagem = "✅ Alerta de pânico acionado com sucesso!",
                panicAlertId = alerta.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao acionar alerta de pânico.");
            return Json(new { sucesso = false, mensagem = "Erro ao acionar alerta. Tente novamente." });
        }
    }

    /// <summary>
    /// POST: Panico/ConfirmarAlerta - Confirma e gera link do WhatsApp
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ConfirmarAlerta([FromBody] ConfirmarAlertaRequest request)
    {
        if (!await IsMae())
        {
            return Json(new { sucesso = false, mensagem = "Acesso negado." });
        }

        try
        {
            var user = await _userManager.GetUserAsync(User);
            var alerta = await _context.PanicAlerts.FindAsync(request.PanicAlertId);

            if (alerta == null || alerta.UserId != user!.Id)
                return Json(new { sucesso = false, mensagem = "Alerta não encontrado." });

            if (alerta.Confirmado)
                return Json(new { sucesso = false, mensagem = "Este alerta já foi confirmado." });

            // Confirmar alerta e gerar link
            var linkWhatsApp = await _panicService.ConfirmarAlertaAsync(request.PanicAlertId);

            // Registrar atividade
            var atividade = new UserActivity
            {
                UserId = user.Id,
                TipoAtividade = TipoAtividade.ConfirmacaoPanico,
                Detalhes = $"Alerta de pânico ID {request.PanicAlertId} confirmado.",
                DataHora = DateTime.UtcNow,
                EnderecoIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconhecido",
                UserAgent = Request.Headers["User-Agent"].ToString()
            };

            _context.UserActivities.Add(atividade);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"✅ Alerta de pânico ID {request.PanicAlertId} confirmado por {user.NomeCompleto}");

            return Json(new
            {
                sucesso = true,
                mensagem = "✅ Confirmado! Abrindo WhatsApp...",
                linkWhatsApp = linkWhatsApp
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao confirmar alerta de pânico.");
            return Json(new { sucesso = false, mensagem = ex.Message });
        }
    }

    /// <summary>
    /// GET: Panico/Historico - Mostra histórico de alertas do usuário
    /// </summary>
    public async Task<IActionResult> Historico()
    {
        if (!await IsMae())
        {
            TempData["Mensagem"] = "⛔ Apenas mães podem acessar esta funcionalidade.";
            return RedirectToAction("Index", "Home");
        }

        var user = await _userManager.GetUserAsync(User);
        var alertas = await _panicService.ObterHistoricoAlertasAsync(user!.Id, 50);

        return View(alertas);
    }

    /// <summary>
    /// GET: Panico/Detalhes/:id - Detalhes de um alerta específico
    /// </summary>
    public async Task<IActionResult> Detalhes(int id)
    {
        if (!await IsMae())
        {
            TempData["Mensagem"] = "⛔ Apenas mães podem acessar esta funcionalidade.";
            return RedirectToAction("Index", "Home");
        }

        var user = await _userManager.GetUserAsync(User);
        var alerta = await _context.PanicAlerts.FindAsync(id);

        if (alerta == null || alerta.UserId != user!.Id)
        {
            TempData["Mensagem"] = "⚠️ Alerta não encontrado.";
            return RedirectToAction("Historico");
        }

        return View(alerta);
    }

    /// <summary>
    /// GET: Panico/Dashboard (Admin/Profissional) - Dashboard de alertas
    /// </summary>
    public async Task<IActionResult> Dashboard()
    {
        var user = await _userManager.GetUserAsync(User);

        // Apenas admin e profissionais podem ver o dashboard
        if (user?.TipoPerfil != TipoPerfil.Administrador && user?.TipoPerfil != TipoPerfil.ProfissionalSaude)
        {
            TempData["Mensagem"] = "⛔ Acesso negado.";
            return RedirectToAction("Index", "Home");
        }

        var alertasAtivos = await _panicService.ObterTodosAlertasAsync(StatusAlertaPanico.Ativo);
        var alertasAtendidos = await _panicService.ObterTodosAlertasAsync(StatusAlertaPanico.Atendido);
        var alertasResolvidos = await _panicService.ObterTodosAlertasAsync(StatusAlertaPanico.Resolvido);

        ViewBag.AlertasAtivos = alertasAtivos.Count;
        ViewBag.AlertasAtendidos = alertasAtendidos.Count;
        ViewBag.AlertasResolvidos = alertasResolvidos.Count;
        ViewBag.TotalAlertas = alertasAtivos.Count + alertasAtendidos.Count + alertasResolvidos.Count;

        return View(alertasAtivos);
    }

    /// <summary>
    /// POST: Panico/MarcarComoAtendido (Admin) - Marca alerta como atendido
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarcarComoAtendido(int id, string? notaAtendimento)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user?.TipoPerfil != TipoPerfil.Administrador && user?.TipoPerfil != TipoPerfil.ProfissionalSaude)
            return Json(new { sucesso = false, mensagem = "Acesso negado." });

        try
        {
            var sucesso = await _panicService.MarcarComoAtendidoAsync(id, notaAtendimento);

            if (sucesso)
            {
                TempData["Mensagem"] = "✅ Alerta marcado como atendido!";
                return Json(new { sucesso = true, mensagem = "Alerta marcado como atendido." });
            }

            return Json(new { sucesso = false, mensagem = "Alerta não encontrado." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao marcar alerta como atendido.");
            return Json(new { sucesso = false, mensagem = ex.Message });
        }
    }
}

/// <summary>
/// Request para acionar alerta de pânico
/// </summary>
public class AlertaPanicoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public NivelUrgenciaPanico NivelUrgencia { get; set; } = NivelUrgenciaPanico.Critico;
}

/// <summary>
/// Request para confirmar alerta
/// </summary>
public class ConfirmarAlertaRequest
{
    public int PanicAlertId { get; set; }
}
