using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de Manejos - Estratégias e dicas práticas compartilhadas pelas mães
/// "Saberes não cientificizados" - conhecimento de vivência
/// </summary>
public class ManejosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ManejosController> _logger;

    public ManejosController(ApplicationDbContext context, ILogger<ManejosController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Manejos
    public async Task<IActionResult> Index(CategoriaManejo? categoria = null)
    {
        var query = _context.Manejos
            .Include(m => m.Autor)
            .Where(m => m.Ativo);

        if (categoria.HasValue)
        {
            query = query.Where(m => m.Categoria == categoria.Value);
        }

        var manejos = await query
            .OrderByDescending(m => m.ValidadoPorEspecialista)
            .ThenByDescending(m => m.DataCriacao)
            .ToListAsync();

        ViewBag.CategoriaAtual = categoria;
        return View(manejos);
    }

    // GET: Manejos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var manejo = await _context.Manejos
            .Include(m => m.Autor)
            .Include(m => m.EspecialistaValidador)
            .FirstOrDefaultAsync(m => m.Id == id && m.Ativo);

        if (manejo == null)
        {
            return NotFound();
        }

        return View(manejo);
    }

    // GET: Manejos/Create
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Manejos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create([Bind("Titulo,Categoria,Descricao,DicaPratica,FaixaEtariaIndicada,NivelSuporteIndicado")] Manejo manejo)
    {
        if (ModelState.IsValid)
        {
            manejo.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            manejo.DataCriacao = DateTime.UtcNow;
            manejo.Ativo = true;
            manejo.ValidadoPorEspecialista = false;

            _context.Manejos.Add(manejo);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Sua dica foi compartilhada! Obrigada por ajudar outras mães! 💕";
            return RedirectToAction(nameof(Index));
        }
        return View(manejo);
    }

    // GET: Manejos/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var manejo = await _context.Manejos.FindAsync(id);
        if (manejo == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (manejo.UserId != userId)
        {
            return Forbid();
        }

        return View(manejo);
    }

    // POST: Manejos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Categoria,Descricao,DicaPratica,FaixaEtariaIndicada,NivelSuporteIndicado")] Manejo manejo)
    {
        if (id != manejo.Id)
        {
            return NotFound();
        }

        var existingManejo = await _context.Manejos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        if (existingManejo == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (existingManejo.UserId != userId)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                manejo.UserId = existingManejo.UserId;
                manejo.DataCriacao = existingManejo.DataCriacao;
                manejo.DataAtualizacao = DateTime.UtcNow;
                manejo.Ativo = existingManejo.Ativo;
                manejo.ValidadoPorEspecialista = existingManejo.ValidadoPorEspecialista;
                manejo.EspecialistaValidadorId = existingManejo.EspecialistaValidadorId;

                _context.Update(manejo);
                await _context.SaveChangesAsync();

                TempData["Mensagem"] = "Manejo atualizado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ManejoExists(manejo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(manejo);
    }

    // POST: Manejos/Validar/5 (apenas profissionais de saúde)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Validar(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);
        
        if (user?.TipoPerfil != TipoPerfil.ProfissionalSaude)
        {
            TempData["Erro"] = "Apenas profissionais de saúde podem validar manejos.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var manejo = await _context.Manejos.FindAsync(id);
        if (manejo == null)
        {
            return NotFound();
        }

        manejo.ValidadoPorEspecialista = true;
        manejo.EspecialistaValidadorId = userId;
        manejo.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["Mensagem"] = "Manejo validado com sucesso! ✓";
        return RedirectToAction(nameof(Details), new { id });
    }

    // GET: Manejos/Categoria/Ansiedade
    public async Task<IActionResult> Categoria(CategoriaManejo categoria)
    {
        return await Index(categoria);
    }

    private async Task<bool> ManejoExists(int id)
    {
        return await _context.Manejos.AnyAsync(e => e.Id == id);
    }
}
