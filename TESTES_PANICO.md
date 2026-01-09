# 🧪 Guia de Testes - Botão de Pânico

## ✅ Checklist de Testes

### 1️⃣ **Testes de Autenticação & Autorização**

- [ ] Usuário NÃO autenticado: Botão SOS não aparece
- [ ] Usuário MAE: Botão SOS aparece na navbar
- [ ] Usuário PROFISSIONAL: Botão SOS NÃO aparece
- [ ] Usuário ADMIN: Botão SOS NÃO aparece
- [ ] Usuário EMPRESA: Botão SOS NÃO aparece
- [ ] Usuário GOVERNO: Botão SOS NÃO aparece

### 2️⃣ **Testes da Interface**

- [ ] Botão tem cor vermelha (danger)
- [ ] Botão tem animação de pulse contínua
- [ ] Em desktop: mostra texto "SOS"
- [ ] Em mobile: mostra apenas ícone
- [ ] Posição correta na navbar (direita, antes de notificações)

### 3️⃣ **Testes do Modal de Confirmação**

- [ ] Modal aparece ao clicar no botão
- [ ] Modal tem aviso crítico
- [ ] Campo de descrição tem contador de caracteres
- [ ] Campo de nível de urgência tem 4 opções:
  - [ ] 🟢 Normal
  - [ ] 🟠 Moderado
  - [ ] 🔴 Crítico (padrão)
  - [ ] ⚫ Emergência
- [ ] Checkbox de confirmação é obrigatório
- [ ] Botão "Confirmar e Abrir WhatsApp" é destacado

### 4️⃣ **Testes de Validação**

- [ ] Descrição em branco → erro "Campo obrigatório"
- [ ] Descrição com > 500 caracteres → truncado/erro
- [ ] Sem marcar checkbox → erro "Confirmação necessária"
- [ ] Modal limpa ao fechar e reabrir

### 5️⃣ **Testes da Criação de Alerta**

- [ ] Alerta é criado no banco de dados
- [ ] Atividade é registrada em UserActivity
- [ ] Status inicial é "Ativo"
- [ ] DataCriacao é UTC
- [ ] Confirmado começa como False
- [ ] LinkWhatsApp é gerado corretamente

### 6️⃣ **Testes de Geração de Link WhatsApp**

```
Número testado: 551199999999
Descrição testada: "Meu filho está em crise"
```

- [ ] Link contém wa.me
- [ ] Link contém número correto
- [ ] Link contém descrição encoded em URL
- [ ] Mensagem começa com "🆘 *ALERTA DE PÂNICO*"
- [ ] Descrição está no corpo da mensagem
- [ ] Link pode ser testado em browser

**Teste manual:**
```
1. Acionar alerta com descrição "Teste de pânico"
2. Clicar "Confirmar e Abrir WhatsApp"
3. URL aberta deve ser algo como:
   https://wa.me/551199999999?text=🆘%20*ALERTA%20DE%20PÂNICO*...
4. Se WhatsApp instalado: abre conversa
5. Se não: mostra página web do WhatsApp
```

### 7️⃣ **Testes do Histórico**

- [ ] Página `/Panico/Historico` carrega
- [ ] Lista todos os alertas da mãe
- [ ] Ordenação por data (mais recente primeiro)
- [ ] Mostra card para cada alerta com:
  - [ ] ID do alerta
  - [ ] Status (com badge colorida)
  - [ ] Nível de urgência (com ícone)
  - [ ] Descrição
  - [ ] Data de criação
  - [ ] Botão "Ver Detalhes"
- [ ] Resumo estatístico (Ativos, Atendidos, Resolvidos, Total)
- [ ] Timeline visual de ações

### 8️⃣ **Testes de Detalhes**

- [ ] Página `/Panico/Detalhes/:id` carrega
- [ ] Mostra informações completas do alerta
- [ ] Timeline com todas as ações:
  - [ ] Alerta Acionado
  - [ ] Confirmado (se sim)
  - [ ] Atendido (se sim)
- [ ] Mostra link WhatsApp (se confirmado)
- [ ] Links de navegação funcionam
- [ ] Breadcrumb correto

### 9️⃣ **Testes do Dashboard (Admin)**

**Acesso:** Apenas ADMIN e PROFISSIONAL DE SAÚDE

- [ ] Página `/Panico/Dashboard` carrega (admin)
- [ ] Página `/Panico/Dashboard` carrega (profissional)
- [ ] Página `/Panico/Dashboard` nega acesso (mae/empresa)
- [ ] Mostra métricas:
  - [ ] Alertas Ativos
  - [ ] Alertas Atendidos
  - [ ] Alertas Resolvidos
  - [ ] Total de Alertas
- [ ] Tabela com todos os alertas (não apenas da mãe)
- [ ] Ordenação por urgência
- [ ] Modal para responder alerta
- [ ] Campo "Nota de Atendimento"
- [ ] Botão "Marcar como Atendido"
- [ ] Alertas desaparecem depois de atendidos (redirecionado para Atendidos)

### 🔟 **Testes de Banco de Dados**

```sql
-- Verificar tabela criada
SELECT * FROM [autistima_sa_sql].[PanicAlerts];

-- Verificar índices
SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('[autistima_sa_sql].[PanicAlerts]');

-- Contar alertas por status
SELECT [Status], COUNT(*) as Total 
FROM [autistima_sa_sql].[PanicAlerts]
GROUP BY [Status];

-- Alertas em última hora
SELECT * FROM [autistima_sa_sql].[PanicAlerts]
WHERE [DataCriacao] > DATEADD(HOUR, -1, GETUTCDATE())
ORDER BY [DataCriacao] DESC;
```

