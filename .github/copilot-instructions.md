# AUTistima - Instruções para Agentes de IA

## Visão Geral
Rede de apoio digital para **mães atípicas** (mães de pessoas autistas). Stack: ASP.NET Core 9.0 MVC + SQL Server + EF Core 9.0 (schema: `autistima_sa_sql`).  
**Idioma obrigatório**: pt-BR em TODO código, mensagens, labels, comentários e nomes de variáveis.

## Comandos Essenciais
```bash
./testar.sh [porta]       # Executa projeto (libera porta, padrão 5000)
cd AUTistima && dotnet ef migrations add NomeMigration  # Nova migration
```
> **Migrations aplicadas automaticamente** no startup (`Program.cs`). Não rodar `dotnet ef database update` manualmente.  
> Admin padrão: `lorena@autistima.app.br` / `Lorena@2025`

## Arquitetura: Autorização por TipoPerfil (NÃO usa ASP.NET Roles)
O sistema usa enum `TipoPerfil` para autorização. **Verificação manual obrigatória** em cada controller de área:
```csharp
// Areas/Admin/Controllers/*.cs - padrão obrigatório
private async Task<bool> IsAdmin() {
    var user = await _userManager.GetUserAsync(User);
    return user?.TipoPerfil == TipoPerfil.Administrador;
}
// Chamar no início de CADA action: if (!await IsAdmin()) return RedirectToAction("Index", "Home", new { area = "" });
```

| Área | Perfis | Enum |
|------|--------|------|
| `/Admin/*` | Administrador | `TipoPerfil.Administrador` (0) |
| `/Mae/*` | Mães atípicas | `TipoPerfil.Mae` (1) |
| `/Profissional/*` | Saúde/Educação | `ProfissionalSaude` (2), `ProfissionalEducacao` (3) |
| `/Empresa/*` | Empresas parceiras | `TipoPerfil.Empresa` (4) |
| `/Governo/*` | Administração pública | `TipoPerfil.Governo` (5) |

## Padrões de Controller
```csharp
// Injeção OBRIGATÓRIA: ApplicationDbContext, UserManager<ApplicationUser>, ILogger<T>
// Obter usuário: User.FindFirstValue(ClaimTypes.NameIdentifier)
// Feedback: TempData["Mensagem"] (sucesso) ou TempData["Erro"] (erro)

[HttpPost, ValidateAntiForgeryToken, Authorize]
public async Task<IActionResult> Create([Bind("Campo1,Campo2")] Entidade item) {
    ModelState.Remove("UserId");  // SEMPRE remover campos definidos no servidor
    ModelState.Remove("Autor");   // Remover propriedades de navegação também
    
    item.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    item.DataCriacao = DateTime.UtcNow;
    item.Ativo = true;
    
    if (ModelState.IsValid) {
        _context.Add(item);
        await _context.SaveChangesAsync();
        
        // Rastrear atividade (se aplicável)
        // await _activityService.RegistrarAtividade(item.UserId, TipoAtividade.CriacaoPost, "Post", item.Id);

        TempData["Mensagem"] = "Registro salvo com carinho! 💕";
        return RedirectToAction(nameof(Index));
    }
    return View(item);
}
```

## Padrões de Model
```csharp
public class ExemploModel {
    [Key] public int Id { get; set; }
    
    [Required(ErrorMessage = "Campo obrigatório")]
    [StringLength(200)]
    [Display(Name = "Nome do Campo")]  // Labels em português
    public string Campo { get; set; } = string.Empty;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public bool Ativo { get; set; } = true;  // Soft delete - NUNCA deletar fisicamente
    
    // FK obrigatória para autor
    [Required] public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")] public virtual ApplicationUser? Autor { get; set; }
}
```

### DbContext - Relacionamentos (`OnModelCreating`)
- `DeleteBehavior.Restrict`: FK obrigatória (não permite excluir pai com filhos)
- `DeleteBehavior.SetNull`: FK opcional (define null ao excluir pai)
- `DeleteBehavior.Cascade`: Exclui filhos junto (usar com moderação)

## Conceitos de Domínio Críticos
| Termo | Significado |
|-------|-------------|
| **Manejos** | "Saberes não cientificizados" - estratégias práticas das mães (NÃO prescrições médicas). Validáveis por profissionais. Categorias: `CategoriaManejo` enum. |
| **Central de Acolhimento** | Feed social onde posts são "acolhidos" (não "curtidos"). Controller: `AcolhimentoController.cs`. |
| **Acolhimento** | Toggle like/unlike empático - modelo `PostAcolhimento` com índice único `(PostId, UserId)`. |
| **Triagem** | Solicitação de avaliação (`ScreeningRequest`) por professor → aguardando profissional de saúde. |
| **Chat** | Mensagens diretas entre usuários (`ChatController`). |

### Fluxo de Notificações
```csharp
// Criar notificação + push: usar método estático
await NotificacoesController.CriarNotificacao(
    _context, userId, "💕 Título", "Mensagem", TipoNotificacao.Acolhimento, "/link", _pushService);
```
- `IPushNotificationService` injetado para push PWA (WebPush com chaves VAPID)
- Tipos: `TipoNotificacao` enum (Acolhimento, Comentario, Sistema, etc.)

### Entidades Principais (ver `ApplicationDbContext.cs`)
- `ApplicationUser` → `Child`, `Post`, `Manejo`, `Service`, `Opportunity`, `Notification`, `UserActivity`
- `Post` → `PostComment`, `PostAcolhimento`
- `School` → `Child`, `ScreeningRequest`
- `Conversation` → `ChatMessage`

