# AUTistima - Instruções para Agentes de IA

## Panorama Rápido
**Stack:** ASP.NET Core 9 MVC + EF Core 9 + SQL Server | **Propósito:** Rede de apoio para mães atípicas
- **Linguagem:** pt-BR obrigatório em código, comentários, UI e mensagens de feedback
- **Execução:** `./testar.sh [porta]` (padrão 5000) mata processos na porta e executa `dotnet run --urls http://localhost:porta`
- **Bootstrap:** `Program.cs` aplica migrations automaticamente; cria admin `lorena@autistima.app.br` — **nunca remova**

## Segurança & Autenticação
- **Autorização:** `TipoPerfil` enum (`Models/Enums/TipoPerfil.cs`): Administrador, Mae, ProfissionalSaude, ProfissionalEducacao, Empresa, Governo
  - ❌ Nunca use `Roles` padrão do Identity
  - ✅ Valide `TipoPerfil` manualmente: `var user = await _userManager.GetUserAsync(User); if (user?.TipoPerfil != TipoPerfil.Mae) return RedirectToAction(...)`
- **Áreas:** `/Admin`, `/Mae`, `/Profissional`, `/Empresa`, `/Governo` — nova feature deve criar controller em `Areas/{Area}/Controllers/` com `[Area("Area")]` attribute
- **Padrão em Controllers de Área:** implementar método `private async Task<bool> Is{Perfil}()` para validação (ver `Areas/Mae/Controllers/MaeController.cs`)

## Banco de Dados & Modelagem
- **Context:** `ApplicationDbContext` com schema `autistima_sa_sql` (em `Data/ApplicationDbContext.cs`)
- **Cascatas:** configure sempre `DeleteBehavior.Restrict` (erro ao tentar deletar) ou `SetNull` (permite deletar, anula FK); **nunca use Cascade**
  - Exemplo: `HasOne(e => e.Autor).WithMany(u => u.Manejos).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);`
- **Padrão de Modelagem (em `Models/`):**
  - `Ativo: bool` (soft delete — sempre filtrar `Ativo == true`)
  - `DataCriacao: DateTime` (UTC)
  - `UserId: string` (FK para `ApplicationUser`)
  - Índices para filtros frequentes: `.HasIndex(e => e.UserId)`, `.HasIndex(e => e.DataCriacao)`
- **Migrations:** `cd AUTistima && dotnet ef migrations add NomeMigracao` — **não execute `database update`**; `Program.cs` faz `context.Database.Migrate()` no startup
- **Seeds:** apenas em métodos privados dentro de `OnModelCreating` (exemplos: `SeedGlossaryTerms`, `SeedServicesCapsMaceio`)

## Convenções de Código
- **Validação de Modelo:** em Create/Edit, remova campos preenchidos no backend:
  ```csharp
  ModelState.Remove("UserId");
  ModelState.Remove("DataCriacao");
  ```
- **Feedback ao Usuário:** sempre use `TempData["Mensagem"]` com emoji (ex.: `"Salvo com sucesso! 💕"`, `"Compartilhado! 🎉"`)
- **Filtragem:** soft-delete significa sempre incluir `.Where(x => x.Ativo)` em queries (ver `StatisticsService`, `ManejosController`)
- **UI:** Bootstrap com cores `#F28B82` (salmon, acolhimento) e `#AECBFA` (azul, informação)

## Serviços Críticos & Integrações
- **AIService** (`Services/AIService.cs`): interface `IAIService` com assinaturas prontas (SugerirManejos, SugerirTermos, SugerirProfissionais, GerarResumo, SugerirTags)
  - Implementação atual: `BasicAIService` (regras simples)
  - **Mantenha assinaturas** para futura integração Azure OpenAI
  - Registre via `builder.Services.AddAIServices()` em `Program.cs`
- **PushNotificationService** (`Services/PushNotificationService.cs`): WebPush + VAPID fixo
  - Métodos retornam `int` (número de envios)
  - Chame `LimparSubscriptionsInativasAsync` quando houver muitos erros 404/410
- **StatisticsService** (`Services/StatisticsService.cs`) + **ActivityTrackingService**: registram atividades (`UserActivity`) para dashboards
  - Ao criar features significativas, invoque `RegistrarAtividadeComContexto(userId, TipoAtividade.X, HttpContext, ...)`
  - Exponha métricas em DTOs: `DashboardMetrics`, `EngagementMetrics`, `UserMetrics`, `ContentMetrics`, `TriagemMetrics`

## Fluxos Funcionais Principais
| Entidade | Padrão | Detalhe |
|----------|--------|--------|
| `Manejo` | Ativo + UserId + Auditoria | "Saberes não cientificizados" — dicas de mães; validáveis por especialista |
| `Post` + `PostComment` + `PostAcolhimento` | Ativo + Comunidade | Suporte entre mães; acolhimentos rastreados |
| `ScreeningRequest` | Status + Índices | Conecta escolas, professores, profissionais; evite cascatas |
| `Notification` + `ChatMessage` | Persistência + Push | Registre em `Notifications` e use `PushNotificationExtensions` se necessário push |

## Workflow para Novas Features
1. **Modelo** (`Models/`): crie entidade com `Ativo`, `DataCriacao`, `UserId`, relacionamentos via `DeleteBehavior.Restrict/SetNull`
2. **DbContext** (`Data/ApplicationDbContext.cs`): adicione `DbSet<T>`, configure fluent API, seeds se necessário
3. **Migration**: `dotnet ef migrations add NomeFeature` (sem `database update`)
4. **Controller/Área**: crie em `Areas/{Area}/Controllers/` com validação de `TipoPerfil`, feedback `TempData`
5. **Views**: Bootstrap, cores padrão, textos empáticos (pt-BR)
6. **Serviço**: atualizar `AIService`, `StatisticsService` ou `PushNotificationService` se impactado

## Diferenciais & Considerações
- **PWA-First:** `PushSubscription` + `wwwroot/service-worker.js` operação offline — teste via DevTools antes de alterar recursos estáticos
- **Telemetria:** `UserActivity` rastreia engajamento; invoque `RegistrarAtividadeComContexto` em fluxos críticos (login, chat, cadastros) para preservar métricas
- **Secrets:** VAPID keys e admin default já hardcoded; ao modificar, descreva em README e garanta que não vazem para logs
- **Documentação Código:** use XML docs (`/// <summary>`) em serviços públicos e controllers críticos
