using Microsoft.EntityFrameworkCore;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;

namespace AUTistima.Services;

/// <summary>
/// Serviço de IA para sugestões inteligentes
/// Preparado para integração futura com Azure OpenAI ou outros provedores
/// </summary>
public interface IAIService
{
    Task<List<Manejo>> SugerirManejosPorCategoria(CategoriaManejo categoria, int quantidade = 5);
    Task<List<GlossaryTerm>> SugerirTermosRelacionados(string termo, int quantidade = 5);
    Task<List<Service>> SugerirProfissionais(string cidade, Especialidade? especialidade = null, int quantidade = 5);
    Task<string> GerarResumoAcolhedor(string conteudo);
    Task<List<string>> SugerirTagsParaPost(string titulo, string conteudo);
}

/// <summary>
/// Implementação básica do serviço de IA usando regras simples
/// Em produção, substituir por integração com Azure OpenAI, OpenAI API, etc.
/// </summary>
public class BasicAIService : IAIService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BasicAIService> _logger;

    public BasicAIService(ApplicationDbContext context, ILogger<BasicAIService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Sugere manejos baseados na categoria
    /// </summary>
    public async Task<List<Manejo>> SugerirManejosPorCategoria(CategoriaManejo categoria, int quantidade = 5)
    {
        return await _context.Manejos
            .Where(m => m.Ativo && m.Categoria == categoria)
            .OrderByDescending(m => m.ValidadoPorEspecialista)
            .ThenByDescending(m => m.DataCriacao)
            .Take(quantidade)
            .ToListAsync();
    }

    /// <summary>
    /// Sugere termos relacionados do glossário
    /// </summary>
    public async Task<List<GlossaryTerm>> SugerirTermosRelacionados(string termo, int quantidade = 5)
    {
        var termoLower = termo.ToLower();
        
        // Primeiro, busca o termo original para pegar a categoria
        var termoOriginal = await _context.GlossaryTerms
            .FirstOrDefaultAsync(g => g.TermoTecnico.ToLower().Contains(termoLower));
        
        if (termoOriginal == null)
        {
            // Se não encontrou, retorna os mais recentes
            return await _context.GlossaryTerms
                .OrderBy(g => Guid.NewGuid()) // Aleatoriza
                .Take(quantidade)
                .ToListAsync();
        }
        
        // Busca termos da mesma categoria
        return await _context.GlossaryTerms
            .Where(g => g.Categoria == termoOriginal.Categoria && g.Id != termoOriginal.Id)
            .OrderBy(g => Guid.NewGuid())
            .Take(quantidade)
            .ToListAsync();
    }

    /// <summary>
    /// Sugere profissionais baseados em localização e especialidade
    /// </summary>
    public async Task<List<Service>> SugerirProfissionais(string cidade, Especialidade? especialidade = null, int quantidade = 5)
    {
        var query = _context.Services.Where(s => s.Ativo);
        
        if (!string.IsNullOrWhiteSpace(cidade))
        {
            query = query.Where(s => s.Cidade != null && s.Cidade.ToLower().Contains(cidade.ToLower()));
        }
        
        if (especialidade.HasValue)
        {
            query = query.Where(s => s.Especialidade == especialidade.Value);
        }
        
        return await query
            .OrderByDescending(s => s.Verificado)
            .ThenBy(s => s.TipoAtendimento) // Prioriza gratuitos
            .Take(quantidade)
            .ToListAsync();
    }

    /// <summary>
    /// Gera um resumo acolhedor do conteúdo
    /// Em produção, usar IA generativa para criar resumos empáticos
    /// </summary>
    public Task<string> GerarResumoAcolhedor(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
            return Task.FromResult("💕 Você não está sozinha nessa jornada.");
        
        // Versão simplificada - em produção usar IA
        var palavrasChave = new Dictionary<string, string>
        {
            { "crise", "Momentos de crise são difíceis, mas passam. Respire fundo. 💜" },
            { "meltdown", "Meltdowns são intensos, mas lembre-se: seu filho não está fazendo isso de propósito. 🫂" },
            { "escola", "A inclusão escolar é um direito! Você está certa em buscar o melhor para seu filho. 📚" },
            { "diagnóstico", "O diagnóstico é o começo de uma jornada de descobertas, não um fim. 🌟" },
            { "sozinha", "Você NÃO está sozinha. Estamos aqui com você. 💕" },
            { "cansada", "Está tudo bem estar cansada. Cuidar de quem cuida também é importante. 🤗" },
            { "medo", "O medo faz parte, mas você é mais forte do que imagina. 💪" },
            { "feliz", "Celebre cada conquista, por menor que pareça! 🎉" },
            { "alimentação", "Seletividade alimentar é comum e tem manejo. Paciência e amor! 🍎" },
            { "sono", "Noites difíceis passam. Busque ajuda se precisar. 🌙" }
        };
        
        var conteudoLower = conteudo.ToLower();
        foreach (var kv in palavrasChave)
        {
            if (conteudoLower.Contains(kv.Key))
            {
                return Task.FromResult(kv.Value);
            }
        }
        
        return Task.FromResult("💕 Cada dia é uma nova oportunidade. Você está fazendo um ótimo trabalho!");
    }

    /// <summary>
    /// Sugere tags para um post baseado no conteúdo
    /// </summary>
    public Task<List<string>> SugerirTagsParaPost(string titulo, string conteudo)
    {
        var tags = new HashSet<string>();
        var texto = (titulo + " " + conteudo).ToLower();
        
        var tagsPossiveis = new Dictionary<string, List<string>>
        {
            { "escola", new List<string> { "escola", "educação", "inclusão", "professor", "sala de aula", "aee" } },
            { "saúde", new List<string> { "médico", "terapia", "fono", "psicólogo", "caps", "tratamento" } },
            { "comportamento", new List<string> { "crise", "meltdown", "birra", "agressivo", "ansiedade" } },
            { "alimentação", new List<string> { "comer", "alimentação", "seletivo", "comida", "refeição" } },
            { "sono", new List<string> { "dormir", "sono", "noite", "insônia", "acordar" } },
            { "comunicação", new List<string> { "fala", "comunicação", "não fala", "pecs", "caa" } },
            { "sensorial", new List<string> { "sensorial", "barulho", "luz", "textura", "sensibilidade" } },
            { "direitos", new List<string> { "direito", "lei", "benefício", "bpc", "ciptea" } },
            { "família", new List<string> { "família", "marido", "avó", "irmão", "parente" } },
            { "apoio", new List<string> { "ajuda", "apoio", "suporte", "desabafo", "sozinha" } }
        };
        
        foreach (var categoria in tagsPossiveis)
        {
            if (categoria.Value.Any(p => texto.Contains(p)))
            {
                tags.Add(categoria.Key);
            }
        }
        
        // Garante pelo menos uma tag
        if (!tags.Any())
        {
            tags.Add("geral");
        }
        
        return Task.FromResult(tags.Take(5).ToList());
    }
}

/// <summary>
/// Extensões para registrar o serviço de IA
/// </summary>
public static class AIServiceExtensions
{
    public static IServiceCollection AddAIServices(this IServiceCollection services)
    {
        services.AddScoped<IAIService, BasicAIService>();
        return services;
    }
}
