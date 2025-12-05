using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de Oportunidades - Vagas de emprego e marketplace de serviços
/// </summary>
public class OportunidadesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OportunidadesController> _logger;

    public OportunidadesController(ApplicationDbContext context, ILogger<OportunidadesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Oportunidades
    public async Task<IActionResult> Index(TipoOportunidade? tipo = null, string? cidade = null)
    {
        var query = _context.Opportunities
            .Include(o => o.Criador)
            .Where(o => o.Ativo && (o.DataExpiracao == null || o.DataExpiracao > DateTime.UtcNow));

        if (tipo.HasValue)
        {
            query = query.Where(o => o.Tipo == tipo.Value);
        }

        if (!string.IsNullOrEmpty(cidade))
        {
            query = query.Where(o => o.Cidade != null && o.Cidade.Contains(cidade));
        }

        var oportunidades = await query
            .OrderByDescending(o => o.DataCriacao)
            .ToListAsync();

        ViewBag.TipoAtual = tipo;
        ViewBag.CidadeAtual = cidade;
        
        // Estatísticas
        ViewBag.TotalVagasMaes = await _context.Opportunities.CountAsync(o => o.Ativo && o.Tipo == TipoOportunidade.EmpregoParaMae);
        ViewBag.TotalVagasFilhos = await _context.Opportunities.CountAsync(o => o.Ativo && o.Tipo == TipoOportunidade.EmpregoParaFilho);
        ViewBag.TotalServicos = await _context.Opportunities.CountAsync(o => o.Ativo && o.Tipo == TipoOportunidade.ServicoMae);
        ViewBag.EmpresasAmigas = await _context.Users.CountAsync(u => u.EmpresaAmiga);

        return View(oportunidades);
    }

    // GET: Oportunidades/Empregos
    public async Task<IActionResult> Empregos()
    {
        var oportunidades = await _context.Opportunities
            .Include(o => o.Criador)
            .Where(o => o.Ativo && 
                   (o.Tipo == TipoOportunidade.EmpregoParaMae || o.Tipo == TipoOportunidade.EmpregoParaFilho) &&
                   (o.DataExpiracao == null || o.DataExpiracao > DateTime.UtcNow))
            .OrderByDescending(o => o.Criador!.EmpresaAmiga)
            .ThenByDescending(o => o.DataCriacao)
            .ToListAsync();

        return View(oportunidades);
    }

    // GET: Oportunidades/Servicos (Marketplace de mães)
    public async Task<IActionResult> Servicos(string? categoria = null)
    {
        var query = _context.Opportunities
            .Include(o => o.Criador)
            .Where(o => o.Ativo && o.Tipo == TipoOportunidade.ServicoMae);

        var servicos = await query
            .OrderByDescending(o => o.DataCriacao)
            .ToListAsync();

        return View(servicos);
    }

    // GET: Oportunidades/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var oportunidade = await _context.Opportunities
            .Include(o => o.Criador)
            .FirstOrDefaultAsync(o => o.Id == id && o.Ativo);

        if (oportunidade == null)
        {
            return NotFound();
        }

        return View(oportunidade);
    }

    // GET: Oportunidades/Create
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        // Apenas empresas e mães podem criar oportunidades
        if (user?.TipoPerfil != TipoPerfil.Empresa && user?.TipoPerfil != TipoPerfil.Mae)
        {
            TempData["Erro"] = "Apenas empresas e mães podem criar oportunidades.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.TipoPerfil = user.TipoPerfil;
        return View();
    }

    // POST: Oportunidades/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create([Bind("Titulo,Tipo,Descricao,Requisitos,Beneficios,ValorSalario,PermiteHomeOffice,HorarioFlexivel,Cidade,Estado,Contato,LinkExterno,DataExpiracao")] Opportunity oportunidade)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user?.TipoPerfil != TipoPerfil.Empresa && user?.TipoPerfil != TipoPerfil.Mae)
        {
            return Forbid();
        }

        // Mães só podem criar serviços
        if (user.TipoPerfil == TipoPerfil.Mae && oportunidade.Tipo != TipoOportunidade.ServicoMae)
        {
            ModelState.AddModelError("Tipo", "Mães podem apenas cadastrar serviços próprios.");
        }

        if (ModelState.IsValid)
        {
            oportunidade.UserId = userId!;
            oportunidade.DataCriacao = DateTime.UtcNow;
            oportunidade.Ativo = true;

            _context.Opportunities.Add(oportunidade);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Oportunidade publicada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.TipoPerfil = user?.TipoPerfil;
        return View(oportunidade);
    }

    // GET: Oportunidades/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var oportunidade = await _context.Opportunities.FindAsync(id);
        if (oportunidade == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (oportunidade.UserId != userId)
        {
            return Forbid();
        }

        return View(oportunidade);
    }

    // POST: Oportunidades/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Tipo,Descricao,Requisitos,Beneficios,ValorSalario,PermiteHomeOffice,HorarioFlexivel,Cidade,Estado,Contato,LinkExterno,DataExpiracao")] Opportunity oportunidade)
    {
        if (id != oportunidade.Id)
        {
            return NotFound();
        }

        var existingOpp = await _context.Opportunities.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        if (existingOpp == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (existingOpp.UserId != userId)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                oportunidade.UserId = existingOpp.UserId;
                oportunidade.DataCriacao = existingOpp.DataCriacao;
                oportunidade.Ativo = existingOpp.Ativo;

                _context.Update(oportunidade);
                await _context.SaveChangesAsync();

                TempData["Mensagem"] = "Oportunidade atualizada com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OportunidadeExists(oportunidade.Id))
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
        return View(oportunidade);
    }

    // GET: Oportunidades/EmpresasAmigas
    public async Task<IActionResult> EmpresasAmigas()
    {
        var empresas = await _context.Users
            .Where(u => u.TipoPerfil == TipoPerfil.Empresa && u.EmpresaAmiga && u.Ativo)
            .ToListAsync();

        return View(empresas);
    }

    private async Task<bool> OportunidadeExists(int id)
    {
        return await _context.Opportunities.AnyAsync(e => e.Id == id);
    }
}
