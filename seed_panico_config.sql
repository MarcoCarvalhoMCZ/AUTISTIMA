-- Script para adicionar configurações do Botão de Pânico
-- Execute este script após aplicar a migration AddPanicAlertSystem

USE [autistima];
GO

-- Configuração: Número do WhatsApp para botão de pânico
IF NOT EXISTS (SELECT 1 FROM [autistima_sa_sql].[SystemConfiguration] WHERE Chave = 'WHATSAPP_NUMERO_PANICO')
BEGIN
    INSERT INTO [autistima_sa_sql].[SystemConfiguration] 
    (Chave, Valor, Descricao, Categoria, DadoSensivel, Ativo, DataCriacao)
    VALUES 
    ('WHATSAPP_NUMERO_PANICO', '5582999999999', 'Número do WhatsApp para atendimento de emergência (formato: DDI+DDD+número)', 'WhatsApp', 0, 1, GETUTCDATE());
    
    PRINT '✅ Configuração WHATSAPP_NUMERO_PANICO criada!';
END
GO

-- Configuração: Habilitar botão de pânico
IF NOT EXISTS (SELECT 1 FROM [autistima_sa_sql].[SystemConfiguration] WHERE Chave = 'PANICO_HABILITADO')
BEGIN
    INSERT INTO [autistima_sa_sql].[SystemConfiguration] 
    (Chave, Valor, Descricao, Categoria, DadoSensivel, Ativo, DataCriacao)
    VALUES 
    ('PANICO_HABILITADO', 'true', 'Define se o botão de pânico está habilitado', 'WhatsApp', 0, 1, GETUTCDATE());
    
    PRINT '✅ Configuração PANICO_HABILITADO criada!';
END
GO

-- Configuração: Mensagem padrão
IF NOT EXISTS (SELECT 1 FROM [autistima_sa_sql].[SystemConfiguration] WHERE Chave = 'WHATSAPP_MENSAGEM_PADRAO')
BEGIN
    INSERT INTO [autistima_sa_sql].[SystemConfiguration] 
    (Chave, Valor, Descricao, Categoria, DadoSensivel, Ativo, DataCriacao)
    VALUES 
    ('WHATSAPP_MENSAGEM_PADRAO', 'Preciso de apoio urgente!', 'Mensagem padrão do WhatsApp', 'WhatsApp', 0, 1, GETUTCDATE());
    
    PRINT '✅ Configuração WHATSAPP_MENSAGEM_PADRAO criada!';
END
GO

PRINT '📱 Acesse Admin > Configurações > WhatsApp para alterar o número';
