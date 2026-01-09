# AUTistima - Instruções para Agentes de IA

## Visão Geral e Arquitetura
**Stack:** ASP.NET Core 9 MVC + EF Core 9 + SQL Server
**Idioma:** Português (pt-BR) obrigatório em código, comentários, UI e commits.

O projeto é uma plataforma monolítica MVC organizada em **Areas** para separar contextos de usuários (`Admin`, `Empresa`, `Governo`, `Mae`, `Profissional`).
- **Identity Centralizado:** `ApplicationUser` estende `IdentityUser` com campos específicos (CPF, CNPJ, TipoPerfil).
- **Real-time:** SignalR (`ChatHub`) para comunicação instantânea.
- **PWA First:** Foco em mobile, com Service Workers e manifesto configurados.

## Workflow de Desenvolvimento
- **Executar Projeto:** Use `./testar.sh [porta]` (padrão 5000).
  - *Não use `dotnet run` diretamente* se houver conflito de portas; o script gerencia processos zumbis.
- **Banco de Dados:**
  - Criar migration: `dotnet ef migrations add NomeDaMigration` (na pasta `AUTistima/`).
  - *Não aplicar manualmente:* O `Program.cs` aplica migrations pendentes automaticamente no startup.
- **Testes:** Teste fluxos críticos (Login, Cadastro, Chat) manualmente ou via scripts de teste se disponíveis.

## Padrões de Código e Convenções

### Autenticação e Autorização
- **Roles vs Perfil:** O sistema usa `TipoPerfil` (enum) em vez de Roles do Identity para lógica de negócios.
- **Validação:** Em Controllers, use métodos auxiliares como `IsMae()`, `IsProfissional()` para verificar acesso.
  ```csharp
  if (!await IsMae()) return RedirectToAction("AcessoNegado", "Account");
  ```

### Banco de Dados (EF Core)
- **Schema:** Todo o banco reside no schema `autistima_sa_sql`.
- **Soft Delete:** Entidades devem ter propriedade `Ativo`. Nunca delete fisicamente registros de usuários/histórico.
- **Deleção:** Configure `DeleteBehavior.Restrict` ou `SetNull`. **Proibido Cascade Delete** para evitar perda de dados acidental.
- **Campos Padrão:** `DataCriacao` (UTC), `UserId` (FK para ApplicationUser).

### Serviços e Injeção de Dependência
- **IA e Análise:**
  - `AIService`: Implementação básica para sugestões e conteúdo.
  - `SentimentService`: Singleton para análise de texto.
- **Notificações:**
  - `PushNotificationService`: Integração WebPush (VAPID).
  - `ActivityTrackingService`: Registre ações críticas do usuário (`RegistrarAtividadeComContexto`).
- **Estatísticas:** `StatisticsService` para agregação de dados de uso.

### UI/UX (Frontend)
- **Framework:** Bootstrap 5.
- **Paleta de Cores:**
  - Primária (Salmão): `#F28B82`
  - Secundária (Azul): `#AECBFA`
- **Feedback:** Use `TempData["Mensagem"]` com emojis para feedback ao usuário (ex: "✅ Salvo com sucesso!").

## Estrutura de Arquivos
- `AUTistima/Areas/{Area}/Controllers/`: Controllers específicos de perfil.
- `AUTistima/Data/ApplicationDbContext.cs`: Configuração central do EF e DbSets.
- `AUTistima/Services/`: Lógica de negócios complexa e integrações.
- `AUTistima/wwwroot/`: Assets estáticos e Service Worker.

## Contatos e Configuração
- **Admin Padrão:** `lorena@autistima.app.br` (criado no seed).
- **Segurança:** Valide sempre o `TipoPerfil` no backend, não confie apenas na UI.