### 1️⃣1️⃣ **Testes de Performance**

- [ ] Criação de alerta < 500ms
- [ ] Carregamento histórico com 100 alertas < 1s
- [ ] Dashboard com 50 alertas ativos < 2s
- [ ] Não há N+1 queries (verificar SQL Profiler)

### 1️⃣2️⃣ **Testes de Segurança**

- [ ] CSRF token validado
- [ ] Usuário MAE não consegue ver alertas de outra mãe
- [ ] Usuário MAE não consegue chamar `/Panico/Dashboard`
- [ ] SQL Injection: Descrição com `'; DROP TABLE...` é escapada
- [ ] XSS: Caracteres especiais `<script>` são escapados
- [ ] Número WhatsApp não é visível no HTML (apenas em JS)

### 1️⃣3️⃣ **Testes de Acessibilidade**

- [ ] Botão tem atributo `aria-label`
- [ ] Modal tem role="dialog"
- [ ] Textos têm contraste suficiente
- [ ] Teclado consegue navegar (Tab)
- [ ] Leitores de tela entendem conteúdo

### 1️⃣4️⃣ **Testes em Diferentes Navegadores**

- [ ] Chrome/Edge (latest)
- [ ] Firefox (latest)
- [ ] Safari (macOS)
- [ ] Safari (iOS)
- [ ] Chrome (Android)

### 1️⃣5️⃣ **Testes de Responsividade**

- [ ] Desktop (1920px)
- [ ] Tablet (768px)
- [ ] Mobile (360px)
- [ ] Modal responsivo em mobile

---

## 🧑‍💻 Dados de Teste

### Usuária Mãe de Teste
```
Email: mae@autistima.test
Senha: Mae@2025
Nome: Mãe de Teste
CPF: 12345678901
Perfil: Mae
```

### Criar no banco (SQL):
```sql
-- Já existe automaticamente seed de admin
-- Para criar mae de teste, use:
INSERT INTO [autistima_sa_sql].[AspNetUsers] (
    [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail],
    [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
    [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd],
    [LockoutEnabled], [AccessFailedCount], [NomeCompleto], [TipoPerfil],
    [DataNascimento], [CPF], [TermoConsentimentoAceito], [DataCadastro], [Ativo]
)
VALUES (
    NEWID(),
    'mae@autistima.test',
    'MAE@AUTISTIMA.TEST',
    'mae@autistima.test',
    'MAE@AUTISTIMA.TEST',
    1,
    'hash_aqui', -- Use UserManager para criar
    NEWID(),
    NEWID(),
    NULL, 0, 0, NULL, 1, 0,
    'Mãe de Teste',
    1, -- TipoPerfil.Mae
    '1990-01-01',
    '12345678901',
    1,
    GETUTCDATE(),
    1
);
```

---

## 📊 Scripts de Teste Recomendados

### Testar Criação de Múltiplos Alertas
```sql
-- Criar 5 alertas de teste
DECLARE @UserId NVARCHAR(450) = (SELECT TOP 1 Id FROM [autistima_sa_sql].[Users] WHERE [TipoPerfil] = 1);
DECLARE @i INT = 1;

WHILE @i <= 5
BEGIN
    INSERT INTO [autistima_sa_sql].[PanicAlerts] (
        [UserId], [Descricao], [NivelUrgencia], [Status], [Confirmado], 
        [DataCriacao], [Ativo]
    ) VALUES (
        @UserId,
        'Alerta de teste #' + CAST(@i AS NVARCHAR),
        ABS(CHECKSUM(NEWID())) % 4 + 1,
        0,
        0,
        GETUTCDATE(),
        1
    );
    SET @i = @i + 1;
END
```

### Testar Performance
```sql
-- Tempo de execução
SET STATISTICS TIME ON;

SELECT * FROM [autistima_sa_sql].[PanicAlerts]
WHERE [UserId] = (SELECT TOP 1 Id FROM [autistima_sa_sql].[Users] WHERE [TipoPerfil] = 1)
ORDER BY [DataCriacao] DESC;

SET STATISTICS TIME OFF;
```

---

## 🔍 Verificação de Logs

### Onde ver logs de alerta:
1. **Visual Studio**: Janela Output
2. **Event Viewer**: Windows Logs > Application
3. **Application Insights**: se configurado
4. **appsettings.json**: verificar log level

### Procurar por:
```
⚠️ ALERTA DE PÂNICO criado
✅ ALERTA DE PÂNICO ID confirmado
```

---

## ❌ Testes de Erro

- [ ] Alerta sem número WhatsApp configurado → mensagem de erro clara
- [ ] Banco de dados offline → erro 500 apropriado
- [ ] Conexão perdida durante confirmação → retry automático
- [ ] Usuário deletado mas ainda tem sessão → erro apropriado

---

## 📋 Resultado Final

Após passar por todos os testes, marque como ✅:

- [ ] Interface
- [ ] Backend
- [ ] Banco de Dados
- [ ] Segurança
- [ ] Performance
- [ ] Acessibilidade
- [ ] Documentação
- [ ] Produção

**Assinado por:** __________________ **Data:** __/__/____
