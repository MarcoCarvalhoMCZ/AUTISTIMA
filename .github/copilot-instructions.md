# AUTistima - Instruções para Agentes de IA

## Visão Geral
**Stack:** ASP.NET Core 9 MVC + EF Core 9 + SQL Server | **Propósito:** Rede de apoio para mães atípicas
**Idioma:** pt-BR obrigatório em código, comentários, UI e mensagens.

## Execução e Build
- **Rodar:** `./testar.sh [porta]` (padrão 5000). Mata processos na porta antes de rodar.
- **Migrations:** `dotnet ef migrations add Nome` (em `AUTistima/`). **Não** rode `database update`; `Program.cs` aplica no startup.

## Arquitetura e Padrões
- **Autenticação:** Identity com regras relaxadas. Use `TipoPerfil` enum (não Roles).
  - Valide em Controllers: `private async Task<bool> Is{Perfil}()`. Ex: `if (!await IsMae()) return RedirectToAction(...)`.
- **Banco de Dados:** Schema `autistima_sa_sql`.
  - **Deleção:** `DeleteBehavior.Restrict` ou `SetNull`. **Nunca Cascade**.
  - **Entidades:** `Ativo` (soft delete), `DataCriacao` (UTC), `UserId` (FK).
- **Serviços:**
  - `AIService`: Interface `IAIService` (sugestões). Implementação `BasicAIService`.
  - `PushNotificationService`: WebPush (VAPID hardcoded).
  - `ActivityTrackingService`: Use `RegistrarAtividadeComContexto` para ações críticas.
- **UI/UX:** Bootstrap. Cores: `#F28B82` (Salmon), `#AECBFA` (Azul). Feedback via `TempData["Mensagem"]` com emojis.

## Workflow de Desenvolvimento
1. **Model:** Crie em `Models/` com `Ativo`, `UserId`. Adicione `DbSet` em `ApplicationDbContext`.
2. **Migration:** Gere sem aplicar.
3. **Controller:** Crie em `Areas/{Area}/Controllers/` com validação de perfil.
4. **Views:** Use padrões visuais e textos empáticos.

## Avisos
- **Admin:** `lorena@autistima.app.br` (fixo no startup).
- **PWA:** Projeto PWA-first (`service-worker.js`). Teste offline.
- **Segurança:** Valide `TipoPerfil` manualmente.

