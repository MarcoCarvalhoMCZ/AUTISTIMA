# ⚡ QUICK START - Botão de Pânico

## 🚀 5 Passos para Colocar em Funcionamento

### 1️⃣ Criar Migration (2 minutos)

```bash
cd /Users/marcocarvalho/SistemasIA/AUTistima/AUTistima

# Criar migration
dotnet ef migrations add AddPanicAlertSystem

# Aplicar ao banco
dotnet ef database update
```

### 2️⃣ Configurar WhatsApp (1 minuto)

Execute no SQL Server Management Studio:

```sql
USE bd_autistima;

-- Seu número: substitua 551199999999
INSERT INTO [autistima_sa_sql].[SystemConfiguration] 
(Chave, Valor, Descricao, Categoria, DadoSensivel, Ativo, DataCriacao)
VALUES
(
    N'WHATSAPP_NUMERO_PANICO',
    N'551199999999',  -- MUDE PARA SEU NÚMERO!
    N'Número do WhatsApp para receber alertas de pânico',
    N'WhatsApp',
    0,
    1,
    GETUTCDATE()
);
```

### 3️⃣ Reiniciar Aplicação (1 minuto)

```bash
# Parar (se rodando)
# Ctrl + C

# Rodar novamente
./testar.sh 5000
```

### 4️⃣ Testar (2 minutos)

1. Abrir `http://localhost:5000`
2. Login como **mãe**: `lorena@autistima.app.br` / `Lorena@2025`
   - OU crie uma mãe de teste via admin
3. Na navbar, procurar botão **[SOS]** (vermelho, pulsando)
4. Clicar no botão
5. Preencher formulário:
   - Descrição: "Teste de pânico"
   - Urgência: "Crítico"
   - Confirmar checkbox
6. Clicar "Confirmar e Abrir WhatsApp"
7. Deve abrir WhatsApp (ou página web se não instalado)

### 5️⃣ Monitorar (Admin)

1. Login como admin: `diretoria@sosdados.com.br` / `Lorena@2025`
2. Ir para `/Panico/Dashboard`
3. Ver alertas críticos
4. Testar ação "Marcar como Atendido"

---

## 🎯 Arquivos Principais (Abra para Entender)

```
📁 Implementação Core
├─ Models/PanicAlert.cs          [Entidade do banco]
├─ Services/PanicService.cs      [Lógica de negócios]
└─ Controllers/PanicoController.cs [Endpoints HTTP]

📁 Interface (Razor)
├─ Views/Panico/Index.cshtml     [Página com botão SOS]
├─ Views/Panico/Historico.cshtml [Histórico de alertas]
├─ Views/Panico/Detalhes.cshtml  [Detalhes de alerta]
└─ Views/Panico/Dashboard.cshtml [Admin monitor]

📁 Configuração
├─ Program.cs                    [Registra serviço]
├─ Data/ApplicationDbContext.cs  [DbSet + modelBuilder]
└─ Views/Shared/_Layout.cshtml   [Botão na navbar]

📁 Documentação
├─ PANICO_RESUMO_VISUAL.md       [Este arquivo!]
├─ IMPLEMENTACAO_PANICO.md       [Completo + detalhes]
├─ TESTES_PANICO.md              [Checklist de QA]
└─ seed_panico_config.sql        [Seed do WhatsApp]
```

---

## 🔥 URLs Principais

| URL | Quem Acessa | O quê |
|-----|------------|------|
| `/Panico/Index` | Mães | Botão SOS + Modal |
| `/Panico/Historico` | Mães | Histórico pessoal |
| `/Panico/Detalhes/42` | Mães | Detalhes de alerta #42 |
| `/Panico/Dashboard` | Admin/Prof. Saúde | Painel de monitoramento |

---

## ⚙️ Configurações Importantes

### Em `appsettings.json`

Não precisa configurar nada! Tudo vem de banco de dados.

Mas se quiser adicionar em futuro:

```json
{
  "PanicAlert": {
    "Habilitado": true,
    "TempoEntreAlertasMinutos": 5,
    "MaxDescricaoChars": 500
  }
}
```

### Em SQL Server (SystemConfiguration)

