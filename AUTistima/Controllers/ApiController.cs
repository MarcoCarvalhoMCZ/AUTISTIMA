using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using System.Security.Claims;

namespace AUTistima.Controllers;

/// <summary>
/// API Controller para integração com app mobile e sistemas externos
/// Preparado para futuro aplicativo iOS/Android
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ApiController> _logger;

    public ApiController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<ApiController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    #region Glossário

    /// <summary>
    /// Lista todos os termos do glossário
    /// GET: api/Api/glossario
    /// </summary>
    [HttpGet("glossario")]
    public async Task<ActionResult<IEnumerable<GlossarioDto>>> GetGlossario(string? categoria = null, string? busca = null)
    {
        var query = _context.GlossaryTerms.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(categoria))
            query = query.Where(g => g.Categoria == categoria);
        
        if (!string.IsNullOrWhiteSpace(busca))
            query = query.Where(g => g.TermoTecnico.Contains(busca) || 
                                    g.ExplicacaoSimples.Contains(busca));
        
        var termos = await query
            .OrderBy(g => g.TermoTecnico)
            .Select(g => new GlossarioDto
            {
                Id = g.Id,
                Termo = g.TermoTecnico,
                Explicacao = g.ExplicacaoSimples,
                Categoria = g.Categoria,
                ExemploUso = g.ExemploUso
            })
            .ToListAsync();
        
        return Ok(termos);
    }

    /// <summary>
    /// Busca termo específico por ID
    /// GET: api/Api/glossario/5
    /// </summary>
    [HttpGet("glossario/{id}")]
    public async Task<ActionResult<GlossarioDto>> GetGlossarioTermo(int id)
    {
        var termo = await _context.GlossaryTerms.FindAsync(id);
        
        if (termo == null)
            return NotFound(new { mensagem = "Termo não encontrado" });
        
        return Ok(new GlossarioDto
        {
            Id = termo.Id,
            Termo = termo.TermoTecnico,
            Explicacao = termo.ExplicacaoSimples,
            Categoria = termo.Categoria,
            ExemploUso = termo.ExemploUso
        });
    }

    /// <summary>
    /// Lista categorias do glossário
    /// GET: api/Api/glossario/categorias
    /// </summary>
    [HttpGet("glossario/categorias")]
    public async Task<ActionResult<IEnumerable<string>>> GetCategorias()
    {
        var categorias = await _context.GlossaryTerms
            .Select(g => g.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
        
        return Ok(categorias);
    }

    #endregion

    #region Manejos

    /// <summary>
    /// Lista manejos ativos
    /// GET: api/Api/manejos
    /// </summary>
    [HttpGet("manejos")]
    public async Task<ActionResult<IEnumerable<ManejoDto>>> GetManejos(
        CategoriaManejo? categoria = null, 
        NivelSuporte? nivel = null,
        bool? validados = null)
    {
        var query = _context.Manejos
            .Include(m => m.Autor)
            .Where(m => m.Ativo);
        
        if (categoria.HasValue)
            query = query.Where(m => m.Categoria == categoria.Value);
        
        if (nivel.HasValue)
            query = query.Where(m => m.NivelSuporteIndicado == nivel.Value);
        
        if (validados.HasValue)
            query = query.Where(m => m.ValidadoPorEspecialista == validados.Value);
        
        var manejos = await query
            .OrderByDescending(m => m.DataCriacao)
            .Select(m => new ManejoDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Categoria = m.Categoria.ToString(),
                Descricao = m.Descricao,
                DicaPratica = m.DicaPratica,
                FaixaEtaria = m.FaixaEtariaIndicada,
                NivelSuporte = m.NivelSuporteIndicado.HasValue ? m.NivelSuporteIndicado.Value.ToString() : null,
                ValidadoPorEspecialista = m.ValidadoPorEspecialista,
                Autor = m.Autor != null ? m.Autor.NomeCompleto : "Anônimo",
                DataCriacao = m.DataCriacao
            })
            .ToListAsync();
        
        return Ok(manejos);
    }

    /// <summary>
    /// Busca manejo por ID
    /// GET: api/Api/manejos/5
    /// </summary>
    [HttpGet("manejos/{id}")]
    public async Task<ActionResult<ManejoDto>> GetManejo(int id)
    {
        var manejo = await _context.Manejos
            .Include(m => m.Autor)
            .FirstOrDefaultAsync(m => m.Id == id && m.Ativo);
        
        if (manejo == null)
            return NotFound(new { mensagem = "Manejo não encontrado" });
        
        return Ok(new ManejoDto
        {
            Id = manejo.Id,
            Titulo = manejo.Titulo,
            Categoria = manejo.Categoria.ToString(),
            Descricao = manejo.Descricao,
            DicaPratica = manejo.DicaPratica,
            FaixaEtaria = manejo.FaixaEtariaIndicada,
            NivelSuporte = manejo.NivelSuporteIndicado?.ToString(),
            ValidadoPorEspecialista = manejo.ValidadoPorEspecialista,
            Autor = manejo.Autor?.NomeCompleto ?? "Anônimo",
            DataCriacao = manejo.DataCriacao
        });
    }

    #endregion

    #region Serviços de Saúde

    /// <summary>
    /// Lista serviços de saúde
    /// GET: api/Api/servicos
    /// </summary>
    [HttpGet("servicos")]
    public async Task<ActionResult<IEnumerable<ServicoDto>>> GetServicos(
        Especialidade? especialidade = null,
        TipoAtendimento? tipo = null,
        string? cidade = null)
    {
        var query = _context.Services.Where(s => s.Ativo);
        
        if (especialidade.HasValue)
            query = query.Where(s => s.Especialidade == especialidade.Value);
        
        if (tipo.HasValue)
            query = query.Where(s => s.TipoAtendimento == tipo.Value);
        
        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(s => s.Cidade != null && s.Cidade.Contains(cidade));
        
        var servicos = await query
            .OrderBy(s => s.NomeProfissional)
            .Select(s => new ServicoDto
            {
                Id = s.Id,
                Nome = s.NomeProfissional,
                Especialidade = s.Especialidade.ToString(),
                TipoAtendimento = s.TipoAtendimento.ToString(),
                Descricao = s.Descricao,
                Endereco = s.Endereco,
                Bairro = s.Bairro,
                Cidade = s.Cidade,
                Estado = s.Estado,
                Telefone = s.Telefone,
                Email = s.Email,
                Website = s.Website,
                ValorConsulta = s.ValorConsulta,
                AtendeOnline = s.AtendeOnline,
                Verificado = s.Verificado
            })
            .ToListAsync();
        
        return Ok(servicos);
    }

    #endregion

    #region Escolas

    /// <summary>
    /// Lista escolas
    /// GET: api/Api/escolas
    /// </summary>
    [HttpGet("escolas")]
    public async Task<ActionResult<IEnumerable<EscolaDto>>> GetEscolas(string? cidade = null, string? bairro = null)
    {
        var query = _context.Schools.Where(e => e.Ativo);
        
        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(e => e.Cidade != null && e.Cidade.Contains(cidade));
        
        if (!string.IsNullOrWhiteSpace(bairro))
            query = query.Where(e => e.Bairro != null && e.Bairro.Contains(bairro));
        
        var escolas = await query
            .OrderBy(e => e.Nome)
            .Select(e => new EscolaDto
            {
                Id = e.Id,
                Nome = e.Nome,
                TipoRede = e.EscolaPublica ? "Pública" : "Particular",
                Endereco = e.Endereco,
                Bairro = e.Bairro,
                Cidade = e.Cidade,
                Estado = e.Estado,
                Telefone = e.Telefone,
                Email = e.Email,
                PossuiAEE = e.PossuiSalaRecursos,
                PossuiSalaRecursos = e.PossuiSalaRecursos,
                AvaliacaoInclusao = null
            })
            .ToListAsync();
        
        return Ok(escolas);
    }

    #endregion

    #region Oportunidades

    /// <summary>
    /// Lista oportunidades ativas
    /// GET: api/Api/oportunidades
    /// </summary>
    [HttpGet("oportunidades")]
    public async Task<ActionResult<IEnumerable<OportunidadeDto>>> GetOportunidades(TipoOportunidade? tipo = null)
    {
        var query = _context.Opportunities.Where(o => o.Ativo);
        
        if (tipo.HasValue)
            query = query.Where(o => o.Tipo == tipo.Value);
        
        var oportunidades = await query
            .OrderByDescending(o => o.DataCriacao)
            .Select(o => new OportunidadeDto
            {
                Id = o.Id,
                Titulo = o.Titulo,
                Tipo = o.Tipo.ToString(),
                Empresa = o.Criador != null ? o.Criador.NomeEmpresa : null,
                Descricao = o.Descricao,
                Requisitos = o.Requisitos,
                Beneficios = o.Beneficios,
                LocalTrabalho = o.Cidade,
                Salario = o.ValorSalario,
                LinkInscricao = o.LinkExterno,
                DataLimite = o.DataExpiracao,
                DataCriacao = o.DataCriacao
            })
            .ToListAsync();
        
        return Ok(oportunidades);
    }

    #endregion

    #region Busca Unificada

    /// <summary>
    /// Busca unificada em todos os conteúdos
    /// GET: api/Api/busca?q=termo
    /// </summary>
    [HttpGet("busca")]
    public async Task<ActionResult<BuscaApiResultado>> Busca(string q)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            return BadRequest(new { mensagem = "O termo de busca deve ter pelo menos 2 caracteres" });
        
        var termo = q.ToLower();
        
        var resultado = new BuscaApiResultado
        {
            Query = q,
            Glossario = await _context.GlossaryTerms
                .Where(g => g.TermoTecnico.ToLower().Contains(termo) || 
                           g.ExplicacaoSimples.ToLower().Contains(termo))
                .Take(10)
                .Select(g => new GlossarioDto { Id = g.Id, Termo = g.TermoTecnico, Categoria = g.Categoria })
                .ToListAsync(),
            
            Manejos = await _context.Manejos
                .Where(m => m.Ativo && (m.Titulo.ToLower().Contains(termo) || 
                           m.Descricao.ToLower().Contains(termo)))
                .Take(10)
                .Select(m => new ManejoDto { Id = m.Id, Titulo = m.Titulo, Categoria = m.Categoria.ToString() })
                .ToListAsync(),
            
            Servicos = await _context.Services
                .Where(s => s.Ativo && s.NomeProfissional.ToLower().Contains(termo))
                .Take(10)
                .Select(s => new ServicoDto { Id = s.Id, Nome = s.NomeProfissional, Especialidade = s.Especialidade.ToString() })
                .ToListAsync()
        };
        
        resultado.TotalResultados = resultado.Glossario.Count + resultado.Manejos.Count + resultado.Servicos.Count;
        
        return Ok(resultado);
    }

    #endregion

    #region Versão e Status

    /// <summary>
    /// Retorna versão e status da API
    /// GET: api/Api/status
    /// </summary>
    [HttpGet("status")]
    public ActionResult GetStatus()
    {
        return Ok(new
        {
            status = "online",
            versao = "1.0.0",
            nome = "AUTistima API",
            descricao = "API para aplicativo móvel AUTistima",
            timestamp = DateTime.UtcNow
        });
    }

    #endregion
}

