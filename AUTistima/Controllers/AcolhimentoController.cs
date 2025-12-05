using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller da Central de Acolhimento - Feed onde mães desabafam e se apoiam
/// </summary>
public class AcolhimentoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AcolhimentoController> _logger;

    public AcolhimentoController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<AcolhimentoController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    // GET: Acolhimento
    public async Task<IActionResult> Index()
    {
        var posts = await _context.Posts
            .Include(p => p.Autor)
            .Include(p => p.Acolhimentos)
            .Include(p => p.Comentarios)
                .ThenInclude(c => c.Autor)
            .Where(p => p.Ativo)
            .OrderByDescending(p => p.DataCriacao)
            .Take(50)
            .ToListAsync();

        return View(posts);
    }

    // GET: Acolhimento/Create
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Acolhimento/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create([Bind("Conteudo,PermitirComentarios")] Post post)
    {
        // Remove validação de UserId pois será definido no servidor
        ModelState.Remove("UserId");
        
        if (ModelState.IsValid)
        {
            post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            post.DataCriacao = DateTime.UtcNow;
            post.Ativo = true;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Sua mensagem foi compartilhada com carinho. Você não está sozinha! 💕";
            return RedirectToAction(nameof(Index));
        }
        
        // Log dos erros de validação para debug
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogWarning("Erro de validação: {Error}", error.ErrorMessage);
        }
        
        return View(post);
    }

    // POST: Acolhimento/Acolher (AJAX)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Acolher(int postId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Json(new { success = false, message = "Faça login para acolher" });
        }

        // Verificar se já acolheu
        var existeAcolhimento = await _context.PostAcolhimentos
            .AnyAsync(a => a.PostId == postId && a.UserId == userId);

        if (existeAcolhimento)
        {
            // Remove o acolhimento (toggle)
            var acolhimento = await _context.PostAcolhimentos
                .FirstAsync(a => a.PostId == postId && a.UserId == userId);
            _context.PostAcolhimentos.Remove(acolhimento);
        }
        else
        {
            // Adiciona o acolhimento
            var acolhimento = new PostAcolhimento
            {
                PostId = postId,
                UserId = userId,
                DataAcolhimento = DateTime.UtcNow
            };
            _context.PostAcolhimentos.Add(acolhimento);
            
            // Notificar autor do post
            var post = await _context.Posts.FindAsync(postId);
            if (post != null && post.UserId != userId)
            {
                var usuarioAcolheu = await _userManager.FindByIdAsync(userId);
                await NotificacoesController.CriarNotificacao(
                    _context,
                    post.UserId,
                    "💕 Alguém acolheu sua mensagem!",
                    $"{usuarioAcolheu?.NomeCompleto ?? "Alguém"} enviou um acolhimento para você.",
                    TipoNotificacao.Acolhimento,
                    $"/Acolhimento/Details/{postId}"
                );
            }
        }

        await _context.SaveChangesAsync();

        var totalAcolhimentos = await _context.PostAcolhimentos.CountAsync(a => a.PostId == postId);
        var acolhido = !existeAcolhimento;

        return Json(new { success = true, total = totalAcolhimentos, acolhido });
    }

    // POST: Acolhimento/Comentar
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Comentar(int postId, string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
        {
            TempData["Erro"] = "O comentário não pode estar vazio.";
            return RedirectToAction(nameof(Index));
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Account");
        }

        var comentario = new PostComment
        {
            PostId = postId,
            UserId = userId,
            Conteudo = conteudo,
            DataCriacao = DateTime.UtcNow,
            Ativo = true
        };

        _context.PostComments.Add(comentario);
        await _context.SaveChangesAsync();
        
        // Notificar autor do post
        var post = await _context.Posts.FindAsync(postId);
        if (post != null && post.UserId != userId)
        {
            var usuarioComentou = await _userManager.FindByIdAsync(userId);
            await NotificacoesController.CriarNotificacao(
                _context,
                post.UserId,
                "💬 Novo comentário na sua mensagem",
                $"{usuarioComentou?.NomeCompleto ?? "Alguém"} comentou: \"{(conteudo.Length > 50 ? conteudo.Substring(0, 50) + "..." : conteudo)}\"",
                TipoNotificacao.Comentario,
                $"/Acolhimento/Details/{postId}"
            );
        }

        TempData["Mensagem"] = "Seu apoio foi enviado! 💕";
        return RedirectToAction(nameof(Index));
    }

    // GET: Acolhimento/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _context.Posts
            .Include(p => p.Autor)
            .Include(p => p.Acolhimentos)
            .Include(p => p.Comentarios)
                .ThenInclude(c => c.Autor)
            .FirstOrDefaultAsync(p => p.Id == id && p.Ativo);

        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }
}
