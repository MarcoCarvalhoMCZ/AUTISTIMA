using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AUTistima.Services;

/// <summary>
/// Interface para serviço de rastreamento de atividades
/// </summary>
public interface IActivityTrackingService
{
    Task RegistrarAtividade(string userId, TipoAtividade tipo, string? entidade = null, int? entidadeId = null, string? detalhes = null);
    Task RegistrarAtividadeComContexto(string userId, TipoAtividade tipo, HttpContext httpContext, string? entidade = null, int? entidadeId = null, string? detalhes = null);
    Task<List<UserActivity>> ObterAtividadesRecentes(int quantidade = 50);
    Task<List<UserActivity>> ObterAtividadesUsuario(string userId, DateTime? dataInicio = null, DateTime? dataFim = null);
}

/// <summary>
/// Interface para serviço de estatísticas
/// </summary>
public interface IStatisticsService
{
    Task<DashboardMetrics> ObterMetricasDashboard();
    Task<EngagementMetrics> ObterMetricasEngajamento(DateTime? dataInicio = null, DateTime? dataFim = null);
    Task<UserMetrics> ObterMetricasUsuarios(DateTime? dataInicio = null, DateTime? dataFim = null);
    Task<ContentMetrics> ObterMetricasConteudo(DateTime? dataInicio = null, DateTime? dataFim = null);
    Task<TriagemMetrics> ObterMetricasTriagens(DateTime? dataInicio = null, DateTime? dataFim = null);
    Task GerarSnapshotDiario();
    Task<List<StatisticSnapshot>> ObterHistoricoSnapshots(int dias = 30);
    Task<List<ChartDataPoint>> ObterDadosGraficoTemporal(string metrica, int dias = 30);
}

