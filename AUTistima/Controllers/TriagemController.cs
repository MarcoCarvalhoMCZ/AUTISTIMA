using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de Triagem Educacional - Fluxo para acelerar diagnóstico precoce
/// Acesso restrito a profissionais de educação e saúde
/// </summary>
[Authorize]
public class TriagemController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TriagemController> _logger;

    public TriagemController(ApplicationDbContext context, ILogger<TriagemController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Triagem (Fila de Triagens)
    public async Task<IActionResult> Index(StatusTriagem? status = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Verifica se o usuário tem permissão para acessar triagem
        if (user.TipoPerfil != TipoPerfil.ProfissionalEducacao && 
            user.TipoPerfil != TipoPerfil.ProfissionalSaude &&
            user.TipoPerfil != TipoPerfil.Governo)
        {
            TempData["Erro"] = "Acesso restrito a profissionais de educação e saúde.";
            return RedirectToAction("Index", "Home");
        }

        var query = _context.ScreeningRequests
            .Include(s => s.Escola)
            .Include(s => s.ProfessorSolicitante)
            .Include(s => s.ProfissionalResponsavel)
            .AsQueryable();

        // Filtros baseados no perfil do usuário
        if (user.TipoPerfil == TipoPerfil.ProfissionalEducacao)
        {
            // Professores veem apenas suas solicitações
            query = query.Where(s => s.ProfessorSolicitanteId == userId);
        }

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status.Value);
        }

        var triagens = await query
            .OrderByDescending(s => s.DataSolicitacao)
            .ToListAsync();

        ViewBag.StatusAtual = status;
        ViewBag.TipoPerfil = user.TipoPerfil;
        return View(triagens);
    }

    // GET: Triagem/Create (Apenas Profissionais de Educação)
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user?.TipoPerfil != TipoPerfil.ProfissionalEducacao)
        {
            TempData["Erro"] = "Apenas profissionais de educação podem criar solicitações de triagem.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Escolas = await _context.Schools.Where(e => e.Ativo).ToListAsync();
        return View();
    }

    // POST: Triagem/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NomeAluno,IdadeAluno,SerieAno,SinaisObservados,ContextoObservacao,EscolaId")] ScreeningRequest triagem)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user?.TipoPerfil != TipoPerfil.ProfissionalEducacao)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            // Gera código identificador anônimo
            triagem.CodigoIdentificador = $"TRI-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
            triagem.ProfessorSolicitanteId = userId!;
            triagem.Status = StatusTriagem.Pendente;
            triagem.DataSolicitacao = DateTime.UtcNow;

            _context.ScreeningRequests.Add(triagem);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = $"Solicitação de triagem criada com sucesso! Código: {triagem.CodigoIdentificador}";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Escolas = await _context.Schools.Where(e => e.Ativo).ToListAsync();
        return View(triagem);
    }

    // GET: Triagem/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var triagem = await _context.ScreeningRequests
            .Include(s => s.Escola)
            .Include(s => s.ProfessorSolicitante)
            .Include(s => s.ProfissionalResponsavel)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (triagem == null)
        {
            return NotFound();
        }

        return View(triagem);
    }

    // POST: Triagem/Assumir/5 (Profissional de Saúde assume a triagem)
    [HttpPost]
    public async Task<IActionResult> Assumir(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user?.TipoPerfil != TipoPerfil.ProfissionalSaude)
        {
            TempData["Erro"] = "Apenas profissionais de saúde podem assumir triagens.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var triagem = await _context.ScreeningRequests.FindAsync(id);
        if (triagem == null)
        {
            return NotFound();
        }

        if (triagem.Status != StatusTriagem.Pendente)
        {
            TempData["Erro"] = "Esta triagem já está sendo avaliada por outro profissional.";
            return RedirectToAction(nameof(Details), new { id });
        }

        triagem.ProfissionalResponsavelId = userId;
        triagem.Status = StatusTriagem.EmAvaliacao;
        triagem.DataAvaliacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["Mensagem"] = "Você assumiu a responsabilidade por esta triagem.";
        return RedirectToAction(nameof(Avaliar), new { id });
    }

    // GET: Triagem/Avaliar/5
    public async Task<IActionResult> Avaliar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var triagem = await _context.ScreeningRequests
            .Include(s => s.Escola)
            .Include(s => s.ProfessorSolicitante)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (triagem == null)
        {
            return NotFound();
        }

        if (triagem.ProfissionalResponsavelId != userId)
        {
            TempData["Erro"] = "Você não é o responsável por esta triagem.";
            return RedirectToAction(nameof(Index));
        }

        return View(triagem);
    }

    // POST: Triagem/Avaliar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Avaliar(int id, string parecerProfissional, string recomendacoes, string encaminhamento, StatusTriagem novoStatus)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var triagem = await _context.ScreeningRequests.FindAsync(id);

        if (triagem == null)
        {
            return NotFound();
        }

        if (triagem.ProfissionalResponsavelId != userId)
        {
            return Forbid();
        }

        triagem.ParecerProfissional = parecerProfissional;
        triagem.Recomendacoes = recomendacoes;
        triagem.Encaminhamento = encaminhamento;
        triagem.Status = novoStatus;
        
        if (novoStatus == StatusTriagem.Encaminhado || novoStatus == StatusTriagem.Concluido)
        {
            triagem.DataConclusao = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        TempData["Mensagem"] = "Avaliação registrada com sucesso!";
        return RedirectToAction(nameof(Details), new { id });
    }

    // GET: Triagem/Fila (Apenas profissionais de saúde - veem triagens pendentes)
    public async Task<IActionResult> Fila()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        if (user?.TipoPerfil != TipoPerfil.ProfissionalSaude)
        {
            TempData["Erro"] = "Acesso restrito a profissionais de saúde.";
            return RedirectToAction(nameof(Index));
        }

        var triagens = await _context.ScreeningRequests
            .Include(s => s.Escola)
            .Include(s => s.ProfessorSolicitante)
            .Where(s => s.Status == StatusTriagem.Pendente)
            .OrderBy(s => s.DataSolicitacao)
            .ToListAsync();

        return View(triagens);
    }
}
