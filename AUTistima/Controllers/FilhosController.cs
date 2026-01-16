using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using System.Security.Claims;

namespace AUTistima.Controllers;

[Authorize]
public class FilhosController : Controller
{
    private readonly ApplicationDbContext _context;

    public FilhosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Filhos
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filhos = await _context.Children
            .Where(f => f.UserId == userId)
            .OrderBy(f => f.Nome)
            .ToListAsync();
        
        return View(filhos);
    }

    // GET: Filhos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filho = await _context.Children
            .Include(f => f.Usuario)
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

        if (filho == null)
        {
            return NotFound();
        }

        return View(filho);
    }

    // GET: Filhos/Create
    public async Task<IActionResult> Create()
    {
        var escolas = await _context.Schools.Where(e => e.Ativo).OrderBy(e => e.Nome).ToListAsync();
        ViewBag.Escolas = escolas;
        return View();
    }

    // POST: Filhos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Child filho)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Account");
        }
        
        filho.UserId = userId;
        filho.DataCadastro = DateTime.Now;

        if (ModelState.IsValid)
        {
            _context.Add(filho);
            await _context.SaveChangesAsync();
            TempData["Sucesso"] = "✅ Filho(a) cadastrado(a) com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        
        // Recarregar escolas se validação falhar
        var escolas = await _context.Schools.Where(e => e.Ativo).OrderBy(e => e.Nome).ToListAsync();
        ViewBag.Escolas = escolas;
        return View(filho);
    }

    // GET: Filhos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filho = await _context.Children
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
        
        if (filho == null)
        {
            return NotFound();
        }
        
        var escolas = await _context.Schools.Where(e => e.Ativo).OrderBy(e => e.Nome).ToListAsync();
        ViewBag.Escolas = escolas;
        
        return View(filho);
    }

    // POST: Filhos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Child filho)
    {
        if (id != filho.Id)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var existingFilho = await _context.Children
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

        if (existingFilho == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                existingFilho.Nome = filho.Nome;
                existingFilho.DataNascimento = filho.DataNascimento;
                existingFilho.NivelSuporte = filho.NivelSuporte;
                existingFilho.PossuiDiagnostico = filho.PossuiDiagnostico;
                existingFilho.DataDiagnostico = filho.DataDiagnostico;
                existingFilho.EscolaNome = filho.EscolaNome;
                existingFilho.EscolaId = filho.EscolaId;
                existingFilho.PossuiPAE = filho.PossuiPAE;
                existingFilho.EstrategiasCrise = filho.EstrategiasCrise;
                existingFilho.Observacoes = filho.Observacoes;

                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Dados atualizados com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilhoExists(filho.Id))
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
        return View(filho);
    }

    // GET: Filhos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filho = await _context.Children
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

        if (filho == null)
        {
            return NotFound();
        }

        return View(filho);
    }

    // POST: Filhos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filho = await _context.Children
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
        
        if (filho != null)
        {
            _context.Children.Remove(filho);
            await _context.SaveChangesAsync();
            TempData["Sucesso"] = "Cadastro removido com sucesso.";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool FilhoExists(int id)
    {
        return _context.Children.Any(e => e.Id == id);
    }
}