```sql
SELECT * FROM [autistima_sa_sql].[SystemConfiguration]
WHERE Chave LIKE '%PANICO%' OR Chave LIKE '%WHATSAPP%';
```

---

## 🆘 Troubleshooting Rápido

| Problema | Solução |
|----------|---------|
| Botão não aparece | Verificar se TipoPerfil = 1 (Mae) |
| WhatsApp não abre | Verificar número em SystemConfiguration |
| Erro 500 ao acionar | Checar logs, migration foi aplicada? |
| Modal não funciona | Limpar cache, F12 > aba Console |
| Número inválido | Formato deve ser: 55 + DDD + 9 + 8 dígitos |

---

## 📈 O que Monitorar

Depois de colocar em produção:

```sql
-- Quantos alertas por dia?
SELECT CAST(DataCriacao AS DATE) as Data, COUNT(*) as Total
FROM [autistima_sa_sql].[PanicAlerts]
GROUP BY CAST(DataCriacao AS DATE)
ORDER BY Data DESC;

-- Que nível de urgência é mais comum?
SELECT NivelUrgencia, COUNT(*) as Total
FROM [autistima_sa_sql].[PanicAlerts]
GROUP BY NivelUrgencia;

-- Tempo médio de atendimento?
SELECT 
    AVG(DATEDIFF(MINUTE, DataCriacao, DataAtendimento)) as MinutosAteAtendimento
FROM [autistima_sa_sql].[PanicAlerts]
WHERE DataAtendimento IS NOT NULL;
```

---

## 📱 Teste em Mobile

1. Abrir em celular (ou DevTools mobile)
2. Procurar botão SOS (aparece como ícone só)
3. Modal deve ser responsivo
4. WhatsApp deve abrir app (se instalado) ou web

---

## 🎓 Exemplos Rápidos

### Criar Alerta via API (cURL)

```bash
curl -X POST http://localhost:5000/Panico/AcionarAlerta \
  -H "Content-Type: application/json" \
  -H "X-CSRF-TOKEN: {token}" \
  -d '{
    "descricao": "Meu filho está em crise",
    "nivelUrgencia": 3
  }'
```

### Confirmar Alerta via API

```bash
curl -X POST http://localhost:5000/Panico/ConfirmarAlerta \
  -H "Content-Type: application/json" \
  -H "X-CSRF-TOKEN: {token}" \
  -d '{"panicAlertId": 1}'
```

### Buscar Histórico (SQL)

```sql
SELECT TOP 10 
    Id, Descricao, NivelUrgencia, Status, DataCriacao
FROM [autistima_sa_sql].[PanicAlerts]
WHERE UserId = '{user_id}'
ORDER BY DataCriacao DESC;
```

---

## 🎬 Demo em 30 Segundos

1. **Mãe aciona**: Clica botão SOS → descreve situação → confirma
2. **Sistema responde**: Cria alerta + valida + gera link WhatsApp
3. **WhatsApp abre**: Conversa é iniciada automaticamente
4. **Admin monitora**: Vê dashboard → marca como atendido
5. **Mãe vê histórico**: Acompanha status do alerta

---

## 💝 Mensagem para Mães

```
O botão SOS é seu botão de emergência!

Sempre que precisar:
✅ Estiver em crise
✅ Ansiedade insuportável
✅ Seu filho em surto sensorial
✅ Precisar conversar com profissional AGORA

NÃO HESITE. CLIQUE. NÓS ESTAMOS AQUI.

Você não está sozinha. 💙
```

---

## ✅ Checklist Final

- [ ] Migration criada e aplicada
- [ ] Número WhatsApp configurado
- [ ] Aplicação reiniciada
- [ ] Botão aparece para mães
- [ ] Modal funciona
- [ ] WhatsApp abre
- [ ] Dashboard mostra alertas
- [ ] Admin consegue responder
- [ ] Histórico persiste

---

## 📞 Suporte

**Documentação completa**: `IMPLEMENTACAO_PANICO.md`  
**Todos os testes**: `TESTES_PANICO.md`  
**Resumo visual**: `PANICO_RESUMO_VISUAL.md`

---

**Pronto para colocar em produção! Boa sorte! 🚀**
