-- =====================================================
-- Script para inserir os 29 Manejos Iniciais do AUTistima
-- Execute este script após a criação do usuário admin
-- =====================================================

-- Variável para o ID do admin (ajustar conforme necessário)
DECLARE @AdminId NVARCHAR(450)
SELECT TOP 1 @AdminId = Id FROM [autistima_sa_sql].[Users] WHERE Email = 'lorena@autistima.app.br'

IF @AdminId IS NULL
BEGIN
    PRINT 'Erro: Usuário admin não encontrado. Execute primeiro o sistema para criar o usuário.'
    RETURN
END

PRINT 'Inserindo Manejos com UserId: ' + @AdminId

-- =====================================================
-- CRISE SENSORIAL (5 manejos)
-- =====================================================
INSERT INTO [autistima_sa_sql].[Manejos] 
(Titulo, Categoria, Descricao, DicaPratica, FaixaEtariaIndicada, NivelSuporteIndicado, ValidadoPorEspecialista, UserId, DataCriacao, Ativo)
VALUES
('Cantinho do Silêncio', 5, 
'Criar um espaço seguro e calmo na casa onde a criança pode se refugiar durante momentos de sobrecarga sensorial.',
'Monte um cantinho com almofadas, cobertor pesado, luz baixa e abafadores de ruído. Deixe alguns brinquedos sensoriais preferidos. Ensine a criança a ir para lá quando sentir que está ficando sobrecarregada.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Kit de Emergência Sensorial', 5,
'Ter uma bolsa preparada com objetos sensoriais favoritos para usar em momentos de crise fora de casa.',
'Inclua na bolsa: abafadores de ruído, óculos escuros, fidgets preferidos, objeto de conforto, fone de ouvido com música calma. Mantenha sempre no carro ou na mochila.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Técnica da Pressão Profunda', 5,
'Usar pressão firme no corpo para ajudar a acalmar o sistema nervoso durante uma crise sensorial.',
'Use um cobertor pesado, abraço firme (se a criança aceitar), ou faça um "sanduíche" entre almofadas. A pressão profunda ajuda a regular o sistema sensorial.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Rotina de Descompressão', 5,
'Estabelecer um período de transição após atividades intensas para evitar sobrecarga acumulada.',
'Reserve 15-20 minutos de atividades calmas após escola, festas ou ambientes estimulantes. Pode ser ficar no quarto escuro, ouvir música suave, ou brincar com água.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Mapa Sensorial do Ambiente', 5,
'Identificar e registrar quais estímulos em cada ambiente podem causar desconforto sensorial.',
'Faça um "mapa" da casa e dos lugares frequentes identificando: luzes fortes, sons que incomodam, texturas desconfortáveis. Assim você pode se preparar ou adaptar os ambientes.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- ANSIEDADE (3 manejos)
-- =====================================================
('Antecipação Visual', 1,
'Preparar a criança para situações novas mostrando fotos e vídeos do lugar ou atividade antes.',
'Antes de ir a um lugar novo, pesquise fotos e vídeos. Mostre para a criança várias vezes nos dias anteriores. Se possível, faça uma visita prévia ao local.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Objeto de Transição', 1,
'Permitir que a criança leve um objeto favorito como âncora emocional em situações desafiadoras.',
'Escolha junto com a criança um objeto pequeno que a acalma (brinquedo, tecido, foto). Permita que ela leve em situações de ansiedade como consultas médicas ou primeiro dia de aula.',
'2-12 anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Respiração com Bolhas', 1,
'Usar bolhas de sabão para ensinar respiração profunda de forma lúdica e acalmar a ansiedade.',
'Soprar bolhas exige respiração lenta e profunda, que naturalmente acalma. Use em momentos de ansiedade ou como prevenção antes de situações estressantes.',
'2-10 anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- BANHO (3 manejos)
-- =====================================================
('Banho Gradual', 2,
'Introduzir o banho de forma gradual, respeitando a sensibilidade sensorial da criança.',
'Comece pelos pés e vá subindo devagar. Use um termômetro visual para que a criança veja a temperatura. Deixe ela controlar o chuveirinho quando possível.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Hora do Banho Musical', 2,
'Usar música para criar rotina previsível e indicar a duração do banho.',
'Crie uma playlist específica para o banho com músicas que a criança gosta. O final da playlist indica que o banho está acabando. Isso dá previsibilidade.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Produtos Sensoriais Personalizados', 2,
'Experimentar diferentes texturas e cheiros de produtos de higiene até encontrar os aceitos.',
'Teste sabonetes líquidos x em barra, com x sem cheiro, esponjas macias x buchas. Quando encontrar os preferidos, mantenha sempre os mesmos. Mudanças podem causar rejeição.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- ALIMENTAÇÃO (3 manejos)
-- =====================================================
('Prato da Descoberta', 3,
'Apresentar novos alimentos sem pressão, em pequenas porções separadas.',
'Use um prato dividido com pequenas porções separadas. Inclua sempre um alimento aceito junto com o novo. Não force - deixe a criança explorar no seu tempo.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Participação na Cozinha', 3,
'Envolver a criança no preparo dos alimentos para aumentar a aceitação.',
'Convide a criança para ajudar a lavar, cortar (com supervisão), misturar. O contato gradual com os alimentos durante o preparo aumenta a chance de aceitação na hora de comer.',
'3+ anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Mapa de Texturas Alimentares', 3,
'Registrar quais texturas de alimentos são aceitas e rejeitadas para orientar as escolhas.',
'Faça uma lista: crocante, mole, cremoso, seco, úmido. Identifique quais texturas a criança aceita e use isso para introduzir novos alimentos com texturas semelhantes às aceitas.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- SONO (3 manejos)
-- =====================================================
('Ritual do Sono Estruturado', 4,
'Criar uma sequência fixa de atividades antes de dormir para sinalizar ao corpo que é hora de descansar.',
'Estabeleça uma sequência: jantar > banho > pijama > escovar dentes > história > dormir. Sempre na mesma ordem e horário. Use um quadro visual com as etapas.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Ambiente Sensorial Noturno', 4,
'Preparar o quarto com as condições sensoriais ideais para o sono.',
'Verifique: luz (escuro ou luz noturna suave?), temperatura (nem quente nem frio), som (silêncio ou ruído branco?), roupa de cama (textura confortável). Cada criança tem preferências diferentes.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('História Personalizada', 4,
'Criar histórias com roteiro previsível usando os personagens favoritos da criança.',
'Crie uma história curta com o personagem preferido fazendo a rotina de dormir. Repita a mesma história toda noite. A previsibilidade acalma e prepara para o sono.',
'2-10 anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- COMUNICAÇÃO (4 manejos)
-- =====================================================
('Prancha de Emoções', 6,
'Usar imagens de sentimentos para ajudar a criança a comunicar como está se sentindo.',
'Crie ou imprima uma prancha com fotos/desenhos de emoções (feliz, triste, com raiva, com medo, cansado). Ensine a criança a apontar como está se sentindo.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Timer Visual', 6,
'Usar relógios visuais para ajudar na compreensão de tempo e transições.',
'Use ampulhetas coloridas ou apps de timer visual para mostrar quanto tempo falta para algo terminar ou começar. Ajuda muito em esperas e transições.',
'2+ anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Roteiro Social', 6,
'Criar scripts de como agir em situações sociais específicas.',
'Escreva ou desenhe passo a passo o que fazer em situações como: cumprimentar alguém, pedir para brincar, esperar a vez. Pratique em casa antes da situação real.',
'3+ anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Comunicação Alternativa', 6,
'Implementar formas alternativas de comunicação além da fala.',
'Se a criança tem dificuldade com fala, introduza PECS (troca de figuras), pranchas de comunicação ou aplicativos de CAA. Comunicação alternativa NÃO impede o desenvolvimento da fala!',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- ROTINA (2 manejos)
-- =====================================================
('Quadro de Rotina Visual', 9,
'Criar um quadro com imagens mostrando a sequência de atividades do dia.',
'Monte um quadro com fotos ou pictogramas das atividades na ordem: acordar, café, escola, etc. A criança pode mover as imagens para "feito" conforme realiza cada atividade.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Aviso de Mudança', 9,
'Preparar com antecedência quando houver alterações na rotina.',
'Quando souber de mudanças, avise com o máximo de antecedência possível. Use calendário, countdown, histórias sociais. Quanto mais previsível a mudança, menor a ansiedade.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- ESCOLA (3 manejos)
-- =====================================================
('Pasta de Comunicação Família-Escola', 8,
'Manter um caderno ou pasta que vai e volta entre casa e escola com informações do dia.',
'Crie uma pasta/caderno onde você e a professora anotam: como a criança dormiu, se comeu, como foi na escola, se teve crise, conquistas do dia. Facilita muito o acompanhamento.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Combinados Visuais', 8,
'Transformar regras da sala em imagens claras e positivas.',
'Peça para a escola ter regras em formato visual: "Levantar a mão para falar" com imagem, "Esperar a vez" com imagem. Regras visuais são mais fáceis de entender e lembrar.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Cantinho da Calma na Escola', 8,
'Solicitar um espaço na escola onde a criança possa se autorregular quando precisar.',
'Converse com a escola sobre ter um local com almofadas, fone de ouvido, fidgets, onde a criança possa ir quando sentir que está ficando sobrecarregada, ANTES da crise acontecer.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- SOCIALIZAÇÃO (2 manejos)
-- =====================================================
('Play Date Estruturado', 7,
'Organizar encontros para brincar com roteiro e atividades planejadas.',
'Convide uma criança por vez. Planeje atividades específicas: 15min de massinha, 15min de jogo, 15min de lanche. Estrutura ajuda a criança autista a participar melhor.',
'3+ anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Modelagem Social', 7,
'Usar bonecos ou vídeos para ensaiar situações sociais.',
'Brinque de boneco encenando situações: "O boneco quer brincar, como ele pode pedir?" ou assista vídeos de situações sociais e converse sobre o que aconteceu.',
'3+ anos', NULL, 0, @AdminId, GETUTCDATE(), 1),

-- =====================================================
-- AUTOCUIDADO (2 manejos - usando categoria Outros=10 temporariamente)
-- =====================================================
('Escovação Sensorial dos Dentes', 10,
'Adaptar a escovação de dentes para crianças com sensibilidade oral.',
'Experimente escovas com cerdas macias, elétricas (vibração pode ajudar ou atrapalhar), diferentes sabores de pasta. Deixe a criança controlar a escova. Comece pelos dentes da frente.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1),

('Higiene Visual Passo a Passo', 10,
'Criar guias visuais para atividades de autocuidado como lavar mãos, ir ao banheiro.',
'Cole na parede do banheiro uma sequência de fotos: 1) Abrir torneira 2) Molhar mãos 3) Passar sabonete 4) Esfregar 5) Enxaguar 6) Secar. Imagens ajudam na independência.',
'Todas as idades', NULL, 0, @AdminId, GETUTCDATE(), 1)

PRINT '29 Manejos inseridos com sucesso!'
PRINT 'Categorias: CriseSensorial(5), Ansiedade(3), Banho(3), Alimentacao(3), Sono(3), Comunicacao(4), Rotina(2), Escola(3), Socializacao(2), Autocuidado(2)'