## Serviços Registrados (`Program.cs`)
- `IAIService` / `BasicAIService`: Sugestões de manejos, termos do glossário, profissionais
- `IPushNotificationService`: Notificações push para PWA
- `IActivityTrackingService`: Rastreamento de atividades do usuário (`RegistrarAtividade`)
- `IStatisticsService`: Métricas e dashboards (`ObterMetricasDashboard`)
- Registro: `builder.Services.AddAIServices()` (extension method em `Services/AIService.cs`)

## Monitoramento e Métricas (Aderência)
Use `IStatisticsService` para dashboards e relatórios de aderência. **NÃO** faça contagens manuais no controller.
```csharp
// Exemplo: Dashboard Admin
var metricas = await _statisticsService.ObterMetricasDashboard();
var engajamento = await _statisticsService.ObterMetricasEngajamento(); // Inclui Taxa de Aderência
```
- **Aderência**: Calculada via `EngagementMetrics.TaxaEngajamento` (usuários ativos / total).
- **Snapshots**: O sistema gera snapshots diários automáticos para histórico.

## Rastreamento de Atividades
Sempre que uma ação relevante ocorrer (criar post, login, acolhimento), registrar via `IActivityTrackingService`:
```csharp
await _activityService.RegistrarAtividade(userId, TipoAtividade.Login);
// Ou com contexto HTTP (IP, UserAgent)
await _activityService.RegistrarAtividadeComContexto(userId, TipoAtividade.Login, HttpContext);
```

## UI/UX - Tom e Visual
**Tom**: Acolhedor, empático, SEMPRE usar emojis:
```csharp
TempData["Mensagem"] = "Sua mensagem foi compartilhada com carinho. Você não está sozinha! 💕";
TempData["Erro"] = "Ops! Algo deu errado. Tente novamente. 🤗";
```

**Paleta** (ver `wwwroot/css/site.css`):
- Primária (Salmon): `#F28B82` → `btn-salmon`, `text-salmon`, `bg-salmon-light`
- Secundária (Azul bebê): `#AECBFA`
- Destaque (Amarelo): `#FCE883`
- Alto contraste: fundo branco, texto preto

**Ícones**: Bootstrap Icons (`bi bi-*`) - ex: `bi-heart-fill`, `bi-chat-heart`, `bi-people-fill`

## Arquivos de Referência
| Para entender... | Consulte |
|------------------|----------|
| DI, Identity, Startup, migrations auto | `Program.cs` |
| Schema completo + relacionamentos | `Data/ApplicationDbContext.cs` |
| Extensão do Identity | `Models/ApplicationUser.cs` |
| Perfis de usuário | `Models/Enums/TipoPerfil.cs` |
| Categorias de manejo | `Models/Enums/CategoriaManejo.cs` |
| Padrão CRUD público | `Controllers/AcolhimentoController.cs` |
| Padrão Admin com verificação | `Areas/Admin/Controllers/AdminController.cs` |
| Serviços (IA, Push, Stats) | `Services/AIService.cs`, `Services/PushNotificationService.cs`, `Services/StatisticsService.cs` |
| CSS e variáveis | `wwwroot/css/site.css` |

## Estrutura de Áreas (MVC Areas)
```
Areas/
├── Admin/Controllers/     # Administração (dashboard, CRUD completo)
├── Empresa/Controllers/   # Portal de empresas parceiras
├── Governo/Controllers/   # Portal governo/secretarias
├── Mae/Controllers/       # Área exclusiva mães (em desenvolvimento)
└── Profissional/Controllers/  # Portal profissionais saúde/educação
```

## Dicas Rápidas
1. **Nova entidade**: Criar Model → Adicionar DbSet → Configurar em `OnModelCreating` → Migration
2. **Seed de dados**: Adicionar em métodos `SeedXxx()` no `ApplicationDbContext.cs` (existentes: `SeedGlossaryTerms`, `SeedServicesCapsMaceio`, `SeedManejosIniciais`)
3. **Validação falhou?**: Verificar `ModelState.Remove()` para campos server-side
4. **Notificações**: Usar `NotificacoesController.CriarNotificacao()` + `IPushNotificationService`
5. **Atividades**: Registrar ações importantes com `IActivityTrackingService`

## Fluxo de Triagem (Professor → Profissional Saúde)
```
ProfissionalEducacao cria ScreeningRequest → Status: Pendente
   ↓
ProfissionalSaude avalia → Status: EmAvaliacao → adiciona ParecerProfissional
   ↓
Conclusão → Status: Concluida → adiciona Recomendacoes e Encaminhamento
```
- `StatusTriagem` enum: `Pendente`, `EmAvaliacao`, `Concluida`, `Cancelada`
- Ver `Areas/Profissional/Controllers/ProfissionalController.cs`

## PWA - Progressive Web App
O sistema é um PWA completo com suporte offline:
- **Manifest**: `wwwroot/manifest.json` - cores tema `#F28B82`, ícones em `wwwroot/icons/`
- **Service Worker**: `wwwroot/service-worker.js` - cache `CACHE_VERSION = 'v1.0.0'` (incrementar a cada deploy)
- **Offline**: `wwwroot/offline.html` - página exibida sem conexão
- **Push**: Chaves VAPID em `Services/PushNotificationService.cs`

## ViewModels (usar para formulários complexos)
```csharp
// ViewModels/ - usar quando formulário difere do Model
// Exemplos existentes: LoginViewModel, RegisterViewModel, ProfileViewModel
public class RegisterViewModel {
    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [Display(Name = "Nome Completo")]
    public string NomeCompleto { get; set; } = string.Empty;
    // ... campos específicos do formulário
}
```
