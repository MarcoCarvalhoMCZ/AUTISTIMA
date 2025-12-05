using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;

namespace AUTistima.Controllers;

/// <summary>
/// Controller do Mini Dicionário - Glossário de termos técnicos
/// </summary>
public class GlossarioController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<GlossarioController> _logger;

    public GlossarioController(ApplicationDbContext context, ILogger<GlossarioController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Glossario
    public async Task<IActionResult> Index(string? categoria = null, string? busca = null)
    {
        var query = _context.GlossaryTerms.Where(t => t.Ativo);

        if (!string.IsNullOrEmpty(categoria))
        {
            query = query.Where(t => t.Categoria == categoria);
        }

        if (!string.IsNullOrEmpty(busca))
        {
            query = query.Where(t => 
                t.TermoTecnico.Contains(busca) || 
                t.ExplicacaoSimples.Contains(busca));
        }

        var termos = await query
            .OrderBy(t => t.TermoTecnico)
            .ToListAsync();

        // Categorias disponíveis
        ViewBag.Categorias = await _context.GlossaryTerms
            .Where(t => t.Ativo && t.Categoria != null)
            .Select(t => t.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        ViewBag.CategoriaAtual = categoria;
        ViewBag.BuscaAtual = busca;

        return View(termos);
    }

    // GET: Glossario/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var termo = await _context.GlossaryTerms
            .FirstOrDefaultAsync(t => t.Id == id && t.Ativo);

        if (termo == null)
        {
            return NotFound();
        }

        return View(termo);
    }

    // GET: Glossario/Termo/TEA
    public async Task<IActionResult> Termo(string termo)
    {
        if (string.IsNullOrEmpty(termo))
        {
            return RedirectToAction(nameof(Index));
        }

        var glossarioTermo = await _context.GlossaryTerms
            .FirstOrDefaultAsync(t => t.TermoTecnico.ToLower() == termo.ToLower() && t.Ativo);

        if (glossarioTermo == null)
        {
            TempData["Erro"] = $"Termo '{termo}' não encontrado no glossário.";
            return RedirectToAction(nameof(Index));
        }

        return View("Details", glossarioTermo);
    }

    // API: Busca rápida de termos (para autocomplete)
    [HttpGet]
    public async Task<IActionResult> Buscar(string q)
    {
        if (string.IsNullOrEmpty(q) || q.Length < 2)
        {
            return Json(new List<object>());
        }

        var termos = await _context.GlossaryTerms
            .Where(t => t.Ativo && t.TermoTecnico.Contains(q))
            .Select(t => new { t.Id, t.TermoTecnico, t.ExplicacaoSimples })
            .Take(10)
            .ToListAsync();

        return Json(termos);
    }
}
