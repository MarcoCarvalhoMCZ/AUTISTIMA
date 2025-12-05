using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de notificações do sistema
/// </summary>
[Authorize]
public class NotificacoesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NotificacoesController> _logger;

    public NotificacoesController(ApplicationDbContext context, ILogger<NotificacoesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Notificacoes
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var notificacoes = await _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.DataCriacao)
            .Take(50)
            .ToListAsync();
        
        return View(notificacoes);
    }

    // GET: Notificacoes/NaoLidas (retorna JSON para badge)
    [HttpGet]
    public async Task<IActionResult> NaoLidas()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var count = await _context.Notifications
            .Where(n => n.UserId == userId && !n.Lida)
            .CountAsync();
        
        return Json(new { count });
    }

    // POST: Notificacoes/MarcarLida/5
    [HttpPost]
    public async Task<IActionResult> MarcarLida(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var notificacao = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        
        if (notificacao != null)
        {
            notificacao.Lida = true;
            notificacao.DataLeitura = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            // Se tem link, redireciona
            if (!string.IsNullOrEmpty(notificacao.Link))
            {
                return Redirect(notificacao.Link);
            }
        }
        
        return RedirectToAction(nameof(Index));
    }

    // POST: Notificacoes/MarcarTodasLidas
    [HttpPost]
    public async Task<IActionResult> MarcarTodasLidas()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var notificacoesNaoLidas = await _context.Notifications
            .Where(n => n.UserId == userId && !n.Lida)
            .ToListAsync();
        
        foreach (var notificacao in notificacoesNaoLidas)
        {
            notificacao.Lida = true;
            notificacao.DataLeitura = DateTime.UtcNow;
        }
        
        await _context.SaveChangesAsync();
        
        TempData["Mensagem"] = $"✅ {notificacoesNaoLidas.Count} notificações marcadas como lidas!";
        return RedirectToAction(nameof(Index));
    }

    // POST: Notificacoes/Excluir/5
    [HttpPost]
    public async Task<IActionResult> Excluir(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var notificacao = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        
        if (notificacao != null)
        {
            _context.Notifications.Remove(notificacao);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Index));
    }

    // POST: Notificacoes/LimparTodas
    [HttpPost]
    public async Task<IActionResult> LimparTodas()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var notificacoes = await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
        
        _context.Notifications.RemoveRange(notificacoes);
        await _context.SaveChangesAsync();
        
        TempData["Mensagem"] = "🧹 Todas as notificações foram removidas!";
        return RedirectToAction(nameof(Index));
    }
    
    // Método auxiliar para criar notificações (usado por outros controllers)
    public static async Task CriarNotificacao(
        ApplicationDbContext context,
        string userId,
        string titulo,
        string mensagem,
        TipoNotificacao tipo,
        string? link = null)
    {
        var notificacao = new Notification
        {
            UserId = userId,
            Titulo = titulo,
            Mensagem = mensagem,
            Tipo = tipo,
            Link = link,
            DataCriacao = DateTime.UtcNow
        };
        
        context.Notifications.Add(notificacao);
        await context.SaveChangesAsync();
    }
}
