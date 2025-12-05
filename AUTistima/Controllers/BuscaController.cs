using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;

namespace AUTistima.Controllers;

/// <summary>
/// Controller de busca avançada do sistema
/// </summary>
public class BuscaController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BuscaController> _logger;

    public BuscaController(ApplicationDbContext context, ILogger<BuscaController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Busca
    public IActionResult Index(string? q, string? tipo)
    {
        ViewBag.Query = q;
        ViewBag.Tipo = tipo;
        return View();
    }

    // GET: Busca/Resultados
    public async Task<IActionResult> Resultados(string q, string? tipo, int pagina = 1)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return RedirectToAction(nameof(Index));
        }
        
        var termoBusca = q.ToLower().Trim();
        var resultados = new BuscaResultadosViewModel
        {
            Query = q,
            Tipo = tipo,
            Pagina = pagina
        };
        
        // Buscar em cada tipo de conteúdo
        if (string.IsNullOrEmpty(tipo) || tipo == "glossario")
        {
            resultados.Glossario = await _context.GlossaryTerms
                .Where(g => g.TermoTecnico.ToLower().Contains(termoBusca) ||
                           g.ExplicacaoSimples.ToLower().Contains(termoBusca) ||
                           g.Categoria.ToLower().Contains(termoBusca))
                .OrderBy(g => g.TermoTecnico)
                .Take(20)
                .ToListAsync();
        }
        
        if (string.IsNullOrEmpty(tipo) || tipo == "manejos")
        {
            resultados.Manejos = await _context.Manejos
                .Include(m => m.Autor)
                .Where(m => m.Ativo &&
                           (m.Titulo.ToLower().Contains(termoBusca) ||
                            m.Descricao.ToLower().Contains(termoBusca) ||
                            (m.DicaPratica != null && m.DicaPratica.ToLower().Contains(termoBusca))))
                .OrderByDescending(m => m.DataCriacao)
                .Take(20)
                .ToListAsync();
        }
        
        if (string.IsNullOrEmpty(tipo) || tipo == "posts")
        {
            resultados.Posts = await _context.Posts
                .Include(p => p.Autor)
                .Where(p => p.Ativo &&
                           p.Conteudo.ToLower().Contains(termoBusca))
                .OrderByDescending(p => p.DataCriacao)
                .Take(20)
                .ToListAsync();
        }
        
        if (string.IsNullOrEmpty(tipo) || tipo == "servicos")
        {
            resultados.Servicos = await _context.Services
                .Where(s => s.Ativo &&
                           (s.NomeProfissional.ToLower().Contains(termoBusca) ||
                            (s.Descricao != null && s.Descricao.ToLower().Contains(termoBusca)) ||
                            (s.Bairro != null && s.Bairro.ToLower().Contains(termoBusca)) ||
                            (s.Cidade != null && s.Cidade.ToLower().Contains(termoBusca))))
                .OrderBy(s => s.NomeProfissional)
                .Take(20)
                .ToListAsync();
        }
        
        if (string.IsNullOrEmpty(tipo) || tipo == "escolas")
        {
            resultados.Escolas = await _context.Schools
                .Where(e => e.Ativo &&
                           (e.Nome.ToLower().Contains(termoBusca) ||
                            (e.Bairro != null && e.Bairro.ToLower().Contains(termoBusca)) ||
                            (e.Cidade != null && e.Cidade.ToLower().Contains(termoBusca))))
                .OrderBy(e => e.Nome)
                .Take(20)
                .ToListAsync();
        }
        
        if (string.IsNullOrEmpty(tipo) || tipo == "oportunidades")
        {
            resultados.Oportunidades = await _context.Opportunities
                .Include(o => o.Criador)
                .Where(o => o.Ativo &&
                           (o.Titulo.ToLower().Contains(termoBusca) ||
                            o.Descricao.ToLower().Contains(termoBusca) ||
                            (o.Criador != null && o.Criador.NomeEmpresa != null && o.Criador.NomeEmpresa.ToLower().Contains(termoBusca))))
                .OrderByDescending(o => o.DataCriacao)
                .Take(20)
                .ToListAsync();
        }
        
        resultados.TotalResultados = 
            resultados.Glossario.Count + 
            resultados.Manejos.Count + 
            resultados.Posts.Count + 
            resultados.Servicos.Count + 
            resultados.Escolas.Count + 
            resultados.Oportunidades.Count;
        
        return View(resultados);
    }

    // GET: Busca/Rapida (JSON para autocomplete)
    [HttpGet]
    public async Task<IActionResult> Rapida(string q)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
        {
            return Json(new List<object>());
        }
        
        var termoBusca = q.ToLower().Trim();
        var resultados = new List<object>();
        
        // Glossário
        var glossario = await _context.GlossaryTerms
            .Where(g => g.TermoTecnico.ToLower().Contains(termoBusca))
            .Take(3)
            .Select(g => new { tipo = "glossario", titulo = g.TermoTecnico, url = $"/Glossario/Details/{g.Id}" })
            .ToListAsync();
        resultados.AddRange(glossario);
        
        // Manejos
        var manejos = await _context.Manejos
            .Where(m => m.Ativo && m.Titulo.ToLower().Contains(termoBusca))
            .Take(3)
            .Select(m => new { tipo = "manejo", titulo = m.Titulo, url = $"/Manejos/Details/{m.Id}" })
            .ToListAsync();
        resultados.AddRange(manejos);
        
        // Serviços
        var servicos = await _context.Services
            .Where(s => s.Ativo && s.NomeProfissional.ToLower().Contains(termoBusca))
            .Take(3)
            .Select(s => new { tipo = "servico", titulo = s.NomeProfissional, url = $"/Saude/Details/{s.Id}" })
            .ToListAsync();
        resultados.AddRange(servicos);
        
        return Json(resultados);
    }
}

/// <summary>
/// ViewModel para resultados de busca
/// </summary>
public class BuscaResultadosViewModel
{
    public string Query { get; set; } = string.Empty;
    public string? Tipo { get; set; }
    public int Pagina { get; set; } = 1;
    public int TotalResultados { get; set; }
    
    public List<GlossaryTerm> Glossario { get; set; } = new();
    public List<Manejo> Manejos { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public List<Service> Servicos { get; set; } = new();
    public List<School> Escolas { get; set; } = new();
    public List<Opportunity> Oportunidades { get; set; } = new();
}