/// <summary>
/// Serviço de rastreamento de atividades dos usuários
/// </summary>
public class ActivityTrackingService : IActivityTrackingService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ActivityTrackingService> _logger;

    public ActivityTrackingService(ApplicationDbContext context, ILogger<ActivityTrackingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task RegistrarAtividade(string userId, TipoAtividade tipo, string? entidade = null, int? entidadeId = null, string? detalhes = null)
    {
        try
        {
            var atividade = new UserActivity
            {
                UserId = userId,
                TipoAtividade = tipo,
                EntidadeRelacionada = entidade,
                EntidadeId = entidadeId,
                Detalhes = detalhes,
                DataHora = DateTime.UtcNow
            };

            _context.UserActivities.Add(atividade);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar atividade: {TipoAtividade} para usuário {UserId}", tipo, userId);
        }
    }

    public async Task RegistrarAtividadeComContexto(string userId, TipoAtividade tipo, HttpContext httpContext, string? entidade = null, int? entidadeId = null, string? detalhes = null)
    {
        try
        {
            var userAgent = httpContext.Request.Headers.UserAgent.ToString();
            var dispositivo = DetectarDispositivo(userAgent);

            var atividade = new UserActivity
            {
                UserId = userId,
                TipoAtividade = tipo,
                EntidadeRelacionada = entidade,
                EntidadeId = entidadeId,
                Detalhes = detalhes,
                EnderecoIP = httpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = userAgent.Length > 500 ? userAgent[..500] : userAgent,
                Dispositivo = dispositivo,
                SessaoId = httpContext.Session?.Id,
                DataHora = DateTime.UtcNow
            };

            _context.UserActivities.Add(atividade);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar atividade com contexto: {TipoAtividade} para usuário {UserId}", tipo, userId);
        }
    }

    private static TipoDispositivo DetectarDispositivo(string userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return TipoDispositivo.Web;

        userAgent = userAgent.ToLower();

        if (userAgent.Contains("android") && userAgent.Contains("wv"))
            return TipoDispositivo.PWAMobile;
        if (userAgent.Contains("iphone") || userAgent.Contains("ipad"))
            return userAgent.Contains("safari") ? TipoDispositivo.iOS : TipoDispositivo.PWAMobile;
        if (userAgent.Contains("mobile"))
            return TipoDispositivo.PWAMobile;
        if (userAgent.Contains("electron") || userAgent.Contains("pwa"))
            return TipoDispositivo.PWADesktop;

        return TipoDispositivo.Web;
    }

    public async Task<List<UserActivity>> ObterAtividadesRecentes(int quantidade = 50)
    {
        return await _context.UserActivities
            .Include(a => a.Usuario)
            .OrderByDescending(a => a.DataHora)
            .Take(quantidade)
            .ToListAsync();
    }

    public async Task<List<UserActivity>> ObterAtividadesUsuario(string userId, DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var query = _context.UserActivities
            .Where(a => a.UserId == userId);

        if (dataInicio.HasValue)
            query = query.Where(a => a.DataHora >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(a => a.DataHora <= dataFim.Value);

        return await query
            .OrderByDescending(a => a.DataHora)
            .ToListAsync();
    }
}

/// <summary>
/// Serviço de estatísticas e métricas
/// </summary>
public class StatisticsService : IStatisticsService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StatisticsService> _logger;

    public StatisticsService(ApplicationDbContext context, ILogger<StatisticsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DashboardMetrics> ObterMetricasDashboard()
    {
        var hoje = DateTime.UtcNow.Date;
        var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
        var inicioSemana = hoje.AddDays(-(int)hoje.DayOfWeek);

        return new DashboardMetrics
        {
            // Totais
            TotalUsuarios = await _context.Users.CountAsync(),
            TotalMaes = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.Mae),
            TotalProfissionaisSaude = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.ProfissionalSaude),
            TotalProfissionaisEducacao = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.ProfissionalEducacao),
            TotalEmpresas = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.Empresa),
            TotalGoverno = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.Governo),
            
            // Novos este mês
            NovosUsuariosMes = await _context.Users.CountAsync(u => u.DataCadastro >= inicioMes),
            NovosUsuariosSemana = await _context.Users.CountAsync(u => u.DataCadastro >= inicioSemana),
            NovosUsuariosHoje = await _context.Users.CountAsync(u => u.DataCadastro >= hoje),
            
            // Conteúdo
            TotalPosts = await _context.Posts.CountAsync(p => p.Ativo),
            TotalManejos = await _context.Manejos.CountAsync(m => m.Ativo),
            TotalOportunidades = await _context.Opportunities.CountAsync(o => o.Ativo),
            TotalTriagens = await _context.ScreeningRequests.CountAsync(),
            
            // Engajamento
            TotalAcolhimentos = await _context.PostAcolhimentos.CountAsync(),
            TotalComentarios = await _context.PostComments.CountAsync(c => c.Ativo),
            MensagensHoje = await _context.ChatMessages.CountAsync(m => m.DataEnvio >= hoje),
            
            // Estrutura
            TotalEscolas = await _context.Schools.CountAsync(),
            TotalServicos = await _context.Services.CountAsync(),
            TotalFilhosCadastrados = await _context.Children.CountAsync()
        };
    }

    public async Task<EngagementMetrics> ObterMetricasEngajamento(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var inicio = dataInicio ?? DateTime.UtcNow.AddDays(-30);
        var fim = dataFim ?? DateTime.UtcNow;

        // Usuários que fizeram alguma atividade no período
        var usuariosAtivos = await _context.UserActivities
            .Where(a => a.DataHora >= inicio && a.DataHora <= fim)
            .Select(a => a.UserId)
            .Distinct()
            .CountAsync();

        var totalUsuarios = await _context.Users.CountAsync(u => u.Ativo);

        // Posts e interações no período
        var postsNoPeriodo = await _context.Posts.CountAsync(p => p.DataCriacao >= inicio && p.DataCriacao <= fim && p.Ativo);
        var acolhimentosNoPeriodo = await _context.PostAcolhimentos.CountAsync(a => a.DataAcolhimento >= inicio && a.DataAcolhimento <= fim);
        var comentariosNoPeriodo = await _context.PostComments.CountAsync(c => c.DataCriacao >= inicio && c.DataCriacao <= fim && c.Ativo);
        var manejosNoPeriodo = await _context.Manejos.CountAsync(m => m.DataCriacao >= inicio && m.DataCriacao <= fim && m.Ativo);
        var mensagensNoPeriodo = await _context.ChatMessages.CountAsync(m => m.DataEnvio >= inicio && m.DataEnvio <= fim);

        // Atividades por tipo
        var atividadesPorTipo = await _context.UserActivities
            .Where(a => a.DataHora >= inicio && a.DataHora <= fim)
            .GroupBy(a => a.TipoAtividade)
            .Select(g => new { Tipo = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Tipo, x => x.Total);

        // Engajamento por perfil
        var engajamentoPorPerfil = await _context.UserActivities
            .Where(a => a.DataHora >= inicio && a.DataHora <= fim)
            .Join(_context.Users, a => a.UserId, u => u.Id, (a, u) => new { a, u.TipoPerfil })
            .GroupBy(x => x.TipoPerfil)
            .Select(g => new { Perfil = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Perfil, x => x.Total);

        return new EngagementMetrics
        {
            PeriodoInicio = inicio,
            PeriodoFim = fim,
            UsuariosAtivos = usuariosAtivos,
            TaxaEngajamento = totalUsuarios > 0 ? Math.Round((decimal)usuariosAtivos / totalUsuarios * 100, 2) : 0,
            PostsCriados = postsNoPeriodo,
            TotalAcolhimentos = acolhimentosNoPeriodo,
            TotalComentarios = comentariosNoPeriodo,
            ManejosCompartilhados = manejosNoPeriodo,
            MensagensEnviadas = mensagensNoPeriodo,
            MediaAcolhimentosPorPost = postsNoPeriodo > 0 ? Math.Round((decimal)acolhimentosNoPeriodo / postsNoPeriodo, 2) : 0,
            MediaComentariosPorPost = postsNoPeriodo > 0 ? Math.Round((decimal)comentariosNoPeriodo / postsNoPeriodo, 2) : 0,
            AtividadesPorTipo = atividadesPorTipo,
            EngajamentoPorPerfil = engajamentoPorPerfil
        };
    }

    public async Task<UserMetrics> ObterMetricasUsuarios(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var inicio = dataInicio ?? DateTime.UtcNow.AddDays(-30);
        var fim = dataFim ?? DateTime.UtcNow;

        var novosPorPerfil = await _context.Users
            .Where(u => u.DataCadastro >= inicio && u.DataCadastro <= fim)
            .GroupBy(u => u.TipoPerfil)
            .Select(g => new { Perfil = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Perfil, x => x.Total);

        var novosPorDia = await _context.Users
            .Where(u => u.DataCadastro >= inicio && u.DataCadastro <= fim)
            .GroupBy(u => u.DataCadastro.Date)
            .Select(g => new ChartDataPoint { Label = g.Key.ToString("dd/MM"), Value = g.Count() })
            .OrderBy(x => x.Label)
            .ToListAsync();

        var novosPorCidade = await _context.Users
            .Where(u => u.DataCadastro >= inicio && u.DataCadastro <= fim && u.Cidade != null)
            .GroupBy(u => u.Cidade)
            .Select(g => new { Cidade = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total)
            .Take(10)
            .ToDictionaryAsync(x => x.Cidade!, x => x.Total);

        // Logins por dia (se rastreado)
        var loginsPorDia = await _context.UserActivities
            .Where(a => a.TipoAtividade == TipoAtividade.Login && a.DataHora >= inicio && a.DataHora <= fim)
            .GroupBy(a => a.DataHora.Date)
            .Select(g => new ChartDataPoint { Label = g.Key.ToString("dd/MM"), Value = g.Count() })
            .OrderBy(x => x.Label)
            .ToListAsync();

        // Dispositivos
        var dispositivosUsados = await _context.UserActivities
            .Where(a => a.DataHora >= inicio && a.DataHora <= fim)
            .GroupBy(a => a.Dispositivo)
            .Select(g => new { Dispositivo = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Dispositivo, x => x.Total);

        return new UserMetrics
        {
            PeriodoInicio = inicio,
            PeriodoFim = fim,
            TotalNovosUsuarios = await _context.Users.CountAsync(u => u.DataCadastro >= inicio && u.DataCadastro <= fim),
            NovosPorPerfil = novosPorPerfil,
            NovosPorDia = novosPorDia,
            NovosPorCidade = novosPorCidade,
            LoginsPorDia = loginsPorDia,
            DispositivosUsados = dispositivosUsados
        };
    }

    public async Task<ContentMetrics> ObterMetricasConteudo(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var inicio = dataInicio ?? DateTime.UtcNow.AddDays(-30);
        var fim = dataFim ?? DateTime.UtcNow;

        // Posts mais acolhidos
        var postsMaisAcolhidos = await _context.Posts
            .Where(p => p.Ativo && p.DataCriacao >= inicio && p.DataCriacao <= fim)
            .Select(p => new PostResumo
            {
                Id = p.Id,
                ConteudoResumido = p.Conteudo.Length > 100 ? p.Conteudo.Substring(0, 100) + "..." : p.Conteudo,
                Autor = p.Autor!.NomeCompleto,
                TotalAcolhimentos = p.Acolhimentos.Count,
                TotalComentarios = p.Comentarios.Count(c => c.Ativo),
                DataCriacao = p.DataCriacao
            })
            .OrderByDescending(p => p.TotalAcolhimentos)
            .Take(10)
            .ToListAsync();

        // Manejos por categoria
        var manejosPorCategoria = await _context.Manejos
            .Where(m => m.Ativo)
            .GroupBy(m => m.Categoria)
            .Select(g => new { Categoria = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Categoria, x => x.Total);

        // Manejos validados vs não validados
        var manejosValidados = await _context.Manejos.CountAsync(m => m.Ativo && m.ValidadoPorEspecialista);
        var manejosNaoValidados = await _context.Manejos.CountAsync(m => m.Ativo && !m.ValidadoPorEspecialista);

        // Oportunidades por tipo
        var oportunidadesPorTipo = await _context.Opportunities
            .Where(o => o.Ativo)
            .GroupBy(o => o.Tipo)
            .Select(g => new { Tipo = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Tipo, x => x.Total);

        return new ContentMetrics
        {
            PeriodoInicio = inicio,
            PeriodoFim = fim,
            TotalPostsNoPeriodo = await _context.Posts.CountAsync(p => p.Ativo && p.DataCriacao >= inicio && p.DataCriacao <= fim),
            TotalManejosNoPeriodo = await _context.Manejos.CountAsync(m => m.Ativo && m.DataCriacao >= inicio && m.DataCriacao <= fim),
            TotalOportunidadesNoPeriodo = await _context.Opportunities.CountAsync(o => o.Ativo && o.DataCriacao >= inicio && o.DataCriacao <= fim),
            PostsMaisAcolhidos = postsMaisAcolhidos,
            ManejosPorCategoria = manejosPorCategoria,
            ManejosValidados = manejosValidados,
            ManejosNaoValidados = manejosNaoValidados,
            OportunidadesPorTipo = oportunidadesPorTipo
        };
    }

    public async Task<TriagemMetrics> ObterMetricasTriagens(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var inicio = dataInicio ?? DateTime.UtcNow.AddDays(-30);
        var fim = dataFim ?? DateTime.UtcNow;

        var triagensPorStatus = await _context.ScreeningRequests
            .GroupBy(t => t.Status)
            .Select(g => new { Status = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Status, x => x.Total);

        var triagensPorMes = await _context.ScreeningRequests
            .Where(t => t.DataSolicitacao >= inicio && t.DataSolicitacao <= fim)
            .GroupBy(t => new { t.DataSolicitacao.Year, t.DataSolicitacao.Month })
            .Select(g => new ChartDataPoint
            {
                Label = $"{g.Key.Month:D2}/{g.Key.Year}",
                Value = g.Count()
            })
            .OrderBy(x => x.Label)
            .ToListAsync();

        var escolasComMaisTriagens = await _context.ScreeningRequests
            .Where(t => t.DataSolicitacao >= inicio && t.DataSolicitacao <= fim)
            .GroupBy(t => t.Escola!.Nome)
            .Select(g => new { Escola = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total)
            .Take(10)
            .ToDictionaryAsync(x => x.Escola, x => x.Total);

        // Tempo médio de conclusão
        var triagensConcluidasComTempo = await _context.ScreeningRequests
            .Where(t => t.Status == StatusTriagem.Concluido && t.DataConclusao.HasValue)
            .Select(t => new { t.DataSolicitacao, DataConclusao = t.DataConclusao!.Value })
            .ToListAsync();

        var tempoMedioConclusao = triagensConcluidasComTempo.Any()
            ? triagensConcluidasComTempo.Average(t => (t.DataConclusao - t.DataSolicitacao).TotalDays)
            : 0;

        return new TriagemMetrics
        {
            PeriodoInicio = inicio,
            PeriodoFim = fim,
            TotalTriagensNoPeriodo = await _context.ScreeningRequests.CountAsync(t => t.DataSolicitacao >= inicio && t.DataSolicitacao <= fim),
            TriagensPorStatus = triagensPorStatus,
            TriagensPorMes = triagensPorMes,
            EscolasComMaisTriagens = escolasComMaisTriagens,
            TempoMedioConclusaoDias = Math.Round(tempoMedioConclusao, 1)
        };
    }

    public async Task GerarSnapshotDiario()
    {
        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
        
        // Verificar se já existe snapshot para hoje
        var existente = await _context.StatisticSnapshots.AnyAsync(s => s.Data == hoje);
        if (existente) return;

        var ontem = DateTime.UtcNow.Date.AddDays(-1);
        var inicioHoje = DateTime.UtcNow.Date;

        var snapshot = new StatisticSnapshot
        {
            Data = hoje,
            
            // Totais
            TotalUsuarios = await _context.Users.CountAsync(),
            NovosUsuarios = await _context.Users.CountAsync(u => u.DataCadastro >= ontem && u.DataCadastro < inicioHoje),
            UsuariosAtivos = await _context.UserActivities
                .Where(a => a.DataHora >= ontem && a.DataHora < inicioHoje)
                .Select(a => a.UserId)
                .Distinct()
                .CountAsync(),
            
            // Por perfil
            TotalMaes = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.Mae),
            TotalProfissionaisSaude = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.ProfissionalSaude),
            TotalProfissionaisEducacao = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.ProfissionalEducacao),
            TotalEmpresas = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.Empresa),
            TotalGoverno = await _context.Users.CountAsync(u => u.TipoPerfil == TipoPerfil.Governo),
            
            // Engajamento
            PostsCriados = await _context.Posts.CountAsync(p => p.DataCriacao >= ontem && p.DataCriacao < inicioHoje && p.Ativo),
            Comentarios = await _context.PostComments.CountAsync(c => c.DataCriacao >= ontem && c.DataCriacao < inicioHoje && c.Ativo),
            Acolhimentos = await _context.PostAcolhimentos.CountAsync(a => a.DataAcolhimento >= ontem && a.DataAcolhimento < inicioHoje),
            ManejosCompartilhados = await _context.Manejos.CountAsync(m => m.DataCriacao >= ontem && m.DataCriacao < inicioHoje && m.Ativo),
            ManejosValidados = await _context.Manejos.CountAsync(m => m.DataAtualizacao >= ontem && m.DataAtualizacao < inicioHoje && m.ValidadoPorEspecialista),
            
            // Triagens
            TriagensSolicitadas = await _context.ScreeningRequests.CountAsync(t => t.DataSolicitacao >= ontem && t.DataSolicitacao < inicioHoje),
            TriagensConcluidas = await _context.ScreeningRequests.CountAsync(t => t.DataConclusao >= ontem && t.DataConclusao < inicioHoje && t.Status == StatusTriagem.Concluido),
            
            // Oportunidades
            OportunidadesPublicadas = await _context.Opportunities.CountAsync(o => o.DataCriacao >= ontem && o.DataCriacao < inicioHoje && o.Ativo),
            
            // Atividades
            TotalLogins = await _context.UserActivities.CountAsync(a => a.TipoAtividade == TipoAtividade.Login && a.DataHora >= ontem && a.DataHora < inicioHoje),
            VisualizacoesPagina = await _context.UserActivities.CountAsync(a => a.TipoAtividade == TipoAtividade.VisualizacaoPagina && a.DataHora >= ontem && a.DataHora < inicioHoje),
            BuscasRealizadas = await _context.UserActivities.CountAsync(a => a.TipoAtividade == TipoAtividade.Busca && a.DataHora >= ontem && a.DataHora < inicioHoje),
            MensagensChat = await _context.ChatMessages.CountAsync(m => m.DataEnvio >= ontem && m.DataEnvio < inicioHoje),
            
            DataGeracao = DateTime.UtcNow
        };

        // Calcular métricas
        var totalUsuarios = snapshot.TotalUsuarios;
        var usuariosAtivos = snapshot.UsuariosAtivos;
        snapshot.TaxaEngajamento = totalUsuarios > 0 ? Math.Round((decimal)usuariosAtivos / totalUsuarios * 100, 2) : 0;
        snapshot.MediaAcolhimentosPorPost = snapshot.PostsCriados > 0 ? Math.Round((decimal)snapshot.Acolhimentos / snapshot.PostsCriados, 2) : 0;

        _context.StatisticSnapshots.Add(snapshot);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Snapshot de estatísticas gerado para {Data}", hoje);
    }

    public async Task<List<StatisticSnapshot>> ObterHistoricoSnapshots(int dias = 30)
    {
        var dataLimite = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-dias));
        return await _context.StatisticSnapshots
            .Where(s => s.Data >= dataLimite)
            .OrderBy(s => s.Data)
            .ToListAsync();
    }

    public async Task<List<ChartDataPoint>> ObterDadosGraficoTemporal(string metrica, int dias = 30)
    {
        var snapshots = await ObterHistoricoSnapshots(dias);

        return snapshots.Select(s => new ChartDataPoint
        {
            Label = s.Data.ToString("dd/MM"),
            Value = metrica.ToLower() switch
            {
                "usuarios" => s.TotalUsuarios,
                "novos" => s.NovosUsuarios,
                "ativos" => s.UsuariosAtivos,
                "posts" => s.PostsCriados,
                "acolhimentos" => s.Acolhimentos,
                "comentarios" => s.Comentarios,
                "manejos" => s.ManejosCompartilhados,
                "triagens" => s.TriagensSolicitadas,
                "logins" => s.TotalLogins,
                "mensagens" => s.MensagensChat,
                "engajamento" => (int)s.TaxaEngajamento,
                _ => 0
            }
        }).ToList();
    }
}

#region DTOs para métricas

public class DashboardMetrics
{
    public int TotalUsuarios { get; set; }
    public int TotalMaes { get; set; }
    public int TotalProfissionaisSaude { get; set; }
    public int TotalProfissionaisEducacao { get; set; }
    public int TotalEmpresas { get; set; }
    public int TotalGoverno { get; set; }
    public int NovosUsuariosMes { get; set; }
    public int NovosUsuariosSemana { get; set; }
    public int NovosUsuariosHoje { get; set; }
    public int TotalPosts { get; set; }
    public int TotalManejos { get; set; }
    public int TotalOportunidades { get; set; }
    public int TotalTriagens { get; set; }
    public int TotalAcolhimentos { get; set; }
    public int TotalComentarios { get; set; }
    public int MensagensHoje { get; set; }
    public int TotalEscolas { get; set; }
    public int TotalServicos { get; set; }
    public int TotalFilhosCadastrados { get; set; }
}

public class EngagementMetrics
{
    public DateTime PeriodoInicio { get; set; }
    public DateTime PeriodoFim { get; set; }
    public int UsuariosAtivos { get; set; }
    public decimal TaxaEngajamento { get; set; }
    public int PostsCriados { get; set; }
    public int TotalAcolhimentos { get; set; }
    public int TotalComentarios { get; set; }
    public int ManejosCompartilhados { get; set; }
    public int MensagensEnviadas { get; set; }
    public decimal MediaAcolhimentosPorPost { get; set; }
    public decimal MediaComentariosPorPost { get; set; }
    public Dictionary<TipoAtividade, int> AtividadesPorTipo { get; set; } = new();
    public Dictionary<TipoPerfil, int> EngajamentoPorPerfil { get; set; } = new();
}

public class UserMetrics
{
    public DateTime PeriodoInicio { get; set; }
    public DateTime PeriodoFim { get; set; }
    public int TotalNovosUsuarios { get; set; }
    public Dictionary<TipoPerfil, int> NovosPorPerfil { get; set; } = new();
    public List<ChartDataPoint> NovosPorDia { get; set; } = new();
    public Dictionary<string, int> NovosPorCidade { get; set; } = new();
    public List<ChartDataPoint> LoginsPorDia { get; set; } = new();
    public Dictionary<TipoDispositivo, int> DispositivosUsados { get; set; } = new();
}

public class ContentMetrics
{
    public DateTime PeriodoInicio { get; set; }
    public DateTime PeriodoFim { get; set; }
    public int TotalPostsNoPeriodo { get; set; }
    public int TotalManejosNoPeriodo { get; set; }
    public int TotalOportunidadesNoPeriodo { get; set; }
    public List<PostResumo> PostsMaisAcolhidos { get; set; } = new();
    public Dictionary<CategoriaManejo, int> ManejosPorCategoria { get; set; } = new();
    public int ManejosValidados { get; set; }
    public int ManejosNaoValidados { get; set; }
    public Dictionary<TipoOportunidade, int> OportunidadesPorTipo { get; set; } = new();
}

public class TriagemMetrics
{
    public DateTime PeriodoInicio { get; set; }
    public DateTime PeriodoFim { get; set; }
    public int TotalTriagensNoPeriodo { get; set; }
    public Dictionary<StatusTriagem, int> TriagensPorStatus { get; set; } = new();
    public List<ChartDataPoint> TriagensPorMes { get; set; } = new();
    public Dictionary<string, int> EscolasComMaisTriagens { get; set; } = new();
    public double TempoMedioConclusaoDias { get; set; }
}

public class PostResumo
{
    public int Id { get; set; }
    public string ConteudoResumido { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public int TotalAcolhimentos { get; set; }
    public int TotalComentarios { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class ChartDataPoint
{
    public string Label { get; set; } = string.Empty;
    public int Value { get; set; }
}

#endregion
