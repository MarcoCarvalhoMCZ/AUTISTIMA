using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de Saúde - Busca de profissionais e serviços de saúde
/// </summary>
public class SaudeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SaudeController> _logger;

    public SaudeController(ApplicationDbContext context, ILogger<SaudeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Saude
    public async Task<IActionResult> Index(Especialidade? especialidade = null, TipoAtendimento? tipoAtendimento = null, string? cidade = null, string? bairro = null)
    {
        var query = _context.Services
            .Include(s => s.Profissional)
            .Where(s => s.Ativo);

        if (especialidade.HasValue)
        {
            query = query.Where(s => s.Especialidade == especialidade.Value);
        }

        if (tipoAtendimento.HasValue)
        {
            query = query.Where(s => s.TipoAtendimento == tipoAtendimento.Value);
        }

        if (!string.IsNullOrEmpty(cidade))
        {
            query = query.Where(s => s.Cidade != null && s.Cidade.Contains(cidade));
        }

        if (!string.IsNullOrEmpty(bairro))
        {
            query = query.Where(s => s.Bairro != null && s.Bairro.Contains(bairro));
        }

        var servicos = await query
            .OrderByDescending(s => s.Verificado)
            .ThenBy(s => s.TipoAtendimento)
            .ThenBy(s => s.NomeProfissional)
            .ToListAsync();

        ViewBag.EspecialidadeAtual = especialidade;
        ViewBag.TipoAtendimentoAtual = tipoAtendimento;
        ViewBag.CidadeAtual = cidade;
        ViewBag.BairroAtual = bairro;

        // Estatísticas
        ViewBag.TotalGratuitos = await _context.Services.CountAsync(s => s.Ativo && s.TipoAtendimento == TipoAtendimento.Gratuito);
        ViewBag.TotalValorSocial = await _context.Services.CountAsync(s => s.Ativo && s.TipoAtendimento == TipoAtendimento.ValorSocial);
        ViewBag.TotalConvenio = await _context.Services.CountAsync(s => s.Ativo && s.TipoAtendimento == TipoAtendimento.ConvenioUniversitario);
        ViewBag.TotalOnline = await _context.Services.CountAsync(s => s.Ativo && s.AtendeOnline);

        return View(servicos);
    }

    // GET: Saude/Gratuitos
    public async Task<IActionResult> Gratuitos()
    {
        var servicos = await _context.Services
            .Include(s => s.Profissional)
            .Where(s => s.Ativo && 
                  (s.TipoAtendimento == TipoAtendimento.Gratuito || 
                   s.TipoAtendimento == TipoAtendimento.ConvenioUniversitario))
            .OrderByDescending(s => s.Verificado)
            .ThenBy(s => s.Especialidade)
            .ToListAsync();

        return View(servicos);
    }

    // GET: Saude/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Services
            .Include(s => s.Profissional)
            .FirstOrDefaultAsync(s => s.Id == id && s.Ativo);

        if (servico == null)
        {
            return NotFound();
        }

        return View(servico);
    }

    // GET: Saude/Create
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        // Apenas profissionais de saúde podem criar serviços
        if (user?.TipoPerfil != TipoPerfil.ProfissionalSaude && user?.TipoPerfil != TipoPerfil.Governo)
        {
            TempData["Erro"] = "Apenas profissionais de saúde podem cadastrar serviços.";
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    // POST: Saude/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create([Bind("NomeProfissional,Especialidade,TipoAtendimento,Descricao,RegistroProfissional,Endereco,Bairro,Cidade,Estado,CEP,Telefone,Email,Website,AtendeOnline,ValorConsulta,Observacoes")] Service servico)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user?.TipoPerfil != TipoPerfil.ProfissionalSaude && user?.TipoPerfil != TipoPerfil.Governo)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            servico.UserId = userId;
            servico.DataCadastro = DateTime.UtcNow;
            servico.Ativo = true;
            servico.Verificado = false; // Será verificado posteriormente

            _context.Services.Add(servico);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Serviço cadastrado com sucesso! Aguarde a verificação pela plataforma.";
            return RedirectToAction(nameof(Index));
        }

        return View(servico);
    }

    // GET: Saude/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Services.FindAsync(id);
        if (servico == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (servico.UserId != userId)
        {
            return Forbid();
        }

        return View(servico);
    }

    // POST: Saude/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(int id, [Bind("Id,NomeProfissional,Especialidade,TipoAtendimento,Descricao,RegistroProfissional,Endereco,Bairro,Cidade,Estado,CEP,Telefone,Email,Website,AtendeOnline,ValorConsulta,Observacoes")] Service servico)
    {
        if (id != servico.Id)
        {
            return NotFound();
        }

        var existingService = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (existingService == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (existingService.UserId != userId)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                servico.UserId = existingService.UserId;
                servico.DataCadastro = existingService.DataCadastro;
                servico.Ativo = existingService.Ativo;
                servico.Verificado = existingService.Verificado;

                _context.Update(servico);
                await _context.SaveChangesAsync();

                TempData["Mensagem"] = "Serviço atualizado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServicoExists(servico.Id))
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
        return View(servico);
    }

    // GET: Saude/Especialidade/Psicologia
    public async Task<IActionResult> Especialidade(Especialidade especialidade)
    {
        return await Index(especialidade: especialidade);
    }

    private async Task<bool> ServicoExists(int id)
    {
        return await _context.Services.AnyAsync(e => e.Id == id);
    }
}