#region DTOs

public class GlossarioDto
{
    public int Id { get; set; }
    public string Termo { get; set; } = string.Empty;
    public string? Explicacao { get; set; }
    public string? Categoria { get; set; }
    public string? ExemploUso { get; set; }
}

public class ManejoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public string? Descricao { get; set; }
    public string? DicaPratica { get; set; }
    public string? FaixaEtaria { get; set; }
    public string? NivelSuporte { get; set; }
    public bool ValidadoPorEspecialista { get; set; }
    public string? Autor { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class ServicoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Especialidade { get; set; }
    public string? TipoAtendimento { get; set; }
    public string? Descricao { get; set; }
    public string? Endereco { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? ValorConsulta { get; set; }
    public bool AtendeOnline { get; set; }
    public bool Verificado { get; set; }
}

public class EscolaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? TipoRede { get; set; }
    public string? Endereco { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public bool PossuiAEE { get; set; }
    public bool PossuiSalaRecursos { get; set; }
    public int? AvaliacaoInclusao { get; set; }
}

public class OportunidadeDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Tipo { get; set; }
    public string? Empresa { get; set; }
    public string? Descricao { get; set; }
    public string? Requisitos { get; set; }
    public string? Beneficios { get; set; }
    public string? LocalTrabalho { get; set; }
    public string? Salario { get; set; }
    public string? LinkInscricao { get; set; }
    public DateTime? DataLimite { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class BuscaApiResultado
{
    public string Query { get; set; } = string.Empty;
    public int TotalResultados { get; set; }
    public List<GlossarioDto> Glossario { get; set; } = new();
    public List<ManejoDto> Manejos { get; set; } = new();
    public List<ServicoDto> Servicos { get; set; } = new();
}

#endregion
