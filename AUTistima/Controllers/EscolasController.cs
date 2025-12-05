using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Security.Claims;

namespace AUTistima.Controllers;

public class EscolasController : Controller
{
    private readonly ApplicationDbContext _context;

    public EscolasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Escolas
    public async Task<IActionResult> Index(string? cidade, string? estado, bool? publica)
    {
        var query = _context.Schools.Where(e => e.Ativo).AsQueryable();

        if (!string.IsNullOrEmpty(cidade))
        {
            query = query.Where(e => e.Cidade != null && e.Cidade.Contains(cidade));
        }

        if (!string.IsNullOrEmpty(estado))
        {
            query = query.Where(e => e.Estado == estado);
        }

        if (publica.HasValue)
        {
            query = query.Where(e => e.EscolaPublica == publica.Value);
        }

        ViewBag.Cidade = cidade;
        ViewBag.Estado = estado;
        ViewBag.Publica = publica;

        var escolas = await query.OrderBy(e => e.Nome).ToListAsync();
        return View(escolas);
    }

    // GET: Escolas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var escola = await _context.Schools
            .FirstOrDefaultAsync(e => e.Id == id);

        if (escola == null)
        {
            return NotFound();
        }

        return View(escola);
    }

    // GET: Escolas/Create
    [Authorize]
    public IActionResult Create()
    {
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user?.TipoPerfil != TipoPerfil.Governo && user?.TipoPerfil != TipoPerfil.ProfissionalEducacao)
        {
            TempData["Erro"] = "Apenas profissionais de educação ou governo podem cadastrar escolas.";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // POST: Escolas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create(School escola)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);
        
        if (user?.TipoPerfil != TipoPerfil.Governo && user?.TipoPerfil != TipoPerfil.ProfissionalEducacao)
        {
            TempData["Erro"] = "Apenas profissionais de educação ou governo podem cadastrar escolas.";
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            escola.Ativo = true;
            _context.Add(escola);
            await _context.SaveChangesAsync();
            TempData["Sucesso"] = "Escola cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        return View(escola);
    }

    // GET: Escolas/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user?.TipoPerfil != TipoPerfil.Governo && user?.TipoPerfil != TipoPerfil.ProfissionalEducacao)
        {
            TempData["Erro"] = "Apenas profissionais de educação ou governo podem editar escolas.";
            return RedirectToAction(nameof(Index));
        }

        var escola = await _context.Schools.FindAsync(id);
        if (escola == null)
        {
            return NotFound();
        }
        return View(escola);
    }

    // POST: Escolas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(int id, School escola)
    {
        if (id != escola.Id)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user?.TipoPerfil != TipoPerfil.Governo && user?.TipoPerfil != TipoPerfil.ProfissionalEducacao)
        {
            TempData["Erro"] = "Apenas profissionais de educação ou governo podem editar escolas.";
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(escola);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Escola atualizada com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaExists(escola.Id))
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
        return View(escola);
    }

    private bool EscolaExists(int id)
    {
        return _context.Schools.Any(e => e.Id == id);
    }
}
