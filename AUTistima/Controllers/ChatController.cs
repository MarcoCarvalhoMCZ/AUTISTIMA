using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de chat/mensagens entre usuários
/// </summary>
[Authorize]
public class ChatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ChatController> _logger;

    public ChatController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<ChatController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    // GET: Chat - Lista de conversas
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        // Buscar conversas do usuário
        var conversas = await _context.Conversations
            .Include(c => c.Usuario1)
            .Include(c => c.Usuario2)
            .Where(c => c.Ativo && (c.Usuario1Id == userId || c.Usuario2Id == userId))
            .OrderByDescending(c => c.UltimaMensagem)
            .ToListAsync();
        
        // Contar mensagens não lidas por conversa
        var conversasComContagem = new List<ConversaViewModel>();
        
        foreach (var conversa in conversas)
        {
            var outroUsuarioId = conversa.Usuario1Id == userId ? conversa.Usuario2Id : conversa.Usuario1Id;
            var outroUsuario = conversa.Usuario1Id == userId ? conversa.Usuario2 : conversa.Usuario1;
            
            var naoLidas = await _context.ChatMessages
                .Where(m => m.DestinatarioId == userId && 
                           m.RemetenteId == outroUsuarioId && 
                           !m.Lida && m.Ativo)
                .CountAsync();
            
            var ultimaMensagem = await _context.ChatMessages
                .Where(m => m.Ativo && 
                          ((m.RemetenteId == userId && m.DestinatarioId == outroUsuarioId) ||
                           (m.RemetenteId == outroUsuarioId && m.DestinatarioId == userId)))
                .OrderByDescending(m => m.DataEnvio)
                .FirstOrDefaultAsync();
            
            conversasComContagem.Add(new ConversaViewModel
            {
                ConversaId = conversa.Id,
                OutroUsuarioId = outroUsuarioId,
                NomeOutroUsuario = outroUsuario?.NomeCompleto ?? "Usuário",
                FotoOutroUsuario = outroUsuario?.FotoPerfilUrl,
                UltimaMensagem = ultimaMensagem?.Conteudo ?? "",
                DataUltimaMensagem = conversa.UltimaMensagem,
                MensagensNaoLidas = naoLidas
            });
        }
        
        return View(conversasComContagem);
    }

    // GET: Chat/Conversa/userId
    public async Task<IActionResult> Conversa(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var outroUsuario = await _userManager.FindByIdAsync(id);
        
        if (outroUsuario == null)
            return NotFound();
        
        // Buscar ou criar conversa
        var conversa = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Ativo &&
                ((c.Usuario1Id == userId && c.Usuario2Id == id) ||
                 (c.Usuario1Id == id && c.Usuario2Id == userId)));
        
        if (conversa == null)
        {
            conversa = new Conversation
            {
                Usuario1Id = userId!,
                Usuario2Id = id,
                UltimaMensagem = DateTime.UtcNow
            };
            _context.Conversations.Add(conversa);
            await _context.SaveChangesAsync();
        }
        
        // Buscar mensagens
        var mensagens = await _context.ChatMessages
            .Include(m => m.Remetente)
            .Where(m => m.Ativo &&
                ((m.RemetenteId == userId && m.DestinatarioId == id) ||
                 (m.RemetenteId == id && m.DestinatarioId == userId)))
            .OrderBy(m => m.DataEnvio)
            .ToListAsync();
        
        // Marcar como lidas as mensagens recebidas
        var naoLidas = mensagens.Where(m => m.DestinatarioId == userId && !m.Lida).ToList();
        foreach (var msg in naoLidas)
        {
            msg.Lida = true;
            msg.DataLeitura = DateTime.UtcNow;
        }
        
        if (naoLidas.Any())
            await _context.SaveChangesAsync();
        
        ViewBag.OutroUsuario = outroUsuario;
        ViewBag.UserId = userId;
        
        return View(mensagens);
    }

    // POST: Chat/Enviar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enviar(string destinatarioId, string conteudo)
    {
        if (string.IsNullOrEmpty(destinatarioId) || string.IsNullOrWhiteSpace(conteudo))
        {
            TempData["Erro"] = "Mensagem não pode estar vazia.";
            return RedirectToAction(nameof(Conversa), new { id = destinatarioId });
        }
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        // Criar mensagem
        var mensagem = new ChatMessage
        {
            RemetenteId = userId!,
            DestinatarioId = destinatarioId,
            Conteudo = conteudo.Trim(),
            DataEnvio = DateTime.UtcNow
        };
        
        _context.ChatMessages.Add(mensagem);
        
        // Atualizar conversa
        var conversa = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Ativo &&
                ((c.Usuario1Id == userId && c.Usuario2Id == destinatarioId) ||
                 (c.Usuario1Id == destinatarioId && c.Usuario2Id == userId)));
        
        if (conversa != null)
        {
            conversa.UltimaMensagem = DateTime.UtcNow;
        }
        
        // Criar notificação para o destinatário
        var remetente = await _userManager.FindByIdAsync(userId!);
        await NotificacoesController.CriarNotificacao(
            _context,
            destinatarioId,
            "💬 Nova mensagem",
            $"{remetente?.NomeCompleto ?? "Alguém"} enviou uma mensagem para você",
            TipoNotificacao.Mensagem,
            $"/Chat/Conversa/{userId}"
        );
        
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Conversa), new { id = destinatarioId });
    }

    // GET: Chat/NaoLidas (JSON para badge)
    [HttpGet]
    public async Task<IActionResult> NaoLidas()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var count = await _context.ChatMessages
            .Where(m => m.DestinatarioId == userId && !m.Lida && m.Ativo)
            .CountAsync();
        
        return Json(new { count });
    }

    // GET: Chat/NovaConversa - Buscar usuários
    public async Task<IActionResult> NovaConversa(string? busca)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var query = _userManager.Users
            .Where(u => u.Id != userId && u.Ativo);
        
        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(u => 
                u.NomeCompleto.Contains(busca) || 
                u.Email!.Contains(busca));
        }
        
        var usuarios = await query
            .OrderBy(u => u.NomeCompleto)
            .Take(20)
            .ToListAsync();
        
        ViewBag.Busca = busca;
        return View(usuarios);
    }

    // POST: Chat/Excluir/5
    [HttpPost]
    public async Task<IActionResult> Excluir(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var mensagem = await _context.ChatMessages
            .FirstOrDefaultAsync(m => m.Id == id && m.RemetenteId == userId);
        
        if (mensagem != null)
        {
            mensagem.Ativo = false;
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Index));
    }
}

/// <summary>
/// ViewModel para lista de conversas
/// </summary>
public class ConversaViewModel
{
    public int ConversaId { get; set; }
    public string OutroUsuarioId { get; set; } = string.Empty;
    public string NomeOutroUsuario { get; set; } = string.Empty;
    public string? FotoOutroUsuario { get; set; }
    public string UltimaMensagem { get; set; } = string.Empty;
    public DateTime DataUltimaMensagem { get; set; }
    public int MensagensNaoLidas { get; set; }
}
