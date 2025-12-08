# AUTistima - Instruções para Agentes de IA

## Visão Geral
Rede de apoio para mães atípicas. **Stack**: ASP.NET Core 9.0 MVC, EF Core 9.0, SQL Server.
**Idioma**: Português (pt-BR) OBRIGATÓRIO em variáveis, comentários e UI.

## Arquitetura & Segurança
- **Auth**: Baseada em `TipoPerfil` enum (NÃO usar Roles).
  - **Admin**: `TipoPerfil.Administrador` (0).
  - **Verificação**: Manual em cada Controller/Action crítica.
  - *Exemplo*: `if (user?.TipoPerfil != TipoPerfil.Administrador) return RedirectToAction("Index", "Home");`
- **Areas**: Organizado em `/Admin`, `/Mae`, `/Profissional`, `/Empresa`, `/Governo`.
- **Services**: `AIService` (Sugestões), `PushNotificationService` (PWA), `StatisticsService` (inclui `IActivityTrackingService`).

## Padrões de Código
- **Models**:
  - **Soft Delete**: `public bool Ativo { get; set; } = true;` (Nunca deletar fisicamente).
  - **Auditoria**: `DataCriacao`, `UserId` (FK para `ApplicationUser`).
  - **EF Core**: Configurar `DeleteBehavior.Restrict` em `OnModelCreating` para evitar cascatas.
- **Controllers**:
  - **Create/Edit**: Usar `ModelState.Remove("UserId")` para campos definidos no backend.
  - **Feedback**: `TempData["Mensagem"]` com emoji (ex: "Salvo com sucesso! 💕").
- **Frontend**:
  - **Estilo**: Bootstrap + Cores `#F28B82` (Salmon), `#AECBFA` (Azul).
  - **PWA**: Suporte offline via `service-worker.js`.

## Workflow & Comandos
- **Run**: `./testar.sh [porta]` (Gerencia porta e inicia app).
- **Migrations**: `cd AUTistima && dotnet ef migrations add Nome`. **Auto-apply** no startup.
- **Novas Features**:
  1. Criar Model em `Models/` (com padrões acima).
  2. Adicionar `DbSet` e config em `ApplicationDbContext.cs`.
  3. Gerar Migration (não rodar update manual).
  4. Implementar Controller na Area correta.

## Arquivos Críticos
- `Program.cs`: Configuração de DI, Identity e Auto-Migrations.
- `Data/ApplicationDbContext.cs`: Definição do Schema e Seeds.
- `Models/Enums/TipoPerfil.cs`: Níveis de acesso.
